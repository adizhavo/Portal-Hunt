using UnityEngine;
using System.Collections;

public abstract class FrameStateObject : MonoBehaviour
{
    public Vector3 Position { get { return transform.position; } }
    public Vector2 Position2D { get { return (Vector2)transform.position; } }

    protected IFrameStates currentState;

    private void Update()
    {
       currentState.StateFrameCheck();
    }

    public void ChangeState(IFrameStates newState)
    {
        currentState = newState;
    }

    public abstract void Init();
}
