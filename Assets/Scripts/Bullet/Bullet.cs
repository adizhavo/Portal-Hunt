using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    
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

    public void WaitTime(float time)
    {
        timeWait += time;
        currentBulletState = State.Cooldown;
        movement.PhysicsDisabled = false; 
    }

    public bool IsReady()
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
