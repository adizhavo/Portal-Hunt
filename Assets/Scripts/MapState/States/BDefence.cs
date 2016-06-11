using UnityEngine;
using System.Collections;

public class BDefence : MapState 
{
    [SerializeField] private TerrainChanger defenceB;

    public override bool IsStateActive()
    {
        return defenceB.IsOpen();
    }

    public override int GetStateId()
    {
        return 9;
    }
}
