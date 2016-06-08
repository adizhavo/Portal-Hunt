using UnityEngine;
using System.Collections;

public abstract class TerrainChanger : MonoBehaviour 
{
    public PlayerType DefenderType;
    protected ObjectState terrainState = ObjectState.Close;

    protected void Awake()
    {
        SetCloseState();
    }

    public bool IsOpen()
    {
        return terrainState.Equals(ObjectState.Open);
    }

    protected void SetOpenState()
    {
        terrainState = ObjectState.Open;
    }

    protected void SetCloseState()
    {
        terrainState = ObjectState.Close;
    }

    public abstract void AnimateEntry(Collision2D coll);
    public abstract void AnimateExit();
    public abstract void AnotherCollision(TerrainStopObject StopObject, Collision2D coll);
}
