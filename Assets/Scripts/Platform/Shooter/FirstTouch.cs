using UnityEngine;

public interface IFrameStates
{
    void StateFrameCheck();
}

public class FirstTouch : IFrameStates
{
    private PlatformShoot platform;
    private Vector2 firstTouchedPos;

    public FirstTouch(PlatformShoot platformTr)
    {
        this.platform = platformTr;
    }

    public void StateFrameCheck()
    {
        if (TouchInput.TouchDown())
        {
            firstTouchedPos = (Vector2)Camera.main.ScreenToWorldPoint(TouchInput.TouchPos());
        }
        else if (TouchInput.Touch() && platform.IsPlatformTouch(firstTouchedPos))
        {
            CheckStateChange();
        }
        else if (TouchInput.TouchUp() && platform.IsPlatformTouch(firstTouchedPos))
        {
            platform.ReleaseGizmo();
        }
    }

    private void CheckStateChange()
    {
        DirectionVector currentTouch = MathCalc.GetTouchDistance(firstTouchedPos);
        platform.DrawGizmos(firstTouchedPos, currentTouch);
        if (platform.IsDirectionValid(currentTouch))
            platform.ChangeState(new TouchDragAim(platform, firstTouchedPos));
    }
}