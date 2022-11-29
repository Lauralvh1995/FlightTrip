using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    Player player;

    [SerializeField]
    List<int> selectedTables;

    [SerializeField]
    int thinkingTimeEasy = 6;
    [SerializeField]
    int thinkingTimeMedium = 3;
    [SerializeField]
    int thinkingTimeHard = 0;
    private int thinkingTime = -1;

    private void Awake()
    {
        selectedTables = new List<int>();
    }

    public void AddBase(int baseNumber)
    {
        selectedTables.Add(baseNumber);
    }
    public void RemoveBase(int baseNumber)
    {
        selectedTables.Remove(baseNumber);
    }

    public void StartGame()
    {
        if (selectedTables.Count > 0)
        {
            if (thinkingTime != -1)
            {
                DataPersistanceManager.instance.SaveGame();
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            }
            else
            {
                thinkingTime = thinkingTimeMedium;
                DataPersistanceManager.instance.SaveGame();
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            }
        }
        else
        {
            Debug.LogWarning("No tables set! Can't start game!");
        }
    }

    public void ChangeDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.EASY:
                thinkingTime = thinkingTimeEasy;
                break;
            case Difficulty.NORMAL:
                thinkingTime = thinkingTimeMedium;
                break;
            case Difficulty.HARD:
                thinkingTime = thinkingTimeHard;
                break;
        }
    }

    public void LoadData(GameData gameData)
    {
        this.player.SetPlayerData(gameData);
    }

    public void SaveData(GameData gameData)
    {
        gameData.latestSession = new Session(10, selectedTables, Operator.Multiply, thinkingTime);
    }
}

public enum Difficulty
{
    EASY,
    NORMAL,
    HARD
}
