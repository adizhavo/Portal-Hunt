public interface Stoppable
{
    float reloadTime {get;}
    float CooldownTime {get;}
    float PreviousCooldown {get;}
    void StopForSec(float sec);
    bool IsReleased();
    bool IsInCooldown();
}