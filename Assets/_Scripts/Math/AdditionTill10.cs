using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionTill10 : Equation
{
    public override void GenerateEquation()
    {
        //first number must be between 1 and 9
        firstNumber = Random.Range(1, 10);

        //the sum of the first number and the second number must not be more than 10
        secondNumber = Random.Range(1, 11 - firstNumber);
        op = Operator.Add;

        //different implementation
        //secondNumber = GenerateSecondNumber(firstNumber);
    }
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
        //also make sure the fake answer's increase cannot be higher than 10
        else if (correctAnswer + randomValue >= 10)
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

    //public int GenerateSecondNumber(int firstNumber)
    //{
    //    int secondNumber = Random.Range(1, 10);

    //    bool SumEqualsTenOrLess = false;

    //    while (!SumEqualsTenOrLess)
    //    {
    //        //count the sum
    //        int sum = firstNumber + secondNumber;

    //        //the sum is more than 10
    //        if (sum > 10)
    //        {
    //            //randomize the second number again
    //            secondNumber = Random.Range(1, 10);
    //        }
    //        //the sum is not more than 10
    //        else
    //        {
    //            SumEqualsTenOrLess = true;
    //        }
    //    }
    //    return secondNumber;
    //}

    //get a fake answer whose value is similar to the correct answer by a randomized margin between 1 and 2


}
