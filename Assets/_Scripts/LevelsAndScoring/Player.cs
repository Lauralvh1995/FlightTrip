using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField]
    PlayerData playerData;

    [SerializeField]
    List<Ring> rings;
    [SerializeField]
    Equation currentEquation;
    [SerializeField]
    TextMeshPro equationText;
    [SerializeField]
    Transform plane;

    public Transform Plane { get => plane; set => plane = value; }

    public void SetPlayerUI(List<Ring> rings, Equation currentEquation)
    {
        this.currentEquation = currentEquation;
        this.equationText.text = this.currentEquation.GenerateEquationToString();
        for (int i = 0; i < rings.Count; i++)
        {
            this.rings[i].transform.localPosition = rings[i].transform.localPosition;
            this.rings[i].Answer = rings[i].Answer;
            this.rings[i].SetGraphics(this.rings[i].Answer);
        }
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

    public void AddSessionData(Session session)
    {
        playerData.playedSessions.Add(session);
    }
}
