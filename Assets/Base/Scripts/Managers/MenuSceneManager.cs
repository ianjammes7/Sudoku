using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void OnEasyGameClicked()
    {
        GameManager.Instance._GameMode = GAME_MODE.EASY;
        GameManager.Instance.gameModeString = "easy";
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void OnMediumGameClicked()
    {
        GameManager.Instance._GameMode = GAME_MODE.MEDIUM;
        GameManager.Instance.gameModeString = "medium";
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void OnHardGameClicked()
    {
        GameManager.Instance._GameMode = GAME_MODE.HARD;
        GameManager.Instance.gameModeString = "hard";
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }



}
