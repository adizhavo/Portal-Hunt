using UnityEngine;
using System.Collections;

public class PortalStopObject : StopObject {

    public PortalHealth healthBar;

    protected ComboCalculator calculator = new ComboCalculator();
    [SerializeField] protected string smallTextCodeCall = "SmallFloatingText";
    [SerializeField] protected string bigTextCodeCall = "BigFloatingText";

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);
        healthBar.ApplyDamage(calculator.GetDamage(this));
        DisplayFloatingText();
    }

    private void DisplayFloatingText()
    {
        FloatingText text = ObjectFactory.Instance.CreateObjectCode(GetTextCallCode()).GetComponent<FloatingText>();
        if (text != null)
        {
            text.Initialize(transform.position + new Vector3(0f, 1f, 0f), 0f, string.Format("Combo {0}x", calculator.GetComboMultiplier(this)), Color.white);
        }
    }

    private string GetTextCallCode()
    {
        if (calculator.GetComboMultiplier(this) < 3)
            return smallTextCodeCall;

        return bigTextCodeCall;
    }
}

public class ComboCalculator
{
    public float GetDamage(PortalStopObject portal)
    {
        return 1;
    }

    public int GetComboMultiplier(PortalStopObject portal)
    {
        return 1;
    }
}