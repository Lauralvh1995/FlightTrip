using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreUI;

    // Start is called before the first frame update
    void Start()
    {
        scoreUI = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScoreUI(int score)
    {
        scoreUI.text = "SCORE " + score;
    }
}
