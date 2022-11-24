using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreEntry
{
    public Equation equation;
    public int answer;
    public float accuracy;

    public ScoreEntry(Equation equation, int answer, float accuracy)
    {
        this.equation = equation;
        this.answer = answer;
        this.accuracy = accuracy;
    }

    public int GetScore()
    {
        //you always get 10 points
        int score = 10;

        //get 10 points for correct answer
        if (equation.GetCorrectAnswer() == answer)
            score += 10;

        //get 10 points for going through the ring
        //get 0 to 9 extra points for going through the ring other than the center
        //get 10 extra points for going through the center
        score += Mathf.FloorToInt(accuracy * 10);

        return score;
    }
}
