using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI UIText;
    [SerializeField]
    private TextMeshProUGUI AddText;

    private float addTextDuration = 1;

    public int currentScore { get; private set; }

    private void Awake()
    {
        StartCoroutine(CountUp());
    }

    public void AddScore(int amount)
    {
        currentScore += amount;

        StartCoroutine(ShowAddition(amount));
    }

    private IEnumerator ShowAddition(int amount)
    {
        AddText.gameObject.SetActive(true);
        AddText.text = "+" + amount.ToString();

        float currentDuration = 0;

        while(currentDuration < addTextDuration)
        {
            currentDuration += Time.deltaTime;
            yield return null;
        }

        AddText.gameObject.SetActive(false);
    }

    private IEnumerator CountUp()
    {
        float displayScore = 0;

        //shouldnt be a while(true) but this is a quick fix for now
        while(true)
        {
            while (displayScore < currentScore)
            {
                displayScore++;
                UIText.text = displayScore.ToString();
                yield return null;
            }
            yield return null;
        }
    }
}
