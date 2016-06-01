using UnityEngine;
using System.Collections;

public class BulletMovement
{
    public bool PhysicsDisabled = true;

    protected DirectionVector dirShoot;
    protected Transform bulletTr;

    public BulletMovement(Transform bulletTr)
    {
        this.bulletTr = bulletTr;
    }

    public void SetDirection(DirectionVector dirShoot, Vector3 initialPos)
    {
        this.dirShoot = dirShoot;
        bulletTr.position = initialPos;
    }

    public void FrameUpdate()
    {
        if (PhysicsDisabled) return;

        CalculateCurrentPos();
    }

    protected virtual void CalculateCurrentPos()
    {
        bulletTr.position += (Vector3)(dirShoot.direction * Time.deltaTime);
        dirShoot.direction += Physics2D.gravity * Time.deltaTime;
    }
}
