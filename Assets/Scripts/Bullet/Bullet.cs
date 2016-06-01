using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour, Stoppable {
    
    protected enum State
    {
        Ready, 
        Fired,
        Cooldown
    }
    protected State currentBulletState = State.Ready;

    private BulletMovement movement;
    private float timeWait = 0f;

    public void Setup()
    {
        movement.PhysicsDisabled = false;
        gameObject.SetActive(true);
    }

    public void Shoot(DirectionVector direction, Vector3 initialPos)
    {
        movement.SetDirection(direction, initialPos);
        currentBulletState = State.Fired;
    }

    public void StopForSec(float sec)
    {
        timeWait += sec;
        currentBulletState = State.Cooldown;
        movement.PhysicsDisabled = false; 
    }

    public bool IsReleased()
    {
        return currentBulletState.Equals(State.Ready);
    }

    private void Awake()
    {
        movement = new BulletMovement(transform);
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
            }
        }
    }
}
