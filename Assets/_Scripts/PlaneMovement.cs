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
            Vector3 localPos = new Vector3(pos.x * verticalBounds * screenAspect,
                pos.y * verticalBounds * invert,
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
