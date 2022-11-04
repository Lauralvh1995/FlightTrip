using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationPoint : MonoBehaviour
{
    [SerializeField]
    Equation equation;

    public void SetBasesAndGenerateEquation(List<int> bases, Operator op)
    {
        equation.GenerateEquation(bases, op);

        //Spawn Rings
    }


}
