public class ReleaseTouch : IFrameStates
{
    private PlatformShoot platform;
    private DirectionVector shootDirValues;

    public ReleaseTouch(PlatformShoot platform, ref DirectionVector shootDirValues)
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