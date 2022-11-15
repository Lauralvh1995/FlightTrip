using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Ring : MonoBehaviour
{
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
    UnityEvent<int> sendScore;
    public int Answer { get => answer; set => answer = value; }
    private void OnEnable()
    {
        if (player == null)
            player = FindObjectOfType<Player>();
        sendScore.AddListener(player.AddScore);
        particles = GetComponentInChildren<ParticleSystem>();
    }
    private void OnDisable()
    {
        sendScore.RemoveListener(player.AddScore);
    }

    public void SetGraphics(int answer)
    {
        this.answer = answer;
        answerText.text = answer.ToString();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.GetMask("Airplane"))
        {
            particles.Play();
            //Points for ring accuracy
            float distancePlaneToRingCenter = Vector3.Distance(other.GetContact(0).point, transform.localPosition);
            int score = 0;
            Debug.Log("Distance from plane to ring center: " + distancePlaneToRingCenter);
            //If plane hits ring at all
            if (distancePlaneToRingCenter < ringRadius)
            {
                score += 10;
                //If plane goes through the center
                if (distancePlaneToRingCenter < ringInnerRadius)
                {
                    score += 10;
                }
                //add a fraction of 10 for accuracy
                else
                {
                    score += Mathf.FloorToInt(10f * (distancePlaneToRingCenter - ringInnerRadius));
                }
            }
            sendScore.Invoke(score);
            //TODO: Add another score breakdown thing here
        }
    }
}
