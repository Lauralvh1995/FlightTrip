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
    ParticleSystem particles;
    [SerializeField]
    Player player;
    [SerializeField]
    TextMeshPro answerText;
    [SerializeField]
    int answer;
    public new Renderer renderer;

    [SerializeField]
    Vector3 positionWithinEquationPoint;

    [SerializeField]
    UnityEvent<float> sendAccuracy;
    public int Answer { get => answer; set => answer = value; }
    private void OnEnable()
    {
        if (player == null)
            player = FindObjectOfType<Player>();

        particles = GetComponentInChildren<ParticleSystem>();
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
}
