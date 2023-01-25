using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerColorController))]
public class Player : MonoBehaviour
{
    [Header("Other Components and Data")]
    [SerializeField]
    PlayerData playerData;
    [SerializeField]
    PlayerColorController colorController;

    [Header("Rings and Equation")]
    [SerializeField]
    List<Ring> rings;
    [SerializeField]
    Ring RingBL;
    [SerializeField]
    Ring RingBR;
    [SerializeField]
    Ring RingTL;
    [SerializeField]
    Ring RingTR;
    [SerializeField]
    Ring selectedRing;

    [SerializeField]
    Equation currentEquation;
    [SerializeField]
    TextMeshProUGUI equationText;
    [SerializeField]
    Transform plane;

    [Header("Attributes for distance measurement")]
    [SerializeField]
    Quadrant quadrant;
    [SerializeField]
    float distanceToCenterOfRing;
    [SerializeField]
    float margin = 0.5f;
    [SerializeField]
    float ringInnerRadius;

    public Transform Plane { get => plane; private set => plane = value; }
    public Quadrant Quadrant { get => quadrant; private set => quadrant = value; }
    public float DistanceToCenterOfRing { get => distanceToCenterOfRing; private set => distanceToCenterOfRing = value; }

    private void Start()
    {
        colorController = GetComponent<PlayerColorController>();
        colorController.SetUpGraphics(playerData.planeColor, playerData.playerColor);
        ringInnerRadius = rings[0].InnerRadius;
    }
    private void Update()
    {
        Debug.DrawLine(plane.position - plane.forward * margin, plane.position + plane.forward * margin, Color.red);
        Debug.DrawLine(plane.position - plane.up * margin, plane.position + plane.up * margin, Color.red);
        if (plane.localPosition.x < 0 && plane.localPosition.y < 0)
        {
            quadrant = Quadrant.BL;
        }
        else if (plane.localPosition.x > 0 && plane.localPosition.y < 0)
        {
            quadrant = Quadrant.BR;
        }
        else if (plane.localPosition.x < 0 && plane.localPosition.y > 0)
        {
            quadrant = Quadrant.TL;
        }
        else if (plane.localPosition.x > 0 && plane.localPosition.y > 0)
        {
            quadrant = Quadrant.TR;
        }
        else
        {
            quadrant = Quadrant.NONE;
        }
        //the plane is on a different z offset
        Vector3 correctedPlanePosition = plane.localPosition - (Vector3.forward * 7.5f);
        switch (quadrant)
        {
            case Quadrant.BL:
                selectedRing = RingBL;
                break;
            case Quadrant.BR:
                selectedRing = RingBR;
                break;
            case Quadrant.TL:
                selectedRing = RingTL;
                break;
            case Quadrant.TR:
                selectedRing = RingTR;
                break;
            default:
                distanceToCenterOfRing = float.MaxValue;
                Debug.Log("You're exactly in the middle, that's not good");
                break;
        }
        if(distanceToCenterOfRing != float.MaxValue)
            distanceToCenterOfRing = Vector3.Distance(correctedPlanePosition, selectedRing.transform.localPosition) - margin;
        HandleSelectedRing();
    }

    private void HandleSelectedRing()
    {
        foreach(Ring ring in rings)
        {
            ring.Unselect();
        }
        selectedRing.Select();
        if(distanceToCenterOfRing < ringInnerRadius)
        {
            selectedRing.SelectedInner(true);
        }
        else
        {
            selectedRing.SelectedInner(false);
        }
    }

    public void SetPlayerUI(List<Ring> rings, Equation currentEquation)
    {
        this.currentEquation = currentEquation;
        this.equationText.text = this.currentEquation.GenerateEquationToString();

        //Yes, this could have been a for-loop, 
        //but this makes sure that each ring is guaranteed to be in the correct position
        //0 = BL
        //1 = BR
        //2 = TL
        //3 = TR
        RingBL.transform.localPosition = rings[0].transform.localPosition;
        RingBL.Answer = rings[0].Answer;
        RingBL.SetGraphics(this.rings[0].Answer);
        RingBR.transform.localPosition = rings[1].transform.localPosition;
        RingBR.Answer = rings[1].Answer;
        RingBR.SetGraphics(this.rings[1].Answer);
        RingTL.transform.localPosition = rings[2].transform.localPosition;
        RingTL.Answer = rings[2].Answer;
        RingTL.SetGraphics(this.rings[2].Answer);
        RingTR.transform.localPosition = rings[3].transform.localPosition;
        RingTR.Answer = rings[3].Answer;
        RingTR.SetGraphics(this.rings[3].Answer);

    }
    public void ShowRings()
    {
        foreach (Ring ring in this.rings)
        {
            ring.gameObject.SetActive(true);
        }
    }
    public void ShowEquation()
    {
        equationText.gameObject.SetActive(true);
    }
    public void HideRings()
    {
        foreach (Ring ring in this.rings)
        {
            ring.gameObject.SetActive(false);
        }
    }
    public void HideEquation()
    {
        equationText.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        HideRings();
        HideEquation();
    }
    public void SetPlayerData(GameData data)
    {
        this.playerData = data.playerData;
    }
    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public void AddSessionData(Session session)
    {
        playerData.playedSessions.Add(session);
    }
}

public enum Quadrant
{
    NONE,
    TL,
    TR,
    BL,
    BR
}
