using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameResult
{
    Draw,
    Win, 
    Lost
}

public class GameEnd : MonoBehaviour
{
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject LostScreen;
    [SerializeField] private GameObject DrawScreen;

    public void Show(GameResult result)
    {
        if(result.Equals(GameResult.Draw))
        {
            DrawScreen.SetActive(true);
        }
        else if(result.Equals(GameResult.Win))
        {
            WinScreen.SetActive(true);
        }
        else if (result.Equals(GameResult.Lost))
        {
            LostScreen.SetActive(true);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }
}