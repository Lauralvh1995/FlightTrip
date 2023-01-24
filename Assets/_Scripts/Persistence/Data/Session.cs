using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Session
{
    [SerializeField]
    string sessionID;
    [SerializeField]
    long date; //date in UnixTimeSeconds since 1-1-1970
    [SerializeField]
    int numberOfEquations = 10;
    [SerializeField]
    private List<int> bases;
    [SerializeField]
    private Operator op;
    [SerializeField]
    private int thinkingTime;
    [SerializeField]
    List<ScoreEntry> scoreEntries;
    [SerializeField]
    List<TrickScore> trickScores;

    public int NumberOfEquations { get => numberOfEquations; set => numberOfEquations = value; }
    public List<int> Bases { get => bases; set => bases = value; }
    public Operator Op { get => op; set => op = value; }
    public int ThinkingTime { get => thinkingTime; set => thinkingTime = value; }

    public Session(int numberOfEquations, List<int> bases, Operator op, int thinkingTime)
    {
        this.sessionID = Guid.NewGuid().ToString();
        this.date = GetCurrentDateInEpoch();
        this.numberOfEquations = numberOfEquations;
        this.bases = bases;
        this.op = op;
        this.thinkingTime = thinkingTime;
    }

    //Default data the game will load with if there is no data to load
    public Session()
    {
        this.sessionID = Guid.NewGuid().ToString();
        this.date = GetCurrentDateInEpoch();
        numberOfEquations = 10;
        //start with bases 1, 2, 5 and 10
        bases = new List<int>();
        bases.Add(1);
        bases.Add(2);
        bases.Add(5);
        bases.Add(10);
        op = Operator.Multiply;
        thinkingTime = 4;
    }

    public void AddEntry(ScoreEntry entry)
    {
        scoreEntries.Add(entry);
    }
    public void AddTrickEntry(TrickScore entry)
    {
        trickScores.Add(entry);
    }

    public int GetScore()
    {
        int score = 0;
        foreach(ScoreEntry entry in scoreEntries)
        {
            score += entry.GetScore();
        }
        foreach(TrickScore trickScore in trickScores)
        {
            score += trickScore.score;
        }
        return score;
    }

    public long GetCurrentDateInEpoch()
    {
        DateTimeOffset dto = DateTimeOffset.Now;
        return dto.ToUnixTimeSeconds();
    }

    public string ConvertEpochToHumanReadableDate()
    {
        DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(date);
        return TimeZoneInfo.ConvertTime(dto, TimeZoneInfo.Local).ToString();
    }
}
