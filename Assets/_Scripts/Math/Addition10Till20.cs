using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addition10Till20 : Equation
{
    public override void GenerateEquation()
    {
        //first number must be between 1 and 19
        firstNumber = Random.Range(1, 20);

        //the sum of the first number and the second number must be between 10 and 20
        secondNumber = GenerateSecondNumber(firstNumber);


        op = Operator.Add;
    }

    //implementation of a sum that ranges between 10 and 20
    public int GenerateSecondNumber(int firstNumber)
    {

        int secondNumber = Random.Range(1, 20);

        bool SumEqualsBetween10And20 = false;

        while (!SumEqualsBetween10And20)
        {
            //count the sum
            int sum = firstNumber + secondNumber;

            //the sum is not between 10 and 20
            if (sum < 10 || sum > 20)
            {
                //re-randomize the second number, and run the while loop again
                secondNumber = Random.Range(1, 20);
            }
            //the sum is between 10 and 20, kill the while loop 
            else
            {
                SumEqualsBetween10And20 = true;
            }
        }
        return secondNumber;
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
        //also make sure the fake answer's increase cannot be higher than 20
        else if (correctAnswer + randomValue >= 20)
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
