using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquationPoint : MonoBehaviour
{
    [SerializeField]
    Equation equation;
    [SerializeField]
    List<Ring> rings;
    [SerializeField]
    Transform ringOrigin;
    [SerializeField]
    Player player;
    [SerializeField]
    float ringRadius = 1f;
    [SerializeField]
    float ringInnerRadius = 0.4f;
    [SerializeField]
    float planeMargin = 0.5f;

    [SerializeField]
    private List<RingAnchor> ringAnchors;
    [SerializeField]
    private int nextPointIndex;
    public int NextPointIndex { get => nextPointIndex; set => nextPointIndex = value; }

    public UnityEvent<List<Ring>, Equation> OnRingsSetup;
    public UnityEvent<int> setupNext;
    public UnityEvent<int> sendScore;

    public void SetBasesAndGenerateEquation(List<int> bases, Operator op, Player player)
    {
        equation.GenerateEquation(bases, op);
        Debug.Log(equation.firstNumber + equation.op + equation.secondNumber);
        this.player = player;
        //Spawn Rings
        SetUpRings();
    }

    public void SetUpRings()
    {
        //Setup each ring position
        for (int i = 0; i < ringAnchors.Count; i++)
        {
            ringAnchors[i].Ring = rings[i];
            rings[i].transform.localPosition = new Vector3(
                Random.Range(-1f * ringAnchors[i].Bounds.x + 0.5f,
                    ringAnchors[i].Bounds.x - 0.5f),
                Random.Range(-1f * ringAnchors[i].Bounds.y + 0.5f,
                    ringAnchors[i].Bounds.y - 0.5f),
                0);
            rings[i].SetGraphics(equation.GetSimilarAnswer());
            rings[i].gameObject.SetActive(true);
        }
        //Assign correct answer to a random ring
        int randomRingIndex = Random.Range(0, rings.Count);
        rings[randomRingIndex].SetGraphics(equation.GetCorrectAnswer());
        OnRingsSetup.Invoke(rings, equation);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + "was hit by: " + other.name);
        EnterAnswer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 3f);
    }

    private void EnterAnswer()
    {
        //Get the closest ring anchor. This determines the answer, and the score
        RingAnchor closestAnchor = ringAnchors[0];
        foreach(RingAnchor anchor in ringAnchors)
        {
            if(Vector3.Distance(anchor.transform.localPosition, player.Plane.localPosition) <
                Vector3.Distance(closestAnchor.transform.localPosition, player.Plane.localPosition))
            {
                closestAnchor = anchor;
            }
        }

        //Scoring
        int score = 10; //base score for everything
        int correctAnswer = equation.GetCorrectAnswer();

        //check answer
        if (closestAnchor.Ring.Answer == correctAnswer)
        {
            Debug.Log("Correct answer entered");
            score += 10;
            //Correct
        }
        else
        {
            Debug.Log("Incorrect answer entered. The correct answer was " + correctAnswer);
            //Incorrect
        }

        //Points for ring accuracy
        float distancePlaneToRingCenter = Vector3.Distance(player.Plane.position, closestAnchor.Ring.transform.position);
        Debug.Log("Distance from plane to ring center: " + distancePlaneToRingCenter);
        //If plane hits ring at all
        if(distancePlaneToRingCenter < ringRadius + planeMargin)
        {
            score += 10;
            //If plane goes through the center
            if (distancePlaneToRingCenter < ringInnerRadius + planeMargin)
            {
                score += 10;
            }
            //add a fraction of 10 for accuracy
            else
            {
                score += Mathf.FloorToInt(10f * (distancePlaneToRingCenter - ringInnerRadius));
            }
        }
        
        //Send the signal for the next point to generate a question
        setupNext.Invoke(NextPointIndex);
        sendScore.Invoke(score);
        //TODO: do some thing with track selection stuff
    }
}
