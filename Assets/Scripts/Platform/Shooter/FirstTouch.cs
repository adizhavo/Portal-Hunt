using UnityEngine;

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
            firstTouchedPos = (Vector2)Camera.main.ScreenToWorldPoint(TouchInput.TouchPos());

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