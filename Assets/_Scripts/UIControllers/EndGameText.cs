using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI gameOverText;
    [SerializeField]
    TextMeshProUGUI totalScoreText;
    [SerializeField]
    string returnScene = "TableSelect";
    public void GameEnded(int score)
    {
        totalScoreText.text = score.ToString();
        gameOverText.gameObject.SetActive(true);
    }

    public void BackToTitleScreen()
    {
        DataPersistanceManager.instance.SaveGame();
        SceneManager.LoadScene(returnScene);
    }
}
