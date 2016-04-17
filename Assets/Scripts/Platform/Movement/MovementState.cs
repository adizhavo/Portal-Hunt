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
        CheckPositionDistance();
        RotateTowardTarget();
        DebugSelectedPos();
        platform.Move();
    }

    private void CheckPositionDistance()
    {
        float currentDis = Vector2.Distance(targetPos, platform.Position2D);

        if (currentDis < platform.MinDistToChange)
        {
            platform.ChangeState(new PositionChooser(platform));
        }
    }

    private void RotateTowardTarget()
    {
        Vector2 targetDir = targetPos - platform.Position2D;
        Vector2 platformDir = platform.Direction ;
        float sign = Mathf.Sign(Vector3.Cross(platformDir, targetDir).z);
        float frameAngle = Vector2.Angle(targetDir, platformDir) * platform.RotationSpeed;
        frameAngle = Mathf.Clamp(frameAngle, platform.RotationSpeed, Mathf.Infinity) * sign;
        platform.Rotation = frameAngle;
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
        Vector2 selectPos = SelectPositionInZone();

        platform.ChangeState(new NormalMovement(platform, selectPos));
    }

    private Vector2 SelectPositionInZone()
    {
        MinMaxValuesHolder minMaxHorizontalPos = new MinMaxValuesHolder(platform.MoveZone.x, platform.MoveZone.x + platform.MoveZone.width);
        MinMaxValuesHolder minMaxVerticalPos = new MinMaxValuesHolder(platform.MoveZone.y, platform.MoveZone.y + platform.MoveZone.height);
        float xRandomPos = Random.Range(minMaxHorizontalPos.min, minMaxHorizontalPos.max);
        float yRandomPos = Random.Range(minMaxVerticalPos.min, minMaxVerticalPos.max);

        return new Vector2(xRandomPos, yRandomPos);
    }
}
