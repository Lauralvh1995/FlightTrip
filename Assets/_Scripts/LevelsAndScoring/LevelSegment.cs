using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour
{
    public Equation equation;

    [SerializeField]
    private List<Ring> rings;

    [SerializeField]
    private List<AreaGroup> areaGroupsList;

    [System.Serializable]
    public class AreaGroup
    {
        public List<Bounds> ringAreas;
    }

    public void SetDuration(float duration)
    {
        foreach (Ring ring in rings)
        {
            StartCoroutine(ring.Fall(duration));
        }
    }

    public bool CheckAnswer()
    {
        Ring selectedRing = null;

        foreach (Ring ring in rings)
        {
            ring.Lock();

            if(ring.IsSelected)
            {
                selectedRing = ring;
            }

            if(ring.Answer == equation.GetCorrectAnswer() && ring.IsSelected)
            {
                ring.Highlight();
            }

            if (ring.Answer == equation.GetCorrectAnswer() && !ring.IsSelected)
            {
                StartCoroutine(ring.Flash());
            }

        }

        if(selectedRing == null)
        {
            equation.GivenAnswer = -1;
            return false;
        }

        equation.GivenAnswer = selectedRing.Answer;

        if (selectedRing.Answer == equation.GetCorrectAnswer())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckIfCenter()
    {
        foreach (Ring ring in rings)
        {
            if (ring.IsSelected)
            {
                return ring.PlayerInCenter();
            }
        }

        return false;
    }

    private void OnEnable()
    {
        rings[0].Answer = equation.GetCorrectAnswer();

        for (int i = 1; i < rings.Count; i++)
        {
            rings[i].Answer = equation.GetSimilarAnswer();
            while(rings[i].Answer == rings[i-1].Answer)
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

            ring.transform.position = new Vector3(
                Random.Range(availableAreas[areaIndex].min.x + ring.renderer.bounds.extents.x,
                    availableAreas[areaIndex].max.x - ring.renderer.bounds.extents.x),
                0,
                Random.Range(availableAreas[areaIndex].min.z + ring.renderer.bounds.extents.z,
                    availableAreas[areaIndex].max.z - ring.renderer.bounds.extents.z));
            availableAreas.RemoveAt(areaIndex);
        }
    }

    private void Update()
    {
        Debug.Log(equation);
    }

    private void OnDisable()
    {
        foreach (Ring ring in rings)
        {
            ring.Deselect();
        }
    }

}
