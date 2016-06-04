using UnityEngine;
using System.Collections;

public class TerrainStopObject : StopObject {

    [SerializeField] protected TerrainChanger terrainAnim;

    private float timeWait = -1f;

    private enum States
    {
        Idle,
        Waiting
    }
    private States currentState;

    public void ResetWait()
    {
        timeWait = Mathf.Epsilon;
    }

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);
        timeWait = StopTime;

        if (currentState.Equals(States.Idle))
            terrainAnim.AnimateEntry(coll);
        else
            terrainAnim.AnotherCollision(this, coll);

        currentState = States.Waiting;
    }

    private void Awake()
    {
        currentState = States.Idle;
    }

    private void LateUpdate()
    {
        if (timeWait > 0)
        {
            timeWait -= Time.deltaTime;

            if (timeWait < 0)
            {
                terrainAnim.AnimateExit();
                currentState = States.Idle;
            }
        }
    }
}
