using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    int averageBufferSize = 10;
    [SerializeField]
    List<Vector2> inputs;
    [SerializeField]
    UnityEvent<Vector2> sendCleanedInput;

    public List<Vector2> Inputs { get => inputs; set => inputs = value; }

    public void OnInputGiven(Vector2 input)
    {
        inputs.Add(input);
        CalculateMovingAverage();
    }
    private void CalculateMovingAverage()
    {
        //take average of the last X inputs, or all inputs if less than X
        Vector2[] lastInputs = new Vector2[averageBufferSize];

        if (inputs.Count <= averageBufferSize)
        {
            inputs.CopyTo(lastInputs);
        }
        else
        {
            inputs.GetRange(inputs.Count - (averageBufferSize + 1), averageBufferSize).CopyTo(lastInputs);
        }
        float averageX = 0f;
        float averageY = 0f;
        foreach (Vector2 inputinfo in lastInputs)
        {
            averageX += inputinfo.x;
            averageY += inputinfo.y;
        }
        averageX /= (float)averageBufferSize;
        averageY /= (float)averageBufferSize;

        Vector2 averageInput = new Vector2(averageX, averageY);

        //send out average
        sendCleanedInput.Invoke(averageInput);
    }
}
