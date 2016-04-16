using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {

    protected DirectionVector dirShoot;

    public void SetDirection(DirectionVector dirShoot)
    {
        this.dirShoot = dirShoot;
    }

    protected void Update()
    {
        CalculateCurrentPos();
    }

    protected virtual void CalculateCurrentPos()
    {
        transform.position += (Vector3)(dirShoot.direction * Time.deltaTime);
        dirShoot.direction += Physics2D.gravity * Time.deltaTime;
    }
}
