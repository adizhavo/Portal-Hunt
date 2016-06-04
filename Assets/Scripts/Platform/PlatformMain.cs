﻿using UnityEngine;
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

    [SerializeField] private BulletContainer Container;

    private void Start()
    {
        Container.Init(PlatformType);
    }
}
