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
    public void GameEnded(int score)
    {
        totalScoreText.text = "Eindscore \n \n" + score;
        gameOverText.gameObject.SetActive(true);
    }

    public void BackToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
