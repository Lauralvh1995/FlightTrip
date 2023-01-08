using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Equation : ICloneable
{
    //the firstNumber and secondNumber are calculated together to form the answerNumber
    [SerializeField]
    public int firstNumber;
    [SerializeField]
    public Operator op;
    [SerializeField]
    public int secondNumber;

    List<int> generatedAnswers;

    public void GenerateEquation(List<int> bases, Operator op, Equation previous)
    {
        this.op = op;
        int randomIndex = UnityEngine.Random.Range(0, bases.Count);
        firstNumber = UnityEngine.Random.Range(1, 11);
        secondNumber = bases[randomIndex];
        if (previous != null)
        {
            while (this.Equals(previous))
            {
                firstNumber = UnityEngine.Random.Range(1, 11);
                secondNumber = bases[randomIndex];
            }
        }
        GeneratePreset();
    }

    public void GeneratePreset()
    {
        generatedAnswers = new List<int>();
        if (firstNumber == 1)
        {
            generatedAnswers.Add(2 * secondNumber);
            generatedAnswers.Add(3 * secondNumber);
            generatedAnswers.Add(4 * secondNumber);
            generatedAnswers.Add(5 * secondNumber);
        }
        else if (firstNumber == 2)
        {
            generatedAnswers.Add(1 * secondNumber);
            generatedAnswers.Add(3 * secondNumber);
            generatedAnswers.Add(4 * secondNumber);
            generatedAnswers.Add(5 * secondNumber);
        }
        else if (firstNumber == 9)
        {
            generatedAnswers.Add(10 * secondNumber);
            generatedAnswers.Add(8 * secondNumber);
            generatedAnswers.Add(7 * secondNumber);
            generatedAnswers.Add(6 * secondNumber);
        }
        else if (firstNumber == 10)
        {
            generatedAnswers.Add(9 * secondNumber);
            generatedAnswers.Add(8 * secondNumber);
            generatedAnswers.Add(7 * secondNumber);
            generatedAnswers.Add(6 * secondNumber);
        }
        else
        {
            generatedAnswers.Add((firstNumber - 2) * secondNumber);
            generatedAnswers.Add((firstNumber - 1) * secondNumber);
            generatedAnswers.Add((firstNumber + 2) * secondNumber);
            generatedAnswers.Add((firstNumber + 1) * secondNumber);
        }
    }
    public int GetCorrectAnswer()
    {
        switch (op)
        {
            case Operator.Add:
                {
                    return firstNumber + secondNumber;
                }
            case Operator.Subtract:
                {
                    return firstNumber - secondNumber;
                }
            case Operator.Multiply:
                {
                    return firstNumber * secondNumber;
                }
            case Operator.Divide:
                {
                    if (secondNumber != 0)
                        return firstNumber / secondNumber;
                    else
                        throw new DivideByZeroException("You can't divide by 0!");
                }
            default:
                {
                    return 0;
                }
        }
    }

    public int GetSimilarAnswer()
    {
        int similar = generatedAnswers[UnityEngine.Random.Range(0, generatedAnswers.Count)];
        foreach (int i in new List<int>(generatedAnswers))
        {
            if (similar == i)
            {
                generatedAnswers.Remove(similar);
            }
        }
        return similar;
    }
    public string GenerateEquationToString()
    {
        switch (op)
        {
            case Operator.Add:
                {
                    return firstNumber.ToString() + " + " + secondNumber.ToString() + " = ";
                }
            case Operator.Subtract:
                {
                    return firstNumber.ToString() + " - " + secondNumber.ToString() + " = ";
                }
            case Operator.Multiply:
                {
                    return firstNumber.ToString() + " x " + secondNumber.ToString() + " = ";
                }
            case Operator.Divide:
                {
                    return firstNumber.ToString() + " / " + secondNumber.ToString() + " = ";
                }
            default:
                {
                    return "Iets is fout gegaan";
                }
        }
    }


    public object Clone()
    {
        return MemberwiseClone();
    }

    public override bool Equals(object obj)
    {
        Equation other = obj as Equation;
        return (this.firstNumber == other.firstNumber) && (this.op == other.op) && (this.secondNumber == other.secondNumber);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
