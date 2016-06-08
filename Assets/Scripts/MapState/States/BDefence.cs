using UnityEngine;
using System.Collections;

public class BDefence : MapState 
{
    [SerializeField] private TerrainChanger defenceB;

    public override bool IsStateActive()
    {
        return defenceB.IsOpen();
    }

    public override byte[] GetStateId()
    {
        return new byte[] { 1, 0, 0, 1 };
    }
}
