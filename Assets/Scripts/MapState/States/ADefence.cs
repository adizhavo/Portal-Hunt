using UnityEngine;
using System.Collections;

public class ADefence : MapState 
{
    [SerializeField] private TerrainChanger defenceA;

    public override bool IsStateActive()
    {
        return defenceA.IsOpen();
    }

    public override byte[] GetStateId()
    {
        return new byte[] { 1, 0, 0, 0 };
    }
}
