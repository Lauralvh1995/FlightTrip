using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField]
    private float screenAspect;
    [SerializeField]
    private float speedMultiplier = 10f;
    [SerializeField]
    private bool invertYAxis = false;

    [SerializeField]
    private float verticalBounds = 3f;
    //TODO: figure out a way to make this value adaptive too
    [SerializeField]
    private float horizontalBounds;

    private bool movementDisabled = false;

    [SerializeField]
    List<Vector2> inputBuffer;
    [SerializeField]
    int trickInterval = 25;
    //inputs are updated on FixedUpdate, which is polled every 0.02 seconds, so 50 inputs in a second

    [SerializeField]
    UnityEvent<bool> doABarrelRoll;
    //add other event triggers for tricks here

    private void Awake()
    {
        screenAspect = Camera.main.aspect;
        horizontalBounds = verticalBounds * screenAspect;
        inputBuffer = new List<Vector2>();
    }

    private void Update()
    {
        //for debugging only
        if (Input.GetKeyDown(KeyCode.Space))
        {
            doABarrelRoll.Invoke(false);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            doABarrelRoll.Invoke(true);
        }
    }

    public void UpdatePosition(Vector2 pos)
    {
        if (!movementDisabled)
        {
            //add input to buffer
            inputBuffer.Add(pos);
            //calculate screen position
            int invert = invertYAxis ? -1 : 1;
            Vector3 localPos = new Vector3(pos.x * speedMultiplier * screenAspect,
                pos.y * speedMultiplier * invert,
                transform.localPosition.z);


            transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, -horizontalBounds, horizontalBounds)
                , Mathf.Clamp(localPos.y, -verticalBounds, verticalBounds)
                , transform.localPosition.z);

            //check for trick each interval 
            if(inputBuffer.Count % trickInterval == 0 && inputBuffer.Count > 0)
                CheckTrick();
        }
    }

    public void DisableMovement()
    {
        movementDisabled = true;
    }

    private void CheckTrick()
    {
        //copy the last X elements in the inputBuffer, where X is the trickInterval
        Vector2[] trickBuffer = new Vector2[trickInterval];
        if (inputBuffer.Count == trickInterval)
            inputBuffer.CopyTo(trickBuffer);
        else
            inputBuffer.GetRange(inputBuffer.Count - trickInterval - 1, trickInterval).CopyTo(trickBuffer);

        //check x-value of the first, middle and last elements for barrel rolls
        if(trickBuffer[0].x == -1 && trickBuffer[trickBuffer.Length / 2 + 1].x == 1 && trickBuffer[trickBuffer.Length - 1].x == -1)
        {
            //DO A BARREL ROLL TO THE RIGHT
            Debug.Log("DO A BARREL ROLL TO THE RIGHT!");
            doABarrelRoll.Invoke(false);
            return;
        }
        if(trickBuffer[0].x == 1 && trickBuffer[trickBuffer.Length / 2 + 1].x == -1 && trickBuffer[trickBuffer.Length - 1].x == 1)
        {
            //DO A BARREL ROLL TO THE LEFT
            Debug.Log("DO A BARREL ROLL TO THE LEFT!");
            doABarrelRoll.Invoke(true);
            return;
        }
    }
}
