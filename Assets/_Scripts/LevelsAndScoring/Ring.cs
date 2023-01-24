using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Ring : MonoBehaviour
{
    [SerializeField]
    bool isOnPlayer = false;
    [SerializeField]
    Player player;
    [SerializeField]
    TextMeshPro answerText;
    [SerializeField]
    int answer;
    [SerializeField]
    float innerRadius = 0.4f;
    public new Renderer renderer;
    [SerializeField]
    Material defaultMaterial;
    [SerializeField]
    Material selectedMaterial;
    [SerializeField]
    GameObject innerRing;
    [SerializeField]
    Material innerRingUnselected;
    [SerializeField]
    Material innerRingSelected;

    [SerializeField]
    Vector3 positionWithinEquationPoint;
    public int Answer { get => answer; set => answer = value; }
    public float InnerRadius { get => innerRadius; set => innerRadius = value; }
    private void OnEnable()
    {
        if (player == null)
            player = FindObjectOfType<Player>();
    }
    private void OnDisable()
    {

    }

    private void Start()
    {
        if (!isOnPlayer)
        {
            EquationPoint parentPoint = GetComponentInParent<EquationPoint>();
            positionWithinEquationPoint = parentPoint.transform.position - transform.position;
        }
    }

    public void SetGraphics(int answer)
    {
        this.answer = answer;
        answerText.text = answer.ToString();
    }

    public Vector3 GetPositionWithinEquationPoint()
    {
        return positionWithinEquationPoint;
    }

    public void Unselect()
    {
        Material[] materials = renderer.materials;
        materials[0] = defaultMaterial;
        renderer.materials = materials;
        innerRing.GetComponent<Renderer>().material = innerRingUnselected;
    }
    public void Select()
    {
        Material[] materials = renderer.materials;
        materials[0] = selectedMaterial;
        renderer.materials = materials;
    }
    public void SelectedInner(bool set)
    {
        if(set)
            innerRing.GetComponent<Renderer>().material = innerRingSelected;
        else
            innerRing.GetComponent<Renderer>().material = innerRingUnselected;
        
    }
}
