using UnityEngine;
using System.Collections;

public interface IFrameStates
{
    void StateFrameCheck();
}

public class FirstTouch : IFrameStates
{
    private PlatformShoot platform;

    public FirstTouch(PlatformShoot platformTr)
    {
        this.platform = platformTr;
    }

    public void StateFrameCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float touchDistance = MathCalc.GetTouchDistance(platform.Position2D).magnitudeOfDir;

            if (touchDistance < platform.MinDistanceOfTouch)
            {
                platform.ChangeState(new TouchDragAim(platform));
            }
        }
    }
}

public class TouchDragAim : IFrameStates
{
    private PlatformShoot platform;
    private ShooterTrajectory shootTraject;
    private DirectionVector shootDir;
    private Vector3 DeltaMove;

    public TouchDragAim(PlatformShoot platform)
    {
        this.platform = platform;

        shootTraject = ShooterTrajectory.GetShooterTrajectory();
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
            DirectionVector shVal = new DirectionVector(-1 * shootDir.direction, shootDir.magnitudeOfDir);
            platform.ChangeState(new TouchReleaseShooter(platform, ref shVal));
        }
    }

    private void SetNewShootDirection()
    {
        DirectionVector calcShootDir = MathCalc.GetTouchDistance(platform.Position2D);
        MathCalc.ClampVectMagnitude(ref calcShootDir, platform.MaxDragDistance);

        if (!IsAngleOutRange(calcShootDir))
        {
            shootDir = calcShootDir;
        }

        shootTraject.Calculate(shootDir, platform.Position2D, platform.ShootForceMultiplier);
    }

    private bool IsAngleOutRange(DirectionVector calcShootDir)
    {
        float currentAngle = Vector2.Angle(Vector2.right, calcShootDir.direction);

        bool isOut = (platform.Position2D.y < calcShootDir.direction.y
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