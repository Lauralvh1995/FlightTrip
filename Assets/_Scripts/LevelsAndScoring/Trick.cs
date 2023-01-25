using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Trick", menuName = "Trick")]
public class Trick : ScriptableObject
{
    [SerializeField]
    float trickMargin = 0.01f;
    [SerializeField]
    AnimationClip animationClip;
    [SerializeField]
    int scoreValue;

    [SerializeField]
    List<Vector2> waypoints;

    public UnityEvent<Trick> doTrick;

    public void Evaluate(Vector2[] inputs)
    {
        int evaluationInterval = inputs.Length / waypoints.Count;
        int currentEvaluationIndex = 0;

        bool trickEvaluated = false;
        foreach(Vector2 waypoint in waypoints)
        {
            if(Vector2.Distance(inputs[currentEvaluationIndex], waypoint) <= trickMargin)
            {
                Debug.Log(inputs[currentEvaluationIndex]);
                trickEvaluated = true;
            }
            else 
            { 
                trickEvaluated = false; 
            }
            currentEvaluationIndex += evaluationInterval;
            if(currentEvaluationIndex >= inputs.Length)
            {
                currentEvaluationIndex = inputs.Length - 1;
            }
        }

        if (trickEvaluated)
        {
            doTrick.Invoke(this);
        }
        else
        {
            Debug.Log("Did not trigger " + animationClip.name);
        }
    }

    public AnimationClip GetAnimationClip()
    {
        return animationClip;
    }
    public int GetScore()
    {
        return scoreValue;
    }
}
