using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlocksTill100 : Equation
{
    public override void GenerateEquation()
    {
        //50% chance to generate an addition or subtraction equation
        if (Random.value > 0.5)
        {
            GenerateAddition();
        }
        else
        {
            GenerateSubtraction();
        }

    }

    private void GenerateAddition()
    {
        //first number must be between 10 and 100
        firstNumber = Random.Range(10, 100);

        //secondNumber cannot be allowed to make sum go above 100
        secondNumber = Random.Range(1, 101 - firstNumber);

        //REMOVE: second number must be the value of (first number - 10) or lower, but the sum must also not be more than 100
        //secondNumber = GenerateSecondNumberAddition(firstNumber);
        op = Operator.Add;
    }

    //public int GenerateSecondNumberAddition(int firstNumber)
    //{
    //    int secondNumber = Random.Range(1, firstNumber);

    //    bool SumEqualsHundredOrLess = false;

    //    while (!SumEqualsHundredOrLess)
    //    {
    //        int sum = firstNumber + secondNumber;
    //        if (sum > 100)
    //        {
    //            secondNumber = Random.Range(1, firstNumber);
    //        }
    //        else
    //        {
    //            SumEqualsHundredOrLess = true;
    //        }
    //    }
    //    return secondNumber;
    //}

    private void GenerateSubtraction()
    {
        //first number must be between 10 and 100
        firstNumber = Random.Range(10, 101);

        //second number must be the value of first number or lower, but the sum must also not be more than 100

        secondNumber = Random.Range(10, firstNumber);
        op = Operator.Subtract;
    }

    //get a fake answer whose value is similar to the correct answer by a randomized margin between 1 and 2
    public override int GetSimilarAnswer()
    {
        //first get the correct answer
        int correctAnswer = GetCorrectAnswer();

        //the correct answer will later be incremented or decremented by this randomized value
        int randomValue = Random.Range(1, 3);

        //make sure the fake answer's decrease can't go lower than 1
        if (correctAnswer - randomValue < 1)
        {
            return correctAnswer + randomValue;
        }
        //also make sure the fake answer's increase cannot be higher than 100
        else if (correctAnswer + randomValue >= 100)
        {
            return correctAnswer - randomValue;
        }
        //if both conditions are fine, 50% chance the answer will be increased or decreased by the random value, return fake answer
        else
        {
            if (Random.value < 0.5f)
            {
                return correctAnswer + randomValue;
            }
            else
            {
                return correctAnswer - randomValue;
            }
        }
    }
}
