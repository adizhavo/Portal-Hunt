using UnityEngine;
using System.Collections;

public class CooldownGizmos : MonoBehaviour {

    [SerializeField] protected Transform ScalePivot;

    protected Stoppable stopObject;

    public void Init(Stoppable stopObject)
    {
        this.stopObject = stopObject;
    }

    public virtual void Update()
    {
        if (stopObject == null)
        {
            ScalePivot.localScale = Vector3.zero;
            return;
        }

        ScalePivot.localScale = new Vector3(stopObject.CooldownTime / stopObject.PreviousCooldown, 1f, 1f);
    }
}
