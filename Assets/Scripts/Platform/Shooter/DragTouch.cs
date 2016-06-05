using UnityEngine;

public class DragTouch : IFrameStates
{
    private PlatformShoot platform;
    private DirectionVector shootDir;
    private Vector2 firstTouchedPos;
    private float lerpSpeed = 4;

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
            platform.ReleaseTrajectory();
            platform.ReleaseGizmo();
            ValidateShoot();
        }
    }

    private void CalculateShootDir(bool lerpShootDir)
    {
        DirectionVector calcShootDir = platform.GetCalculatedShoot(firstTouchedPos);
        shootDir = lerpShootDir ? LerpShootDir(shootDir, calcShootDir) : calcShootDir;
        platform.DrawTrajecotry(shootDir);
        platform.DrawGizmo(firstTouchedPos, calcShootDir, true);
    }

    private DirectionVector LerpShootDir(DirectionVector shootDir, DirectionVector calcShoot)
    {
        shootDir.direction = Vector2.Lerp(shootDir.direction, new Vector2(calcShoot.direction.x, calcShoot.direction.y), Time.deltaTime * lerpSpeed);
        return shootDir;
    }

    private void ValidateShoot()
    {
        DirectionVector calcShootDir = platform.GetCalculatedShoot(firstTouchedPos);

        if (platform.IsDirectionValid(calcShootDir))
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