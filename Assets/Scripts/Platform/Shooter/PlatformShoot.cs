using UnityEngine;
using System.Collections;

public class PlatformShoot : FrameStateObject
{
    #region Public Fields
    public MinMaxValuesHolder MinMaxAngles { get { return minMaxAngles; } }

    public float MinDistanceOfTouch { get { return minDistanceOfTouch; } }

    public float MaxDragDistance { get { return maxDragDistance; } }

    public float ShootForceMultiplier { get { return shootForceMultiplier; } }

    public string FactoryCallCode { get { return factoryCallCode; } }
    #endregion

    #region Protected Fields
    [SerializeField] protected MinMaxValuesHolder minMaxAngles;
    [SerializeField] protected float minDistanceOfTouch;
    [SerializeField] protected float maxDragDistance;
    [SerializeField] protected float shootForceMultiplier;
    [SerializeField] protected string factoryCallCode = "ShootParticle";
    #endregion

    protected override void InitializeStates()
    {
        currentState = new FirstTouch(this);
    }

    public void Shoot(DirectionVector dirVect)
    {
        GameObject particle = ObjectFactory.Instance.CreateObjectCode(FactoryCallCode) as GameObject;

        if (particle != null)
        {
            ShootBullet(particle.GetComponent<BulletMovement>(), dirVect);
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
            Debug.LogWarning("You tried to shoot not a bullet, check you call to the factory!");
        }
    }
}