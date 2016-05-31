using UnityEngine;
using System.Collections;

public enum Position
{
    Left, 
    Right
}

public class PlatformShoot : FrameStateObject
{
    #region Public Fields
    public float MinDistanceOfTouch { get { return minDistanceOfTouch; } }

    public float MaxDragDistance { get { return maxDragDistance; } }

    public float ShootForceMultiplier { get { return shootForceMultiplier; } }

    public string BulletCallCode { get { return bulletCallCode; } }

    public string DragGizmosCallCode { get { return dragGizmosCallCode; } }

    // Setted in the editor or throught code
    public Position ShooterPosition;
    #endregion

    #region Protected Fields
    [SerializeField] protected float minDistanceOfTouch;
    [SerializeField] protected float maxDragDistance;
    [SerializeField] protected float shootForceMultiplier;
    [SerializeField] protected string bulletCallCode = "Bullet";
    [SerializeField] protected string dragGizmosCallCode = "DragGizmos";
    #endregion

    protected override void InitializeStates()
    {
        currentState = new FirstTouch(this);
    }

    public void Shoot(DirectionVector dirVect)
    {
        GameObject bullet = ObjectFactory.Instance.CreateObjectCode(BulletCallCode) as GameObject;

        if (bullet != null)
        {
            ShootBullet(bullet.GetComponent<BulletMovement>(), dirVect);
        }
    }

    protected virtual void ShootBullet(BulletMovement bullet, DirectionVector dirVect)
    {
        if (bullet != null)
        {
            DirectionVector multipliedDirVect = new DirectionVector(dirVect.direction * shootForceMultiplier);
            bullet.SetDirection(multipliedDirVect, transform.position);
        }
        else
        {
            Debug.LogWarning("You tried to shoot not a bullet, check your call to the factory!");
        }
    }

    public DragGizmos GetGizmos()
    {
        GameObject gizmos = ObjectFactory.Instance.CreateObjectCode(DragGizmosCallCode) as GameObject;

        if (gizmos != null)
        {
            return gizmos.GetComponent<DragGizmos>();
        }

        return null;
    }
}