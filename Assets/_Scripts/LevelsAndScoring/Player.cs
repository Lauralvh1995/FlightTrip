using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField]
    List<Ring> rings;
    [SerializeField]
    int score = 0;
    [SerializeField]
    Equation currentEquation;
    [SerializeField]
    TextMeshPro equationText;
    [SerializeField]
    Transform plane;

    [SerializeField]
    TextMeshProUGUI scoreUI;
    [SerializeField]
    UnityEvent<int> passScore;

    public Transform Plane { get => plane; set => plane = value; }

    public void SetPlayerUI(List<Ring> rings, Equation currentEquation)
    {
        this.currentEquation = currentEquation;
        this.equationText.text = this.currentEquation.GenerateEquationToString();
        for (int i = 0; i < rings.Count; i++)
        {
            this.rings[i].transform.localPosition = rings[i].transform.localPosition;
            this.rings[i].Answer = rings[i].Answer;
            this.rings[i].SetGraphics(this.rings[i].Answer);
        }
    }
    public void ShowRings()
    {
        foreach (Ring ring in this.rings)
        {
            ring.gameObject.SetActive(true);
        }
    }
    public void ShowEquation()
    {
        equationText.gameObject.SetActive(true);
    }
    public void HideRings()
    {
        foreach (Ring ring in this.rings)
        {
            ring.gameObject.SetActive(false);
        }
    }
    public void HideEquation()
    {
        equationText.gameObject.SetActive(false);
    }
    public void AddScore(int score)
    {
        this.score += score;
        //set UI stuff
        scoreUI.text = "SCORE " + this.score;
    }

    public void EndGame()
    {
        passScore.Invoke(score);
    }
}
