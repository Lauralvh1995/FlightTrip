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

    public int GivenAnswer { get; set; }

    public void GenerateEquation(List<int> bases, Operator op)
    {
        this.op = op;
        int randomIndex = UnityEngine.Random.Range(0, bases.Count);
        firstNumber = UnityEngine.Random.Range(1, 11);
        secondNumber = bases[randomIndex];
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
        //first get the correct answer
        int correctAnswer = GetCorrectAnswer();

        //the correct answer will be incremented/decremented with its table number multiplied by 1 or 2
        int randomMultipliedValue = UnityEngine.Random.Range(1, 3);

        //the fake answer cannot go above tables of 10, unless the firstNumber is above 10
        int maxFakeAnswer = 0;
        if (firstNumber <= 10)
        {
            maxFakeAnswer = firstNumber * 10;
        }
        else
        {
            maxFakeAnswer = firstNumber * firstNumber;
        }

        //the fake answer is not allowed to go above tables of 10
        if (correctAnswer + (firstNumber * randomMultipliedValue) > maxFakeAnswer)
        {
            return correctAnswer - firstNumber * randomMultipliedValue;
        }
        //the fake answer is not allowed to go below or equal to 0
        else if (correctAnswer - (firstNumber * randomMultipliedValue) <= 0)
        {
            return correctAnswer + (firstNumber * randomMultipliedValue);
        }
        //50% chance to increment or decrement the correct answer with the table number multiplied by 1 or 2, return fake table answer
        else
        {
            if (UnityEngine.Random.value < 0.5f)
            {
                return correctAnswer + (firstNumber * randomMultipliedValue);
            }
            else
            {
                return correctAnswer - (firstNumber * randomMultipliedValue);
            }
        }
    }

    public bool isCorrect()
    {
        if(GivenAnswer == GetCorrectAnswer())
        {
            return true;
        }
        else
        {
            return false;
        }
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
}
