using System;
using UnityEngine;

public class AIDatabase : MonoBehaviour {

    private static AIDatabase instance;
    public static AIDatabase Instance
    {
        get { return instance; }
    }   

    private void Awake()
    {
        instance = this;
        Init();
    }

    protected virtual void Init()
    {
        
    }

    public void LearnShot(DirectionVector shootDir, PlatformPos shootPos, int mapState)
    {
        
    }

    public DirectionVector GetShot(PlatformPos AIPos, int mapState)
    {
        return new DirectionVector(Vector2.zero);
    }
}
