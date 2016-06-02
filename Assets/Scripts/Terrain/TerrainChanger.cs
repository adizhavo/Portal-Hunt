using UnityEngine;
using System.Collections;

public class TerrainChanger : StopObject {

    [SerializeField] protected TerrainAnimation terrainAnim;

    private float timeWait = -1f;

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);
        terrainAnim.AnimateEntry();

        timeWait = StopTime;
    }

    private void Update()
    {
        if (timeWait > 0)
        {
            timeWait -= Time.deltaTime;

            if (timeWait < 0)
            {
                terrainAnim.AnimateExit();
            }
        }
    }
}
