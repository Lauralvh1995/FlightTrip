using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Session
{
    [SerializeField]
    int numberOfEquations = 10;
    [SerializeField]
    private List<int> bases;
    [SerializeField]
    private Operator op;
    [SerializeField]
    private int thinkingTime;

    public int NumberOfEquations { get => numberOfEquations; set => numberOfEquations = value; }
    public List<int> Bases { get => bases; set => bases = value; }
    public Operator Op { get => op; set => op = value; }
    public int ThinkingTime { get => thinkingTime; set => thinkingTime = value; }

    public Session(int numberOfEquations, List<int> bases, Operator op, int thinkingTime)
    {
        this.numberOfEquations = numberOfEquations;
        this.bases = bases;
        this.op = op;
        this.thinkingTime = thinkingTime;
    }
}
