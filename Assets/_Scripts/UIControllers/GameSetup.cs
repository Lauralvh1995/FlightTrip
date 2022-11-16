using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour
{
    [SerializeField]
    List<int> selectedTables;

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
                Session session = new Session(10, selectedTables, Operator.Multiply, 4);

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
                thinkingTime = 7;
                break;
            case Difficulty.NORMAL:
                thinkingTime = 4;
                break;
            case Difficulty.HARD:
                thinkingTime = 1;
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
