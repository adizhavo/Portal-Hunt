using UnityEngine;
using System.Collections;

public enum PlayerType
{
    Neutral,
    APlayer, 
    BPlayer
}

public class PlatformMain : MonoBehaviour
{
    public PlayerType PlatformType;

    [SerializeField] private FrameStateObject[] PlatformStates;
    [SerializeField] private BulletContainer Container;

    private void Start()
    {
        InitializeStates();
        Container.Init(PlatformType);
    }

    private void InitializeStates()
    {
        for (int i = 0; i < PlatformStates.Length; i ++)
            PlatformStates[i].Init();
    }
}
