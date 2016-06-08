using UnityEngine;
using System.Collections;

public class ADefence_BDefence : MapState 
{
    [SerializeField] private TerrainChanger defenceA;
    [SerializeField] private TerrainChanger defenceB;

    public override bool IsStateActive()
    {
        return defenceA.IsOpen() && defenceB.IsOpen();
    }

    public override byte[] GetStateId()
    {
        return new byte[] { 0, 1, 1, 1 };
    }
}
