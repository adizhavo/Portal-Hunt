using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class AIDatabase : MonoBehaviour
{
    private static AIDatabase instance;
    public static AIDatabase Instance
    {
        get { return instance; }
    }   

    private List<StateShots> shots = new List<StateShots>();

    private string dataPath = "LearnedData.txt";

    private void Start()
    {
        instance = this;
        Init();
    }

    private void OnDestroy()
    {
        string learnedData = JsonConvert.SerializeObject(shots);
        ResourcesAccessor.SaveTo(dataPath, learnedData);
    }

    protected virtual void Init()
    {
        string learnedData = ResourcesAccessor.LoadFrom(dataPath);
        if (!string.IsNullOrEmpty(learnedData))
        {
            shots = JsonConvert.DeserializeObject<List<StateShots>>(learnedData);
        }
    }

    public void LearnShot(DirectionVector shotDir, PlatformPos shotPos, int mapState)
    {
        GetStateShot(mapState).AddShot(shotPos, shotDir);
    }

    public DirectionVector GetShot(PlatformPos shotPos, int mapState)
    {
        return GetStateShot(mapState).GetRandomShotVector(shotPos);
    }

    private StateShots GetStateShot(int mapState)
    {
        for (int i = 0; i < shots.Count; i++)
            if (shots[i].mapState == mapState) return shots[i];

        StateShots newState = new StateShots(mapState);
        shots.Add(newState);
        return newState;
    }
}

[System.Serializable]
public class StateShots
{
    public int mapState;
    public List<LearnedShots> shotsForState;

    public StateShots(int mapState)
    {
        this.mapState = mapState;
        shotsForState = new List<LearnedShots>();
    }

    public void AddShot(PlatformPos shotPos, DirectionVector shotVector)
    {
        GetShot(shotPos).AddDirection(shotVector);
    }

    public DirectionVector GetRandomShotVector(PlatformPos shotPos)
    {
        return GetShot(shotPos).GetRandomDirection();
    }

    private LearnedShots GetShot(PlatformPos shotPos)
    {
        for (int i = 0; i < shotsForState.Count; i++)
        {
            Position xPos = shotsForState[i].shotPos.xPos;
            Position yPos = shotsForState[i].shotPos.yPos;
            if(shotPos.xPos.Equals(xPos) && shotPos.yPos.Equals(yPos)) 
            {
//                Debug.Log(shotsForState[i].GetRandomDirection());
                return shotsForState[i];
            }
        }

        LearnedShots newShot = new LearnedShots(shotPos);
        shotsForState.Add(newShot);
        return newShot;
    }
}

[System.Serializable]
public class LearnedShots
{
    public PlatformPos shotPos;
    public List<DirectionVector> shots;

    public LearnedShots(PlatformPos shotPos)
    {
        this.shotPos = shotPos;
        shots = new List<DirectionVector>();
        AddDirection(new DirectionVector(Vector2.zero));
    }

    public void AddDirection(DirectionVector vect)
    {
        Vector2 mirroredVector = new Vector2( -vect.direction.x, vect.direction.y );
        DirectionVector mirrored = new DirectionVector(mirroredVector, vect.magnitudeOfDir);
        shots.Add(mirrored);
    }

    public DirectionVector GetRandomDirection()
    {
        int randomIndex = Random.Range(0, shots.Count);
        return shots[randomIndex];
    }
}