using UnityEngine;
using System.Collections;

public class PortalStopObject : StopObject
{
    public PlayerType PortalSide;
    public PortalHealth healthBar;

    protected ComboCalculator opponentCombo = new ComboCalculator();
    [SerializeField] protected string smallTextCodeCall = "SmallFloatingText";
    [SerializeField] protected string bigTextCodeCall = "BigFloatingText";

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);
        ApplyCalculatedDamage(coll.transform.GetComponent<Damagable>());
    }

    private void Awake()
    {
        ComboBreaker.OnComboFinished += ComboFinished;
    }

    private void OnDestroy()
    {
        ComboBreaker.OnComboFinished -= ComboFinished;
    }

    public void ComboFinished(PlayerType type)
    {
        if (!type.Equals(PortalSide)) opponentCombo.ResetCombo();
    }

    private void ApplyCalculatedDamage(Damagable dmg)
    {
        if (dmg.Type.Equals(PortalSide))
            return;

        float damage = opponentCombo.GetDamage(dmg);
        healthBar.ApplyDamage(damage);
        DisplayFloatingText();
        opponentCombo.IncreseCombo();
    }

    private void DisplayFloatingText()
    {
        FloatingText text = ObjectFactory.Instance.CreateObjectCode(GetTextCallCode()).GetComponent<FloatingText>();
        if (text != null)
        {
            text.Initialize(transform.position + new Vector3(0f, 1f, 0f), Random.Range(-1f, 1f), string.Format("Combo {0}x", opponentCombo.GetComboMultiplier()), Color.white);
        }
    }

    private string GetTextCallCode()
    {
        if (opponentCombo.GetComboMultiplier() < 2)
            return smallTextCodeCall;

        return bigTextCodeCall;
    }
}

public class ComboCalculator
{
    private int comboCounter;

    public ComboCalculator()
    {
        ResetCombo();
    }

    public void IncreseCombo()
    {
        comboCounter ++;
    }

    public void ResetCombo()
    {
        comboCounter = 1;
    }

    public float GetDamage(Damagable dmg)
    {
        return dmg == null ? 0f : comboCounter * dmg.Damage;
    }

    public int GetComboMultiplier()
    {
        return comboCounter;
    }
}