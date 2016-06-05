using UnityEngine;
using System.Collections;

public class PortalHealth : MonoBehaviour {

    public float CurrentHp
    {
        get { return currentHp; }
    }

    public float RigenerationSpeed
    {
        get { return rigeneration; }
    }

    [SerializeField] protected float initHp;
    [SerializeField] protected float rigeneration;
    [SerializeField] protected Transform HealthScaler;
  
    protected float currentHp;

    protected virtual void Start()
    {
        currentHp = initHp;
    }

    protected void Update()
    {
        currentHp += rigeneration * Time.deltaTime;
        currentHp = Mathf.Clamp(currentHp, 0, initHp);
        ChangeGraphic(currentHp);
    }

    public void ApplyDamage(float damage)
    {
        currentHp -= damage;
    }

    // speed is a size between 0-1 per second
    public void SetRigenerationSpeed(float regSpeed)
    {
        if (regSpeed > 0)
            rigeneration = regSpeed > 1f ? 1f : regSpeed;
    }

    protected virtual void ChangeGraphic(float currentHp)
    {
        HealthScaler.localScale = new Vector3(currentHp / initHp, 1f, 1f);
    }
}
