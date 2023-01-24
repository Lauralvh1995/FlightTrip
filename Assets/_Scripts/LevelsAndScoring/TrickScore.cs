using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrickScore
{
    public string TrickName;
    public int score;

    public TrickScore(string trickName, int score)
    {
        TrickName = trickName;
        this.score = score;
    }
}
