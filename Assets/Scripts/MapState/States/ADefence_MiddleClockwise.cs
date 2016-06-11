using UnityEngine;
using System.Collections;

public class ADefence_MiddleClockwise : MapState 
{
    [SerializeField] private TerrainChanger defenceA;
    [SerializeField] private RotatingTerrain rotatingTerrain;

    public override bool IsStateActive()
    {
        return defenceA.IsOpen() && rotatingTerrain.IsOpen() && rotatingTerrain.ClockwiseRotation();
    }

    public override int GetStateId()
    {
        return 3;
    }
}
