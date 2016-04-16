using UnityEngine;
using System.Collections;

public class PlatformShoot : MonoBehaviour
{
    #region Public Fields
    public Vector3 Position { get { return transform.position; } }

    public Vector2 MinMaxAngles { get { return minMaxAngles; } }

    public float MinDistanceOfTouch { get { return minDistanceOfTouch; } }

    public float MaxDragDistance { get { return maxDragDistance; } }

    public float ShootForceMultiplier { get { return shootForceMultiplier; } }

    public string FactoryCallCode { get { return factoryCallCode; } }
    #endregion

    #region Private Fields
    [SerializeField] protected Vector2 minMaxAngles;
    [SerializeField] protected float minDistanceOfTouch;
    [SerializeField] protected float maxDragDistance;
    [SerializeField] protected float shootForceMultiplier;
    [SerializeField] protected string factoryCallCode = "ShootParticle";
    #endregion

    protected IShootStates currentState;

    private void Awake()
    {
        InitializeStates();
    }

    private void Update()
    {
        currentState.ShootFrameCheck();
    }

    protected virtual void InitializeStates()
    {
        currentState = new DetectTouchPos(this);
    }

    public void ChangeShooterState(IShootStates newState)
    {
        currentState = newState;
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
            bullet.SetDirection(multipliedDirVect);
        }
        else
        {
            Debug.LogWarning("You tried to shoot not a bullet, check you call to the factory!");
        }
    }
}