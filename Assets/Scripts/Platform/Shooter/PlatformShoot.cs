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

    public string DragGizmosCallCode { get { return dragGizmosCallCode; } }

    // Setted in the editor or throught code
    public Position ShooterPosition;
    #endregion

    #region Protected Fields
    [SerializeField] private BulletContainer bulletContainer;
    [SerializeField] protected float minDistanceOfTouch;
    [SerializeField] protected float maxDragDistance;
    [SerializeField] protected float shootForceMultiplier;
    [SerializeField] protected string dragGizmosCallCode = "DragGizmos";
    #endregion

    protected override void InitializeStates()
    {
        currentState = new FirstTouch(this);
    }

    public virtual void Shoot(DirectionVector dirVect)
    {
        Bullet bullet = bulletContainer.GetAvailableBullet();

        if (bullet != null)
        {
            DirectionVector multipliedDirVect = new DirectionVector(dirVect.direction * shootForceMultiplier);
            bullet.Shoot(multipliedDirVect, transform.position);
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