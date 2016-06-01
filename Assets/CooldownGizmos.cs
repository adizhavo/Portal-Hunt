using UnityEngine;
using System.Collections;

public class CooldownGizmos : MonoBehaviour {

    [SerializeField] protected Transform ScalePivot;

    protected float initialValue;
   
    protected virtual void Awake()
    {
        ScalePivot.localScale = Vector3.zero;
    }

    public virtual void StartGizmo(float MaxValue)
    {
        initialValue = MaxValue;
    }

    public virtual void UpdateValue(float currentValue)
    {
        ScalePivot.localScale = new Vector3(currentValue / initialValue, 1f, 1f);
    }
}
