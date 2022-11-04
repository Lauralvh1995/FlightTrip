using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    CinemachineDollyCart dollyCart;
    [SerializeField]
    List<CinemachineSmoothPath> tracks;
    [SerializeField]
    EquationPoint equationPointPrefab;
    [SerializeField]
    List<EquationPoint> equationPoints;
    [SerializeField]
    Session session;

    private void Start()
    {
        InitializeEquations();
    }
    private void Update()
    {

    }
    void InitializeEquations()
    {
        //For each track, generate X equation points. X is the amount of equations in the session.
        foreach(CinemachineSmoothPath track in tracks)
        {
            float pathOffset = track.PathLength / session.NumberOfEquations;
            float currentPathPosition = 0f;
            for(int i = 0; i<session.NumberOfEquations; i++)
            {
                currentPathPosition += pathOffset;
                EquationPoint point = Instantiate(equationPointPrefab, track.EvaluatePosition(currentPathPosition), track.EvaluateOrientation(currentPathPosition));
                //Set Equation
                point.SetBasesAndGenerateEquation(session.Bases, session.Op);
                equationPoints.Add(point);
            }
        }
    }
}
