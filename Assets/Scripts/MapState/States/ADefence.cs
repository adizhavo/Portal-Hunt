using UnityEngine;
using System.Collections;

public class ADefence : MapState 
{
    [SerializeField] private TerrainChanger defenceA;

    public override bool IsStateActive()
    {
        return defenceA.IsOpen();
    }

    public override int GetStateId()
    {
        return 8;
    }
}
