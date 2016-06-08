using UnityEngine;
using System.Collections;

public class BDefence_MiddleClockwise : MapState 
{
    [SerializeField] private TerrainChanger defenceB;
    [SerializeField] private RotatingTerrain rotatingTerrain;

    public override bool IsStateActive()
    {
        return defenceB.IsOpen() && rotatingTerrain.IsOpen() && rotatingTerrain.ClockwiseRotation();
    }

    public override byte[] GetStateId()
    {
        return new byte[] { 0, 1, 0, 1 };
    }
}
