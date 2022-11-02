using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class MultiplicationTables : Equation
{
    public List<int> selectedNumbers;

    public void UpdateListSelectedNumbers(List<int> selectedNumbers)
    {
        this.selectedNumbers = selectedNumbers;
    }

    public override void GenerateEquation()
    {
        int randomIndex = Random.Range(0, selectedNumbers.Count);
        firstNumber = selectedNumbers[randomIndex];

        if(firstNumber <= 10)
        {
            secondNumber = Random.Range(1, 11);
        }
        else
        {
            secondNumber = Random.Range(1, firstNumber + 1);
        }

        op = Operator.Multiply;
    }

    public override int GetSimilarAnswer()
    {
        //first get the correct answer
        int correctAnswer = GetCorrectAnswer();

        //the correct answer will be incremented/decremented with its table number multiplied by 1 or 2
        int randomMultipliedValue = Random.Range(1, 3);

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
            if (Random.value < 0.5f)
            {
                return correctAnswer + (firstNumber * randomMultipliedValue);
            }
            else
            {
                return correctAnswer - (firstNumber * randomMultipliedValue);
            }
        }
    }
}
