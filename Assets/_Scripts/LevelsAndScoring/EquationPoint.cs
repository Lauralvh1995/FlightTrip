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
    private List<AreaGroup> areaGroupsList;
    [SerializeField]
    private int nextPointIndex;
    public int NextPointIndex { get => nextPointIndex; set => nextPointIndex = value; }

    public UnityEvent<List<Ring>, Equation> OnRingsSetup;
    public UnityEvent<int> setupNext;

    public void SetBasesAndGenerateEquation(List<int> bases, Operator op, Player player)
    {
        equation.GenerateEquation(bases, op);
        Debug.Log(equation.firstNumber + equation.op + equation.secondNumber);
        //Spawn Rings
        SetUpRings();
    }

    public void SetUpRings()
    {
        rings[0].SetGraphics(equation.GetCorrectAnswer());
        rings[0].gameObject.SetActive(true);

        for (int i = 1; i < rings.Count; i++)
        {
            rings[i].SetGraphics(equation.GetSimilarAnswer());
            rings[i].gameObject.SetActive(true);
            while (rings[i].Answer == rings[i - 1].Answer)
            {
                rings[i].Answer = equation.GetSimilarAnswer();
            }
        }

        //Select a random group of ring Areas. We copy the list so we can remove entries from it as we iterate through it
        List<Bounds> availableAreas = new List<Bounds>(areaGroupsList[Random.Range(0, areaGroupsList.Count)].ringAreas);

        //Assign each ring a random location without overlap
        foreach (Ring ring in rings)
        {
            int areaIndex = Random.Range(0, availableAreas.Count);

            ring.transform.localPosition = new Vector3(
                Random.Range(availableAreas[areaIndex].min.x + ring.renderer.bounds.extents.x,
                    availableAreas[areaIndex].max.x - ring.renderer.bounds.extents.x),
                Random.Range(availableAreas[areaIndex].min.y + ring.renderer.bounds.extents.y,
                    availableAreas[areaIndex].max.y - ring.renderer.bounds.extents.y),
                0);
            availableAreas.RemoveAt(areaIndex);
        }
        OnRingsSetup.Invoke(rings, equation);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit by: " + other.name);
        setupNext.Invoke(NextPointIndex);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 3f);
    }
}
[System.Serializable]
public class AreaGroup
{
    public List<Bounds> ringAreas;
}
