public interface Damagable 
{
    PlayerType Type {set;get;}
    float Damage {get;}
    void BoostDamage(float boostValue);
}
