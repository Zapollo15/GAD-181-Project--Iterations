using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]public Stack LevelsLeft = new Stack { };
    public int NextLevelIndex =1;
    public enum GameStates
    {
        MainMenuMode,
        PlayGameMode,
        GameOverMode,
    }

    public GameStates GameState;

    void Start()
    {
        AssignAllLevelsToPlay();
        DontDestroyOnLoad(gameObject);
    }

   
    void Update()
    {
        DontDestroyOnLoad(gameObject);
    }

    void AssignAllLevelsToPlay()
    {
        LevelsLeft.Push(2);
        LevelsLeft.Push(1);
    }

    
    public void MainMenuState()
    {
        GameState = GameStates.MainMenuMode;
        if ( GameState == GameStates.MainMenuMode )
        {
            LoadAScene(0);
        }
    }
    public void PlayGameState(int scene)
    {
        GameState = GameStates.PlayGameMode;
        if (GameState == GameStates.PlayGameMode)
        {
            LoadAScene(scene);
        } 
    }
    public void RepeatPreviousLevel()
    {
        GameState = GameStates.PlayGameMode;
        if (GameState == GameStates.PlayGameMode )
        {
            SceneManager.LoadScene((int)(LevelsLeft.Peek()));
        }
    }

    public void GameOverState()
    {
        GameState = GameStates.GameOverMode;
        if (GameState == GameStates.GameOverMode)
        {
            LoadAScene(3);
        }
    }

    public void LoadNextlevel()
    {
        NextLevelIndex++;
        LoadAScene(NextLevelIndex);
    }

    public void LoadAScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void DecreaseAmountOfLevelsToPlay()
    {
        LevelsLeft.Pop();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
