using UnityEngine;
using System.Collections;

public enum GameState
{
    Stop,
    Play
}

public class Main : MonoBehaviour
{
    [SerializeField] private PortalHealth allyHealth;
    [SerializeField] private PortalHealth opponentHealth;
    [SerializeField] private SessionTimer timer;

    [SerializeField] private GameEnd endScreen;

    public static GameState activeState
    {
        private set;
        get;
    }

    private void Awake()
    {
        PortalHealth.OnPlayerDied += GameOver;
        activeState = GameState.Stop;
    }

    private void Start ()
    {
        timer.StartTimer();
        activeState = GameState.Play;
	}

    private void OnDestroy()
    {
        PortalHealth.OnPlayerDied -= GameOver;
    }

	private void Update ()
    {
        if(timer.CurrentTime < Mathf.Epsilon)
        {
            GameOver();
        }
	}

    private void GameOver()
    {
        if (activeState.Equals(GameState.Stop)) return;

        GameResult result = GameResult.Draw;

        if (allyHealth.CurrentHp > opponentHealth.CurrentHp)
        {
            result = GameResult.Win;
        }
        else if (allyHealth.CurrentHp < opponentHealth.CurrentHp)
        {
            result = GameResult.Lost;
        }

        endScreen.Show(result);
        activeState = GameState.Stop;
    }
}