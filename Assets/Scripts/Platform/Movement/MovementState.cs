using UnityEngine;
using System.Collections;

public class NormalMovement : IFrameStates
{
    private PlatformMovement platform;
    private Vector2 targetPos;

    public NormalMovement(PlatformMovement platform, Vector2 targetPos)
    {
        this.platform = platform;
        this.targetPos = targetPos;
    }

    public void StateFrameCheck()
    {
        platform.RotateTowardTarget(targetPos);
        platform.Move();

        CheckPositionDistance();
        DebugSelectedPos();
    }

    private void CheckPositionDistance()
    {
        float currentDis = Vector2.Distance(targetPos, platform.Position2D);

        if (currentDis < platform.MinDistToChange)
        {
            platform.ChangeState(new PositionChooser(platform));
        }
    }

    protected virtual void DebugSelectedPos()
    {
        #if UNITY_EDITOR

        Debug.DrawLine(platform.Position2D, targetPos, Color.red);
        Debug.DrawLine(platform.Position2D, platform.Position2D + platform.Direction, Color.green);

        #endif
    }
}

public class PositionChooser : IFrameStates
{
    private PlatformMovement platform;

    public PositionChooser(PlatformMovement platform)
    {
        this.platform = platform;
    }

    public void StateFrameCheck()
    {
        Vector2 selectPos = platform.SelectPositionInZone();

        platform.ChangeState(new NormalMovement(platform, selectPos));
    }
}
