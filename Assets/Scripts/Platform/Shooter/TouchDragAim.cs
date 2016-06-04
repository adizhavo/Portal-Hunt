﻿using UnityEngine;
public class TouchDragAim : IFrameStates
{
    private PlatformShoot platform;
    private ShooterTrajectory shootTraject;
    private DirectionVector shootDir;
    private Vector2 targetPosition;
    private DragGizmos dragGizmos;
    private float lerpSpeed = 4;

    public TouchDragAim(PlatformShoot platform, Vector2 targetPos, DragGizmos dragGizmos)
    {
        this.platform = platform;
        this.targetPosition = targetPos;
        this.dragGizmos = dragGizmos;

        shootTraject = new ShooterTrajectory();
        CalculateShootDir(false);
    }

    public void StateFrameCheck()
    {
        if (TouchInput.Touch())
        {
            CalculateShootDir(true);
            DebugDragVector();
        }
        else if (TouchInput.TouchUp())
        {
            ValidateShoot();

            shootTraject.Disable();
            dragGizmos.Release();
        }
    }

    private void CalculateShootDir(bool lerpShootDir)
    {
        DirectionVector calcShootDir = GetCalculatedShoot();
        shootDir = lerpShootDir ? LerpShootDir(calcShootDir) : calcShootDir;

        shootTraject.Enable();
        shootTraject.Calculate(shootDir, platform.Position2D, platform.ShootForceMultiplier);

        bool isAllowedShoot = IsMinDistanceOfShot(calcShootDir.magnitudeOfDir) && IsInAllowedShootPosition(calcShootDir.direction);

        dragGizmos.Enable();
        dragGizmos.SetState(isAllowedShoot);
        dragGizmos.SetDraggableZone(2, 2);
        dragGizmos.PositionObject(targetPosition, targetPosition + calcShootDir.direction);
    }

    private DirectionVector GetCalculatedShoot()
    {
        DirectionVector calcShootDir = MathCalc.GetTouchDistance(targetPosition);
        MathCalc.ClampVectMagnitude(ref calcShootDir, platform.MaxDragDistance);
        return calcShootDir;
    }

    private DirectionVector LerpShootDir(DirectionVector calcShoot)
    {
        DirectionVector sDir = shootDir;
        sDir.direction = Vector2.Lerp(sDir.direction, calcShoot.direction, Time.deltaTime * lerpSpeed);
        sDir.magnitudeOfDir = Mathf.Lerp(sDir.magnitudeOfDir, calcShoot.magnitudeOfDir, Time.deltaTime * lerpSpeed);
        return sDir;
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