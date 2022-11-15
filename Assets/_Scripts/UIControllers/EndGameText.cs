using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI gameOverText;
    [SerializeField]
    TextMeshProUGUI totalScoreText;
    public void GameEnded(int score)
    {
        totalScoreText.text = "Total Score \n \n" + score;
        gameOverText.gameObject.SetActive(true);
    }
}
