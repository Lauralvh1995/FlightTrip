using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DivisionTables : Equation
{
    public List<int> selectedNumbers { get; private set; }

    public void UpdateListSelectedNumbers(List<int> selectedNumbers)
    {
        this.selectedNumbers = selectedNumbers;
    }

    public override void GenerateEquation()
    {
        //if the list contains a table number higher than 10, divide by that number too
        if(selectedNumbers.Max() <= 10)
        {
            secondNumber = Random.Range(1, 11);
        }
        else
        {
            secondNumber = Random.Range(1, selectedNumbers.Max() + 1);
        }
        
        //randomly select one of the numbers
        int randomIndex = Random.Range(0, selectedNumbers.Count);
        
        //the firstNumber becomes the top part of the fraction
        firstNumber = selectedNumbers[randomIndex] * secondNumber;

        op = Operator.Divide;
    }

    public override int GetSimilarAnswer()
    {
        //first get the correct answer
        int correctAnswer = GetCorrectAnswer();

        //the correct answer will be incremented/decremented with its table number multiplied by 1 or 2
        int randomMultipliedValue = Random.Range(1, 3);

        //the fake answer cannot go above tables of 10, unless the firstNumber is above 10
        int maxFakeAnswer = 0;
        if (secondNumber <= 10)
        {
            maxFakeAnswer = secondNumber * 10;
        }
        else
        {
            maxFakeAnswer = secondNumber * secondNumber;
        }

        //the fake answer is not allowed to go above tables of 10 or higher
        if (correctAnswer + (secondNumber * randomMultipliedValue) > maxFakeAnswer)
        {
            return correctAnswer - secondNumber * randomMultipliedValue;
        }
        //the fake answer is not allowed to go below or equal to 0
        else if (correctAnswer - (secondNumber * randomMultipliedValue) <= 0)
        {
            return correctAnswer + (secondNumber * randomMultipliedValue);
        }
        //50% chance to increment or decrement the correct answer with the table number multiplied by 1 or 2, return fake table answer
        else
        {
            if (Random.value < 0.5f)
            {
                return correctAnswer + (secondNumber * randomMultipliedValue);
            }
            else
            {
                return correctAnswer - (secondNumber * randomMultipliedValue);
            }
        }
    }
}
