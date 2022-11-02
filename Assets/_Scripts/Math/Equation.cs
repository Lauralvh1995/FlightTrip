using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Equation : ICloneable
{
    
    //the operator refers to the type of the equation, e.g. Addition, Subtraction, Multiplication or Division
    public enum Operator { Add, Subtract, Multiply, Divide}

    //public enum BarekaTopic { AdditionTill10, SubtractionTill10, AdditionTill20, SubtractionTill20, BuildingBlocksTill100, MultiplicationTillMax, DivisionTillMax, MultiplicationOfNumber, DivisionOfNumber }

    //the firstNumber and secondNumber are calculated together to form the answerNumber
    public int firstNumber { get; protected set; }
    public int secondNumber { get; protected set; }

    public Operator op;

    public int GivenAnswer { get; set; }

    public abstract void GenerateEquation();

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
                    return firstNumber / secondNumber;
                }
            default:
                {
                    return 0;
                }
        }
    }

    public abstract int GetSimilarAnswer();

    //get a fake answer whose value is similar to the correct answer by a randomized margin between 1 and 2
   
    //public int GetSimilarAnswerAdditionSubtraction()
    //{
    //    //first get the correct answer
    //    int correctAnswer = GetCorrectAnswer();

    //    //the correct answer will later be incremented or decremented by this randomized value
    //    int randomValue = Random.Range(1, 3);
    //    int maxSum = 0;

    //    //first check if answer is below/equal to 10
    //    if (correctAnswer <= 10)
    //    {
    //        //the fake answer cannot go above 10
    //        maxSum = 10;
    //    }
    //    //answer goes above 10, so...
    //    else
    //    {
    //        //the fake answer cannot go above 20
    //        maxSum = 20;
    //    }
    //    //make sure the fake answer's decrease can't go lower than 1
    //    if (correctAnswer - randomValue < 1)
    //    {
    //        return correctAnswer + randomValue;
    //    }
    //    //also make sure the fake answer's increase cannot be higher than the maxSum
    //    else if (correctAnswer + randomValue >= maxSum)
    //    {
    //        return correctAnswer - randomValue;
    //    }
    //    //if both conditions are fine, 50% chance the answer will be increased or decreased by the random value, return fake answer
    //    else
    //    {
    //        if (Random.value < 0.5f)
    //        {
    //            return correctAnswer + randomValue;
    //        }
    //        else
    //        {
    //            return correctAnswer - randomValue;
    //        }
    //    }
    //}

    //public int GetSimilarAnswerTables()
    //{
    //    //first get the correct answer
    //    int correctAnswer = GetCorrectAnswer();

    //    //the correct answer will be incremented/decremented with its table number multiplied by 1 or 2
    //    int randomMultipliedValue = Random.Range(1, 3);

    //    //the fake answer cannot go above tables of 10, unless the firstNumber is above 10
    //    int maxFakeAnswer = 0;
    //    if (firstNumber <= 10)
    //    {
    //        maxFakeAnswer = firstNumber * 10;
    //    }
    //    else
    //    {
    //        maxFakeAnswer = firstNumber * firstNumber;
    //    }

    //    //the fake answer is not allowed to go above tables of 10 or higher
    //    if (correctAnswer + (firstNumber * randomMultipliedValue) > maxFakeAnswer)
    //    {
    //        return correctAnswer - firstNumber * randomMultipliedValue;   
    //    }
    //    //the fake answer is not allowed to go below or equal to 0
    //    else if (correctAnswer - (firstNumber * randomMultipliedValue) <= 0)
    //    {
    //        return correctAnswer + (firstNumber * randomMultipliedValue);
    //    }
    //    //50% chance to increment or decrement the correct answer with the table number multiplied by 1 or 2, return fake table answer
    //    else
    //    {
    //        if (Random.value < 0.5f)
    //        {
    //            return correctAnswer + (firstNumber * randomMultipliedValue);
    //        }
    //        else
    //        {    
    //            return correctAnswer - (firstNumber * randomMultipliedValue);
    //        }
    //    }
    //}


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
