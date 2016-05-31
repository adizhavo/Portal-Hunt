using UnityEngine;
using System.Collections;

public interface IFrameStates
{
    void StateFrameCheck();
}

public class FirstTouch : IFrameStates
{
    private PlatformShoot platform;
    private float minTimeHold = .2f;
    private float timePressed;
    private Vector3 firstTouchedPos;

    public FirstTouch(PlatformShoot platformTr)
    {
        this.platform = platformTr;
        timePressed = 0;
    }

    public void StateFrameCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstTouchedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            timePressed += Time.deltaTime;
            float touchDistance = MathCalc.GetTouchDistance(firstTouchedPos).magnitudeOfDir;

            if (touchDistance < platform.MinDistanceOfTouch || (platform.FreeTouch && timePressed > minTimeHold))
            {
                platform.ChangeState(new TouchDragAim(platform, firstTouchedPos));
            }
        }
        else if (Input.GetMouseButtonUp(0) && platform.FreeTouch)
        {
            timePressed = 0;
        }
    }
}

public class TouchDragAim : IFrameStates
{
    private PlatformShoot platform;
    private ShooterTrajectory shootTraject;
    private DirectionVector shootDir;
    private Vector3 DeltaMove;
    private Vector2 targetPosition;

    public TouchDragAim(PlatformShoot platform, Vector2 targetPos)
    {
        this.platform = platform;
        this.targetPosition = targetPos;

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
            DirectionVector shVal = new DirectionVector(shootDir.InvertedDirection(), shootDir.magnitudeOfDir);
            platform.ChangeState(new TouchReleaseShooter(platform, ref shVal));
        }
    }

    private void SetNewShootDirection()
    {
        DirectionVector calcShootDir = MathCalc.GetTouchDistance(targetPosition);
        MathCalc.ClampVectMagnitude(ref calcShootDir, platform.MaxDragDistance);

        if (!IsAngleOutRange(calcShootDir))
        {
            shootDir = calcShootDir;
        }
        else
        {
            shootDir.direction = new Vector2(calcShootDir.direction.x, shootDir.direction.y);
        }

        shootTraject.Calculate(shootDir, platform.Position2D, platform.ShootForceMultiplier);
    }

    private bool IsAngleOutRange(DirectionVector calcShootDir)
    {
        float sign = Mathf.Sign(Vector3.Cross(Vector2.right, calcShootDir.direction).z);
        float currentAngle = Vector2.Angle(Vector2.right, calcShootDir.direction);

        bool isOut = (sign > 0
            || currentAngle > platform.MinMaxAngles.max
            || currentAngle < platform.MinMaxAngles.min);

        return isOut;
    }

    protected virtual void DebugDragVector()
    {
        #if UNITY_EDITOR

        Debug.DrawLine(platform.Position2D, platform.Position2D + shootDir.direction, Color.yellow);

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