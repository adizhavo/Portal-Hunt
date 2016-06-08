﻿using UnityEngine;
using System.Collections;

public class ADefence_MiddleClockwise : MapState 
{
    [SerializeField] private TerrainChanger defenceA;
    [SerializeField] private RotatingTerrain rotatingTerrain;

    public override bool IsStateActive()
    {
        return defenceA.IsOpen() && rotatingTerrain.IsOpen() && rotatingTerrain.ClockwiseRotation();
    }

    public override byte[] GetStateId()
    {
        return new byte[] { 0, 0, 1, 1 };
    }
}
