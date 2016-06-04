using UnityEngine;
public class TouchDragAim : IFrameStates
{
    private PlatformShoot platform;
    private ShooterTrajectory shootTraject;
    private DirectionVector shootDir;
    private Vector2 targetPosition;
    private DragGizmos dragGizmos;

    public TouchDragAim(PlatformShoot platform, Vector2 targetPos, DragGizmos dragGizmos)
    {
        this.platform = platform;
        this.targetPosition = targetPos;
        this.dragGizmos = dragGizmos;

        shootTraject = new ShooterTrajectory();
        CalculateShootDir();
    }

    public void StateFrameCheck()
    {
        if (TouchInput.Touch())
        {
            CalculateShootDir();
            DebugDragVector();
        }
        else if (TouchInput.TouchUp())
        {
            ValidateShoot();

            shootTraject.Disable();
            dragGizmos.Release();
        }
    }

    private void CalculateShootDir()
    {
        DirectionVector calcShootDir = MathCalc.GetTouchDistance(targetPosition);
        MathCalc.ClampVectMagnitude(ref calcShootDir, platform.MaxDragDistance);
        shootDir = calcShootDir;

        shootTraject.Enable();
        shootTraject.Calculate(shootDir, platform.Position2D, platform.ShootForceMultiplier);

        bool isAllowedShoot = IsMinDistanceOfShot(calcShootDir.magnitudeOfDir) && IsInAllowedShootPosition(calcShootDir.direction);

        dragGizmos.Enable();
        dragGizmos.SetState(isAllowedShoot);
        dragGizmos.SetDraggableZone(2, 2);
        dragGizmos.PositionObject(targetPosition, targetPosition + calcShootDir.direction);
    }

    private void ValidateShoot()
    {
        DirectionVector calcShootDir = MathCalc.GetTouchDistance(targetPosition);
        MathCalc.ClampVectMagnitude(ref calcShootDir, platform.MaxDragDistance);
        if (IsMinDistanceOfShot(calcShootDir.magnitudeOfDir) && IsInAllowedShootPosition(calcShootDir.direction))
        {
            DirectionVector shVal = new DirectionVector(shootDir.InvertedDirection(), shootDir.magnitudeOfDir);
            platform.ChangeState(new TouchRelease(platform, ref shVal));
        }
        else
            platform.ChangeState(new FirstTouch(platform));
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