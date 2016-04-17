using UnityEngine;
using System.Collections;

public interface IShootStates
{
    void ShootFrameCheck();
}

public class DetectTouchPos : IShootStates
{
    private PlatformShoot platform;

    public DetectTouchPos(PlatformShoot platformTr)
    {
        this.platform = platformTr;
    }

    public void ShootFrameCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float touchDistance = MathCalc.GetTouchDistance(platform.Position).magnitudeOfDir;

            if (touchDistance < platform.MinDistanceOfTouch)
            {
                platform.ChangeShooterState(new AimDragger(platform));
            }
        }
    }
}

public class AimDragger : IShootStates
{
    private PlatformShoot platform;
    private ShooterTrajectory shootTraject;
    private DirectionVector shootDirValues;
    private Vector3 DeltaMove;

    public AimDragger(PlatformShoot platform)
    {
        this.platform = platform;

        shootTraject = ShooterTrajectory.GetShooterTrajectory();
        SetNewShootDirection();
    }

    public void ShootFrameCheck()
    {
        if (Input.GetMouseButton(0))
        {
            SetNewShootDirection();
            DebugDragVector();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            shootTraject.Disable();
            DirectionVector shVal = new DirectionVector(-1 * shootDirValues.direction, shootDirValues.magnitudeOfDir);
            platform.ChangeShooterState(new ObjectShooter(platform, ref shVal));
        }
    }

    private void SetNewShootDirection()
    {
        DirectionVector currentCalcDir = MathCalc.GetTouchDistance(platform.Position);
        MathCalc.ClampVectMagnitude(ref currentCalcDir, platform.MaxDragDistance);

        if (!IsAngleOutRange(currentCalcDir))
        {
            shootDirValues = currentCalcDir;
        }

        shootTraject.Calculate(shootDirValues, platform.ShootForceMultiplier);
    }

    private bool IsAngleOutRange(DirectionVector currentCalcDir)
    {
        float currentAngle = Vector2.Angle(Vector2.right, (Vector2)platform.Position + currentCalcDir.direction);

        bool isOut = (platform.Position.y < currentCalcDir.direction.y
            || currentAngle > platform.MinMaxAngles.y
            || currentAngle < platform.MinMaxAngles.x);

        return isOut;
    }

    private void DebugDragVector()
    {
        #if UNITY_EDITOR

        Debug.DrawLine(platform.Position, platform.Position + (Vector3)shootDirValues.direction, Color.yellow);

        #endif
    }
}

public class ObjectShooter : IShootStates
{
    private PlatformShoot platform;
    private DirectionVector shootDirValues;

    public ObjectShooter(PlatformShoot platform, ref DirectionVector shootDirValues)
    {
        this.platform = platform;
        this.shootDirValues = shootDirValues;
    }

    public void ShootFrameCheck()
    {
        platform.Shoot(shootDirValues);
        platform.ChangeShooterState(new DetectTouchPos(platform));
    }
}