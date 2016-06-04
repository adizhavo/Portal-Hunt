using UnityEngine;
using System.Collections;

public abstract class TerrainChanger : MonoBehaviour 
{
    public PlayerType DefenderType;

    public abstract void AnimateEntry(Collision2D coll);
    public abstract void AnimateExit();
    public abstract void AnotherCollision(TerrainStopObject StopObject, Collision2D coll);
}
