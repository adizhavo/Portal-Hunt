using UnityEngine;
using System.Collections;

public class ADefence_BDefence_MiddleCounterClockwise : MapState 
{
    [SerializeField] private TerrainChanger defenceA;
    [SerializeField] private TerrainChanger defenceB;
    [SerializeField] private RotatingTerrain rotatingTerrain;

    public override bool IsStateActive()
    {
        return defenceA.IsOpen() && defenceB.IsOpen() && rotatingTerrain.IsOpen() && !rotatingTerrain.ClockwiseRotation();
    }

    public override byte[] GetStateId()
    {
        return new byte[] { 0, 0, 1, 0 };
    }
}