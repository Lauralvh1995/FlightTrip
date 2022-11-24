using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Ring : MonoBehaviour
{
    [SerializeField]
    GameManager manager;

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
    float ringRadius = 1f;
    [SerializeField]
    float ringInnerRadius = 0.4f;

    [SerializeField]
    Vector3 positionWithinEquationPoint;

    [SerializeField]
    UnityEvent<float> sendAccuracy;
    public int Answer { get => answer; set => answer = value; }
    private void OnEnable()
    {
        if(manager == null)
        {
            manager = FindObjectOfType<GameManager>();
        }
        if (player == null)
            player = FindObjectOfType<Player>();
        sendAccuracy.AddListener(manager.AddAccuracyToLatestEntry);
        particles = GetComponentInChildren<ParticleSystem>();
    }
    private void OnDisable()
    {
        sendAccuracy.RemoveListener(manager.AddAccuracyToLatestEntry);
    }

    private void Start()
    {
        EquationPoint parentPoint = GetComponentInParent<EquationPoint>();
        positionWithinEquationPoint = parentPoint.transform.position - transform.position;
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
