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

    public int NumberOfEquations { get => numberOfEquations; set => numberOfEquations = value; }
    public List<int> Bases { get => bases; set => bases = value; }
    public Operator Op { get => op; set => op = value; }

    public Session(int numberOfEquations, List<int> bases, Operator op)
    {
        this.numberOfEquations = numberOfEquations;
        this.bases = bases;
        this.op = op;
    }
}
