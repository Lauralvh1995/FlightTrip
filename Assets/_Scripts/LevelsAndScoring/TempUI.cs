using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempUI : MonoBehaviour
{
    public Session session;
    public int numberOfEquations = 100;

    public MultiplicationTables multiplicationTablesSession;
    public DivisionTables divisionTablesSession;

    //private MultiplicationTables hardTables;

    public bool AdditionTill10 = false;
    public bool SubtractionTill10 = false;
    public bool Addition10Till20 = false;
    public bool Subtraction10Till20 = false;
    public bool BuildingBlocksTill100 = false;
    public bool MultiplicationTables = false;
    public bool DivisionTables = false;

    //public bool MoeilijkeTafels = false;
    //public bool Optellen = false;

    private void Start()
    {
        multiplicationTablesSession = new MultiplicationTables();
        divisionTablesSession = new DivisionTables();

        //hardTables = new MultiplicationTables();
        //hardTables.UpdateListSelectedNumbers(new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });
    }

    private void Update()
    {
        if(AdditionTill10)
        {
            AdditionTill10 = false;
            session.Generate(numberOfEquations, new List<Equation> { new AdditionTill10() });
            StartCoroutine(session.StartSession());
        }    
        if(SubtractionTill10)
        {
            SubtractionTill10 = false;
            session.Generate(numberOfEquations, new List<Equation> { new SubtractionTill10() });
            StartCoroutine(session.StartSession());
        }
        if(Addition10Till20)
        {
            Addition10Till20 = false;
            session.Generate(numberOfEquations, new List<Equation> { new Addition10Till20() });
            StartCoroutine(session.StartSession());
        }
        if(Subtraction10Till20)
        {
            Subtraction10Till20 = false;
            session.Generate(numberOfEquations, new List<Equation> { new Subtraction10Till20() });
            StartCoroutine(session.StartSession());
        }
        if(BuildingBlocksTill100)
        {
            BuildingBlocksTill100 = false;
            session.Generate(numberOfEquations, new List<Equation> { new BuildingBlocksTill100() });
            StartCoroutine(session.StartSession());
        }
        if(MultiplicationTables)
        {
            MultiplicationTables = false;
            session.Generate(numberOfEquations, new List<Equation> { multiplicationTablesSession });
            StartCoroutine(session.StartSession());
        }
        if(DivisionTables)
        {
            DivisionTables = false;
            session.Generate(numberOfEquations, new List<Equation> { divisionTablesSession });
            StartCoroutine(session.StartSession());
        }


        
        //if (MoeilijkeTafels)
        //{
        //    //TEMPGenerate = false;
        //    MoeilijkeTafels = false;
        //    session.Generate(15, new List<Equation> { hardTables });
        //    StartCoroutine(session.StartSession());
        //}
        //if(Optellen)
        //{
        //    Optellen = false;
        //    session.Generate(15, new List<Equation> { new AdditionTill10(), new Addition10Till20(), new BuildingBlocksTill100() });
        //    StartCoroutine(session.StartSession());
        //}
    }
}
