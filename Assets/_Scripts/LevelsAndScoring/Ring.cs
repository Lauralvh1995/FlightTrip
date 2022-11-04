using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ring : MonoBehaviour
{
    [SerializeField]
    TextMeshPro answerText;
    [SerializeField]
    int answer;
    public new Renderer renderer;

    public int Answer { get => answer; set => answer = value; }

    public void SetGraphics(int answer)
    {
        this.answer = answer;
        answerText.text = answer.ToString();
    }
}
