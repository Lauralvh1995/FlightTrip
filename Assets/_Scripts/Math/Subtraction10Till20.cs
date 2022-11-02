using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subtraction10Till20 : Equation
{
    public override void GenerateEquation()
    {
        //first number must be between 0 and 10
        firstNumber = Random.Range(11, 21);

        //second number must be lower than the first number to prevent negative value answer, second number will also never become zero for now (too easy)
        secondNumber = Random.Range(1, firstNumber);
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