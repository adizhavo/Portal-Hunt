using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour, Stoppable, Damagable
{
    public CooldownGizmos cooldownGizmo;

    public PlayerType Type {get; set;}

    public Rigidbody2D RigidBody
    {
        get { return movement.BulletRgB; }
    }

    protected enum State
    {
        Ready, 
        Fired,
        Cooldown, 
    }
    protected State currentBulletState;
    private BulletMovement movement;

    #region Stoppable Imp
    private float cooldownTime = 0f;
    public float CooldownTime
    {
        get { return cooldownTime; }
    }

    private float previousCooldown = 1f;
    public float PreviousCooldown
    {
        get { return previousCooldown; }
    }

    public void StopForSec(float sec)
    {
        cooldownTime += sec;

        if (currentBulletState.Equals(State.Fired))
            previousCooldown = sec;
        currentBulletState = State.Cooldown;

        movement.BulletRgB.isKinematic = true;
    }

    public bool IsReleased()
    {
        return currentBulletState.Equals(State.Ready);
    }

    public bool IsInCooldown()
    {
        return currentBulletState.Equals(State.Cooldown);
    }
    #endregion

    #region Damagable Imp

    [SerializeField] private float damage = 1f;
    public float Damage
    {
        get { return damage; }
    }

    public void BoostDamage(float boostValue)
    {
        damage *= boostValue;
    }
    #endregion

    public void Shoot(DirectionVector direction, Vector3 initialPos)
    {
        currentBulletState = State.Fired;
        movement.BulletRgB.isKinematic = false;
        movement.SetDirection(direction, initialPos);
    }

    private void Awake()
    {
        movement = new BulletMovement(transform);
        Setup();
    }

    private void Setup()
    {
        gameObject.SetActive(true);
        currentBulletState = State.Ready;
        movement.BulletRgB.isKinematic = true;
        cooldownGizmo.Init(this);
    }

    private void Update()
    {
        UpdateBulletState();
    }

    private void FixedUpdate()
    {
        movement.FrameUpdate();
    }

    private void UpdateBulletState()
    {
        if (currentBulletState.Equals(State.Cooldown))
        {
            if (cooldownTime > 0f)
                cooldownTime -= Time.deltaTime;
            else
            {
                cooldownTime = 0f;
                currentBulletState = State.Ready;
                // Call event Maybe..
            }
        }
    }
}