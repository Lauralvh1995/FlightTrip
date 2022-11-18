using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour
{
    [SerializeField]
    List<int> selectedTables;

    [SerializeField]
    int thinkingTimeEasy = 6;
    [SerializeField]
    int thinkingTimeMedium = 3;
    [SerializeField]
    int thinkingTimeHard = 0;
    private int thinkingTime;

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
            if (thinkingTime != 0)
            {
                Session session = new Session(10, selectedTables, Operator.Multiply, thinkingTime);

                DataSaver.saveData(session, "sessionInfo");
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            }
            else
            {
                Session session = new Session(10, selectedTables, Operator.Multiply, thinkingTimeMedium);

                DataSaver.saveData(session, "sessionInfo");
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
}

public enum Difficulty
{
    EASY,
    NORMAL,
    HARD
}
