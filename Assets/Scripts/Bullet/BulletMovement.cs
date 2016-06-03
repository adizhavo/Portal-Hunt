using UnityEngine;
using System.Collections;

public class BulletMovement
{
    public bool PhysicsDisabled = true;

    protected DirectionVector dirShoot;
    protected Rigidbody2D bulletRgB;

    public Rigidbody2D BulletRgB
    {
        get { return bulletRgB; }
    }

    public BulletMovement(Transform bulletTr)
    {
        this.bulletRgB = bulletTr.GetComponent<Rigidbody2D>();
    }

    public void SetDirection(DirectionVector dirShoot, Vector3 initialPos)
    {
        this.dirShoot = dirShoot;
        bulletRgB.position = initialPos;
    }

    public void FrameUpdate()
    {
        if (bulletRgB.isKinematic) return;

        CalculateCurrentPos();
    }

    protected virtual void CalculateCurrentPos()
    {
        bulletRgB.position += dirShoot.direction * Time.fixedDeltaTime;
        dirShoot.direction += Physics2D.gravity * Time.fixedDeltaTime;
    }
}
