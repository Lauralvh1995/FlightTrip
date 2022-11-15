using Cinemachine;
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
    Player player;

    [SerializeField]
    private List<RingAnchor> ringAnchors;
    [SerializeField]
    private int nextPointIndex;
    public int NextPointIndex { get => nextPointIndex; set => nextPointIndex = value; }

    public UnityEvent<List<Ring>, Equation> onRingsSetup;
    public UnityEvent<int> setupNext;
    public UnityEvent<int> sendScore;
    public UnityEvent equationFinished;

    private void OnEnable()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }
        sendScore.AddListener(player.AddScore);
        onRingsSetup.AddListener(player.SetPlayerUI);
    }
    private void OnDisable()
    {
        sendScore.RemoveListener(player.AddScore);
        onRingsSetup.RemoveListener(player.SetPlayerUI);
        setupNext.RemoveAllListeners();
        equationFinished.RemoveAllListeners();
    }

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
            //ringAnchors[i].Ring = rings[i];
            rings[i].transform.localPosition = new Vector3(
                Random.Range(ringAnchors[i].transform.localPosition.x + ringAnchors[i].Bounds.min.x + 1f, 
                ringAnchors[i].transform.localPosition.x + ringAnchors[i].Bounds.max.x - 1f),
                Random.Range(ringAnchors[i].transform.localPosition.y + ringAnchors[i].Bounds.min.y + 1f,
                ringAnchors[i].transform.localPosition.y + ringAnchors[i].Bounds.max.y - 1f),
                0);
            rings[i].Answer = equation.GetSimilarAnswer();
            rings[i].gameObject.SetActive(true);
        }
        //Assign correct answer to a random ring
        int randomRingIndex = Random.Range(0, rings.Count);
        rings[randomRingIndex].SetGraphics(equation.GetCorrectAnswer());
        onRingsSetup.Invoke(rings, equation);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + "was hit by: " + other.name);
        if(other.GetComponent<CinemachineDollyCart>())
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
        //Send the signal for the next point to generate a question
        equationFinished.Invoke();
        setupNext.Invoke(NextPointIndex);
        sendScore.Invoke(score);

        //TODO: do some thing with track selection stuff
        //TODO: save the score breakdown somewhere
    }
}
