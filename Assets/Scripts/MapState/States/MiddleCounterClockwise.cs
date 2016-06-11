using UnityEngine;
using System.Collections;

public class MiddleCounterClockwise : MapState 
{
    [SerializeField] private RotatingTerrain rotatingTerrain;

    public override bool IsStateActive()
    {
        return rotatingTerrain.IsOpen() && !rotatingTerrain.ClockwiseRotation();
    }

    public override int GetStateId()
    {
        return 11;
    }
}
