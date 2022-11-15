using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    CinemachineDollyCart dollyCart;
    [SerializeField]
    List<CinemachineSmoothPath> tracks;
    [SerializeField]
    CinemachineSmoothPath currentTrack;
    [SerializeField]
    EquationPoint equationPointPrefab;
    [SerializeField]
    Dictionary<CinemachineSmoothPath, List<EquationPoint>> equationPoints;
    [SerializeField]
    Session session;
    [SerializeField]
    int amountOfSectionsPerTrack = 10;
    [SerializeField]
    int currentEquationIndex = 0;
    [SerializeField]
    int equationsFinished = 0;

    [SerializeField]
    float timeToNextWaypoint = 0f;
    [SerializeField]
    int timeBetweenRingsAndTurnIn = 2;

    [SerializeField]
    UnityEvent gameEnded;

    private void Start()
    {
        equationPoints = new Dictionary<CinemachineSmoothPath, List<EquationPoint>>();
        SwitchTrack(tracks[0]);
        player = FindObjectOfType<Player>();
        InitializeEquations();
    }
    private void Update()
    {

    }
    private void SwitchTrack(CinemachineSmoothPath track)
    {
        currentTrack = track;
        timeToNextWaypoint = currentTrack.PathLength / amountOfSectionsPerTrack / dollyCart.m_Speed;
    }
    void InitializeEquations()
    {
        //For each track, generate X equation points. X is the amount of equations in the session.
        foreach(CinemachineSmoothPath track in tracks)
        {
            List<EquationPoint> tempList = new List<EquationPoint>();
            float pathOffset = track.PathLength / amountOfSectionsPerTrack;
            float currentPathPosition = 0f;
            for(int i = 0; i < amountOfSectionsPerTrack; i++)
            {
                currentPathPosition += pathOffset;
                EquationPoint point = Instantiate(equationPointPrefab, track.EvaluatePositionAtUnit(currentPathPosition, CinemachinePathBase.PositionUnits.Distance),
                    track.EvaluateOrientationAtUnit(currentPathPosition, CinemachinePathBase.PositionUnits.Distance));
                
                point.gameObject.name = currentTrack.name + " Point " + i; 
                Debug.Log("Created " + point.gameObject.name + " at " + track.EvaluatePositionAtUnit(currentPathPosition, CinemachinePathBase.PositionUnits.Distance));
                point.gameObject.transform.SetParent(track.transform);
                //Set Equation
                tempList.Add(point);
                //
                if(i + 1 == amountOfSectionsPerTrack)
                {
                    point.NextPointIndex = 0;
                }
                else
                {
                    point.NextPointIndex = i + 1;
                }
                point.setupNext.AddListener(SetupNextEquation);
            }
            equationPoints.Add(track, tempList);
        }
        SetupNextEquation(0);
    }
    public void SetupNextEquation(int index)
    {
        currentEquationIndex = index;
        player.HideRings();
        player.HideEquation();
        List<EquationPoint> points;
        equationPoints.TryGetValue(currentTrack, out points);
        Debug.Log("Activating " + points[index].name);
        points[index].SetBasesAndGenerateEquation(session.Bases, session.Op, player);
        //Delayed show of new info
        Invoke("ShowEquation", timeToNextWaypoint - timeBetweenRingsAndTurnIn - session.ThinkingTime);
        Invoke("ShowRings", timeToNextWaypoint - timeBetweenRingsAndTurnIn);
    }
    public void ShowRings()
    {
        player.ShowRings();
    }
    public void ShowEquation()
    {
        player.ShowEquation();
    }
    public void IncrementEquationsFinished()
    {
        equationsFinished++;
        if (session.NumberOfEquations != 0)
        {
            if (equationsFinished > session.NumberOfEquations)
            {
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        dollyCart.m_Speed = 0;
        gameEnded.Invoke();
    }
}
