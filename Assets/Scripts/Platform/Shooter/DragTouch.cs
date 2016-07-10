using UnityEngine;

public class DragTouch : IFrameStates
{
    private PlatformShoot platform;
    private DirectionVector shootDir;
    private Vector2 firstTouchedPos;
    private float lerpSpeed = 5f;

    public DragTouch(PlatformShoot platform, Vector2 firstTouchedPos)
    {
        this.platform = platform;
        this.firstTouchedPos = firstTouchedPos;
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
            platform.ReleaseGizmo();
            platform.ReleaseTrajectory();
        }
    }

    private void CalculateShootDir(bool lerpShootDir)
    {
        DirectionVector calcShootDir = platform.GetCalculatedShoot(firstTouchedPos);
        shootDir = lerpShootDir ? LerpShootDir(shootDir, calcShootDir) : calcShootDir;

        platform.DrawTrajecotry(firstTouchedPos, shootDir);
        platform.DrawGizmo(firstTouchedPos, calcShootDir, true);
    }

    private DirectionVector LerpShootDir(DirectionVector from, DirectionVector to)
    {
        from.direction = Vector2.Lerp(from.direction, to.direction, Time.deltaTime * lerpSpeed);
        from.magnitudeOfDir = to.magnitudeOfDir;
        return from;
    }

    private void ValidateShoot()
    {
        if (platform.IsTouchOnGizmo(firstTouchedPos, shootDir))
        {
            DirectionVector shVal = new DirectionVector(shootDir.InvertedDirection(), shootDir.magnitudeOfDir);
            platform.ChangeState(new ReleaseTouch(platform, ref shVal));
        }
        else
            platform.ChangeState(new FirstTouch(platform));
    }

    protected virtual void DebugDragVector()
    {
        #if UNITY_EDITOR

        Debug.DrawLine(firstTouchedPos, firstTouchedPos + shootDir.direction, Color.yellow);

        #endif
    }
}