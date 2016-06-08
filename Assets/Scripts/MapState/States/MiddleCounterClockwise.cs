using UnityEngine;
using System.Collections;

public class MiddleCounterClockwise : MapState 
{
    [SerializeField] private RotatingTerrain rotatingTerrain;

    public override bool IsStateActive()
    {
        return rotatingTerrain.IsOpen() && !rotatingTerrain.ClockwiseRotation();
    }

    public override byte[] GetStateId()
    {
        return new byte[] { 1, 0, 1, 1 };
    }
}
