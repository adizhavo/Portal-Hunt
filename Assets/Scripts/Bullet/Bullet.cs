using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour, Stoppable {

    public Rigidbody2D RigidBody
    {
        get { return movement.BulletRgB; }
    }

    protected enum State
    {
        Ready, 
        Fired,
        Cooldown
    }
    protected State currentBulletState;

    [SerializeField] public CooldownGizmos CooldDown;
    private BulletMovement movement;

    private float timeWait = 0f;

    public void Shoot(DirectionVector direction, Vector3 initialPos)
    {
        currentBulletState = State.Fired;
        movement.BulletRgB.isKinematic = false;
        movement.SetDirection(direction, initialPos);
    }

    public void StopForSec(float sec)
    {
        timeWait += sec;
        currentBulletState = State.Cooldown;
        movement.BulletRgB.isKinematic = true;

        CooldDown.StartGizmo(timeWait);
    }

    public bool IsReleased()
    {
        return currentBulletState.Equals(State.Ready);
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
    }

    private void Update()
    {
        UpdateBulletState();
        movement.FrameUpdate();
    }

    private void UpdateBulletState()
    {
        if (currentBulletState.Equals(State.Cooldown))
        {
            if (timeWait > 0f)
                timeWait -= Time.deltaTime;
            else
            {
                timeWait = 0f;
                currentBulletState = State.Ready;
                // Call event
            }

            CooldDown.UpdateValue(timeWait);
        }
    }
}