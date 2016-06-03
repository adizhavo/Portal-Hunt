using UnityEngine;
using System.Collections;

public interface IFrameStates
{
    void StateFrameCheck();
}

public class FirstTouch : IFrameStates
{
    private PlatformShoot platform;
    private Vector2 firstTouchedPos;
    private DragGizmos dragGizmos;

    public FirstTouch(PlatformShoot platformTr)
    {
        this.platform = platformTr;
    }

    public void StateFrameCheck()
    {
        if (TouchInput.TouchDown())
        {
            firstTouchedPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (IsTouchAtRightPosition())
            {
                dragGizmos = platform.GetGizmos();
                ValidateCurrentTouch();
            }
        }
        else if (TouchInput.Touch() && IsTouchAtRightPosition())
        {
            ValidateCurrentTouch();
        }
        else if (TouchInput.TouchUp() && IsTouchAtRightPosition())
        {
            dragGizmos.Release();
        }
    }

    private void ValidateCurrentTouch()
    {
        DirectionVector currentTouch = MathCalc.GetTouchDistance(firstTouchedPos);
        DrawGizmos(currentTouch);

        if (IsDragValid(currentTouch))
            platform.ChangeState(new TouchDragAim(platform, firstTouchedPos, dragGizmos));
    }

    private bool IsTouchAtRightPosition()
    {
        int touchPos = (int)Camera.main.WorldToScreenPoint(firstTouchedPos).x;

        if (touchPos - Screen.width/2 > 0 && platform.ShooterPosition.Equals(Position.Right)) return true;
        if (touchPos - Screen.width/2 < 0 && platform.ShooterPosition.Equals(Position.Left)) return true;

        return false;
    }

    private bool IsDragValid(DirectionVector currentTouch)
    {
        return currentTouch.magnitudeOfDir > platform.MinDistanceOfTouch && currentTouch.direction.y < - platform.MinDistanceOfTouch;
    }

    private void DrawGizmos(DirectionVector currentTouch)
    {
        dragGizmos.PositionObject(firstTouchedPos, firstTouchedPos + currentTouch.direction);
        dragGizmos.SetState(false);
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

    public TouchDragAim(PlatformShoot platform, Vector2 targetPos, DragGizmos dragGizmos)
    {
        this.platform = platform;
        this.targetPosition = targetPos;
        this.dragGizmos = dragGizmos;

        shootTraject = new ShooterTrajectory();
        SetNewShootDirection();
    }

    public void StateFrameCheck()
    {
        if (TouchInput.Touch())
        {
            SetNewShootDirection();
            DebugDragVector();
        }
        else if (TouchInput.TouchUp())
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
        return currentDistance > platform.MinDistanceOfTouch;
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