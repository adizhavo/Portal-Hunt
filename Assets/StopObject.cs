using UnityEngine;
using System.Collections;

public class StopObject : MonoBehaviour {

    [SerializeField] private float StopTime;

    void OnCollisionEnter2D(Collision2D coll)
    {
        Stoppable stoppable = coll.transform.GetComponent<Stoppable>();

        if (stoppable != null)
        {
            stoppable.StopForSec(StopTime);
        }
    }
}
