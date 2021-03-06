﻿using UnityEngine;
using System.Collections;

public class BDefence_MiddleCounterClockwise : MapState 
{
    [SerializeField] private TerrainChanger defenceB;
    [SerializeField] private RotatingTerrain rotatingTerrain;

    public override bool IsStateActive()
    {
        return defenceB.IsOpen() && rotatingTerrain.IsOpen() && !rotatingTerrain.ClockwiseRotation();
    }

    public override int GetStateId()
    {
        return 6;
    }
}
