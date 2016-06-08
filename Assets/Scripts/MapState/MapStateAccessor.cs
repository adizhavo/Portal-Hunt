using UnityEngine;
using System.Collections.Generic;

public enum ObjectState
{
    Open, 
    Close
}

public class MapStateAccessor : MonoBehaviour 
{
    [SerializeField] private List<MapState> states = new List<MapState>();

    private static MapStateAccessor instance;
    public static MapStateAccessor Instance
    {
        get { return instance; }
    }   

    private void Awake()
    {
        instance = this;
    }
}

// We use abstract so we can inherit from MonoBehaviour
public abstract class MapState : MonoBehaviour 
{
    public abstract bool IsStateActive();
    public abstract byte[] GetStateId();
}