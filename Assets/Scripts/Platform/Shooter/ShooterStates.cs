using UnityEngine;
using System.Collections;

public interface IFrameStates
{
    void StateFrameCheck();
}

public class FirstTouch : IFrameStates
{
    private PlatformShoot platform;
    private Vector3 firstTouchedPos;

    public FirstTouch(PlatformShoot platformTr)
    {
        this.platform = platformTr;
    }

    public void StateFrameCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstTouchedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && IsTouchAtRightPosition())
        {
            float touchDistance = MathCalc.GetTouchDistance(firstTouchedPos).magnitudeOfDir;
            if (touchDistance > platform.MinDistanceOfTouch)
            {
                platform.ChangeState(new TouchDragAim(platform, firstTouchedPos));
            }
        }
    }

    private bool IsTouchAtRightPosition()
    {
        int touchPos = (int)Camera.main.WorldToScreenPoint(firstTouchedPos).x;

        if (touchPos - Screen.width/2 > 0 && platform.ShooterPosition.Equals(Position.Right)) return true;
        if (touchPos - Screen.width/2 < 0 && platform.ShooterPosition.Equals(Position.Left)) return true;

        return false;
    }
}

public class TouchDragAim : IFrameStates
{
    private PlatformShoot platform;
    private ShooterTrajectory shootTraject;
    private DirectionVector shootDir;
    private Vector3 DeltaMove;
    private Vector2 targetPosition;
    private DragGizmos dragGizmos;

    public TouchDragAim(PlatformShoot platform, Vector2 targetPos)
    {
        this.platform = platform;
        this.targetPosition = targetPos;

        dragGizmos = platform.GetGizmos();
        shootTraject = ShooterTrajectory.Instance();
        SetNewShootDirection();
    }

    public void StateFrameCheck()
    {
        if (Input.GetMouseButton(0))
        {
            SetNewShootDirection();
            DebugDragVector();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            shootTraject.Disable();
            dragGizmos.Release();

            DirectionVector shVal = new DirectionVector(shootDir.InvertedDirection(), shootDir.magnitudeOfDir);
            platform.ChangeState(new TouchReleaseShooter(platform, ref shVal));
        }
    }

    private void SetNewShootDirection()
    {
        DirectionVector calcShootDir = MathCalc.GetTouchDistance(targetPosition);
        MathCalc.ClampVectMagnitude(ref calcShootDir, platform.MaxDragDistance);

        bool isAllowedShoot = IsMinDistanceOfShot(calcShootDir.magnitudeOfDir) && IsInAllowedShootPosition(calcShootDir.direction);
        if (isAllowedShoot)
        {
            shootDir = calcShootDir;
        }
        else
        {
            shootDir.direction = new Vector2(calcShootDir.direction.x, shootDir.direction.y);
        }

        shootTraject.Calculate(shootDir, platform.Position2D, platform.ShootForceMultiplier);
        dragGizmos.PositionObject(targetPosition, targetPosition + calcShootDir.direction);
        dragGizmos.SetState(isAllowedShoot);
    }

    private bool IsInAllowedShootPosition(Vector2 fingerPos)
    {
        return fingerPos.y < - platform.MinDistanceOfTouch;
    }

    private bool IsMinDistanceOfShot(float currentDistance)
    {
        float touchDistance = currentDistance;
        return touchDistance > platform.MinDistanceOfTouch;
    }

    protected virtual void DebugDragVector()
    {
        #if UNITY_EDITOR

        Debug.DrawLine(targetPosition, targetPosition + shootDir.direction, Color.yellow);

        #endif
    }
}

public class TouchReleaseShooter : IFrameStates
{
    private PlatformShoot platform;
    private DirectionVector shootDirValues;

    public TouchReleaseShooter(PlatformShoot platform, ref DirectionVector shootDirValues)
    {
        this.platform = platform;
        this.shootDirValues = shootDirValues;
    }

    public void StateFrameCheck()
    {
        platform.Shoot(shootDirValues);
        platform.ChangeState(new FirstTouch(platform));
    }
}