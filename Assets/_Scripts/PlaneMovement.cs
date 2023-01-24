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
    [SerializeField]
    bool allowedToDoTrick = true;
    //inputs are updated on FixedUpdate, which is polled every 0.02 seconds, so 50 inputs in a second

    [SerializeField]
    UnityEvent<AnimationClip> doTrick;
    [SerializeField]
    UnityEvent<string, int> addTrickScore;
    [SerializeField]
    List<Trick> possibleTricks;
    //add other event triggers for tricks here

    private void Awake()
    {
        screenAspect = Camera.main.aspect;
        horizontalBounds = verticalBounds * screenAspect;
        inputBuffer = new List<Vector2>();
        foreach(Trick trick in possibleTricks)
        {
            trick.doTrick.AddListener(DoTrick);
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
            if(inputBuffer.Count > trickInterval)
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
        if (allowedToDoTrick)
        {
            foreach (Trick trick in possibleTricks)
            {
                trick.Evaluate(trickBuffer);
            }
        }
        /*
        //check x-value of the first, middle and last elements for barrel rolls
        float dXFirstHalf = trickBuffer[trickBuffer.Length / 2 + 1].x - trickBuffer[0].x;
        float dXSecondHalf = trickBuffer[trickBuffer.Length - 1].x - trickBuffer[trickBuffer.Length / 2 + 1].x;
        float trickMovementThreshold = 1.5f;
        if ( dXFirstHalf > trickMovementThreshold && dXSecondHalf < -trickMovementThreshold)
        {
            //DO A BARREL ROLL TO THE RIGHT
            Debug.Log("DO A BARREL ROLL TO THE RIGHT!");
            doABarrelRoll.Invoke(false);
            return;
        }
        if(dXFirstHalf < -trickMovementThreshold && dXSecondHalf > trickMovementThreshold)
        {
            //DO A BARREL ROLL TO THE LEFT
            Debug.Log("DO A BARREL ROLL TO THE LEFT!");
            doABarrelRoll.Invoke(true);
            return;
        }
        */
    }
    public void DoTrick(Trick trick)
    {
        doTrick.Invoke(trick.GetAnimationClip());
        addTrickScore.Invoke(trick.name, trick.GetScore());
        allowedToDoTrick = false;
        Invoke("EnableTricksAgain", trick.GetAnimationClip().length); //Cooldown is equal to the length of the animation
    }

    private void EnableTricksAgain()
    {
        allowedToDoTrick = true;
    }
}
