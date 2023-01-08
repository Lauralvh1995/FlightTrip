using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    Player player;
    [SerializeField]
    bool gameHasEnded = false;
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
    [SerializeField]
    UnityEvent<int> gameEndedScore;

    [SerializeField]
    UnityEvent<int> updateScore;

    private void Start()
    {
        equationPoints = new Dictionary<CinemachineSmoothPath, List<EquationPoint>>();
        SwitchTrack(tracks[0]);
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
                point.onScore.AddListener(AddScoreEntry);
                point.setupNext.AddListener(SetupNextEquation);
                point.setupNextWhenWrong.AddListener(SetupNextWhenWrong);
                point.equationFinished.AddListener(IncrementEquationsFinished);

            }
            equationPoints.Add(track, tempList);
        }
        SetupNextEquation(0);
    }
    public void SetupNextWhenWrong(int index, Equation equation)
    {
        if (!gameHasEnded)
        {
            currentEquationIndex = index;
            player.HideRings();
            player.HideEquation();
            List<EquationPoint> points;
            equationPoints.TryGetValue(currentTrack, out points);
            Debug.Log("Activating " + points[index].name);
            points[index].SetupPresetEquation(equation, player);
            //Delayed show of new info
            Invoke("ShowEquation", timeToNextWaypoint - timeBetweenRingsAndTurnIn - session.ThinkingTime);
            Invoke("ShowRings", timeToNextWaypoint - timeBetweenRingsAndTurnIn);
        }
    }
    public void SetupNextEquation(int index)
    {
        if (!gameHasEnded)
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
            if (equationsFinished >= session.NumberOfEquations)
            {
                gameHasEnded = true;
                EndGame();
            }
        }
    }
    public void AddScoreEntry(ScoreEntry entry)
    {
        session.AddEntry(entry);
        updateScore.Invoke(session.GetScore());
    }
    private void EndGame()
    {
        dollyCart.m_Speed = 0;
        player.AddSessionData(session);
        gameEnded.Invoke();
        gameEndedScore.Invoke(session.GetScore());
    }

    public void LoadData(GameData gameData)
    {
        player.SetPlayerData(gameData);
        session = gameData.latestSession;
    }

    public void SaveData(GameData gameData)
    {
        gameData.playerData = player.GetPlayerData();
    }
}
