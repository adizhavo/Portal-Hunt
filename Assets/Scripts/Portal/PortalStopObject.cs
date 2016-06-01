using UnityEngine;
using System.Collections;

public class PortalStopObject : StopObject {

    public PortalHealth healthBar;

    protected ComboCalculator calculator = new ComboCalculator();

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);
        healthBar.ApplyDamage(calculator.GetDamage(this));
    }
}

public class ComboCalculator
{
    public float GetDamage(PortalStopObject portal)
    {
        return 1;
    }
}