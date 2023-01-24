using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquationPoint : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    Player player;

    [Header("Equation")]
    [SerializeField]
    Equation equation;

    [Header("Rings")]
    [SerializeField]
    List<Ring> rings;
    [SerializeField]
    Ring ringBL;
    [SerializeField]
    Ring ringBR;
    [SerializeField]
    Ring ringTL;
    [SerializeField]
    Ring ringTR;

    [Header("Anchors")]
    [SerializeField]
    private List<RingAnchor> ringAnchors;
    [SerializeField]
    RingAnchor ringAnchorBL;
    [SerializeField]
    RingAnchor ringAnchorBR;
    [SerializeField]
    RingAnchor ringAnchorTL;
    [SerializeField]
    RingAnchor ringAnchorTR;

    [Header("Properties")]
    [SerializeField]
    private int nextPointIndex;
    public int NextPointIndex { get => nextPointIndex; set => nextPointIndex = value; }
    [SerializeField]
    float ringRadius = 1f;
    public float RingRadius { get => ringRadius; private set => ringRadius = value; }
    [SerializeField]
    float ringInnerRadius;
    public float RingInnerRadius { get => ringInnerRadius; private set => ringInnerRadius = value; }

    [Header("Events")]
    //These are public because the Game Manager sets them
    public UnityEvent<List<Ring>, Equation> onRingsSetup;
    public UnityEvent<int, Equation, bool> setupNext;
    public UnityEvent<ScoreEntry> onScore;
    public UnityEvent equationFinished;

    private void OnEnable()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }
        onRingsSetup.AddListener(player.SetPlayerUI);
        ringBL = ringAnchorBL.Ring;
        ringBR = ringAnchorBR.Ring;
        ringTL = ringAnchorTL.Ring;
        ringTR = ringAnchorTR.Ring;

        rings.Add(ringBL);
        rings.Add(ringBR);
        rings.Add(ringTL);
        rings.Add(ringTR);
        ringAnchors.Add(ringAnchorBL);
        ringAnchors.Add(ringAnchorBR);
        ringAnchors.Add(ringAnchorTL);
        ringAnchors.Add(ringAnchorTR);
        ringInnerRadius = rings[0].InnerRadius;
    }
    private void OnDisable()
    {
        onRingsSetup.RemoveListener(player.SetPlayerUI);
        setupNext.RemoveAllListeners();
        equationFinished.RemoveAllListeners();
        onScore.RemoveAllListeners();
    }

    public void SetBasesAndGenerateEquation(List<int> bases, Operator op, Player player, Equation previous)
    {
        equation.GenerateEquation(bases, op, previous);
        Debug.Log(equation.firstNumber.ToString() + equation.op.ToString() + equation.secondNumber.ToString());
        this.player = player;
        //Spawn Rings
        SetUpRings();
    }
    public void SetupPresetEquation(Equation equation, Player player)
    {
        this.equation = equation;
        this.equation.GeneratePreset();
        this.player = player;
        SetUpRings();
    }


    public void SetUpRings()
    {
        //Setup each ring position
        for (int i = 0; i < ringAnchors.Count; i++)
        {
            ringAnchors[i].Ring = rings[i];
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
		rings[randomRingIndex].Answer = equation.GetCorrectAnswer();
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
        Gizmos.DrawWireSphere(transform.position, 1f);
    }

    private void EnterAnswer()
    {
        //choose answer based on the player's quadrant
        int answer = 0;
        switch (player.Quadrant)
        {
            case Quadrant.BL:
                answer = ringBL.Answer;
                break;
            case Quadrant.BR:
                answer = ringBR.Answer;
                break;
            case Quadrant.TL:
                answer = ringTL.Answer;
                break;
            case Quadrant.TR:
                answer = ringTR.Answer;
                break;
            default:
                answer = -1; //not an answer
                break;
        }

        float accuracy = 0f;
        float distancePlaneToRingCenter = player.DistanceToCenterOfRing;
        Debug.Log("Distance from plane to ring center: " + distancePlaneToRingCenter);
        //If plane hits ring at all
        if (distancePlaneToRingCenter < ringRadius)
        {
            accuracy = 1f;
            //If plane goes through the center
            if (distancePlaneToRingCenter < ringInnerRadius)
            {
                accuracy = 2f;
            }
            //add a fraction of 10 for accuracy
            else
            {
                accuracy += distancePlaneToRingCenter - ringInnerRadius;
            }
        }
        bool correct = answer == equation.GetCorrectAnswer();
        //Send the signal for the next point to generate a question
        onScore.Invoke(new ScoreEntry(equation, answer, accuracy));
        equationFinished.Invoke();
        setupNext.Invoke(NextPointIndex, equation, correct);

        //TODO: do some thing with track selection stuff
    }
}
