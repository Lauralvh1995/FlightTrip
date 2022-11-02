using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ring : MonoBehaviour
{
    public bool IsSelected { get; private set; }

    public Renderer renderer;
    public Renderer innerRenderer;
    public GameObject FallRing;
    private Vector3 fallRingStartPosition;
    private float maxCenterDistance = 1;

    [SerializeField]
    private Collider collider;

    [SerializeField]
    private TMP_Text answerText;

    [SerializeField]
    private ParticleSystem rewardParticles;

    [SerializeField]
    private Material defaultMaterial;
    [SerializeField]
    private Material hoverMaterial;
    [SerializeField]
    private Material highlightMaterial;

    public int Answer {
        get
        {
            return answer;
        }
        set
        {
            answerText.text = value.ToString();
            answer = value;
        }
    }

    public void Awake()
    {
        fallRingStartPosition = FallRing.transform.position;
    }

    private int answer;

    //This Ring was correct
    public void Highlight()
    {
        renderer.material = highlightMaterial;
        rewardParticles.Play();
    }

    public IEnumerator Flash()
    {
        renderer.material = highlightMaterial;

        float currentFlashDuration = 0;

        while(currentFlashDuration < 1.5f)
        {
            currentFlashDuration += Time.deltaTime;
            renderer.enabled = false;
            if(currentFlashDuration % 0.3f < 0.15f)
            {
                renderer.enabled = true;
            }
            else
            {
                renderer.enabled = false;
            }
            yield return null;
        }
        renderer.enabled = true;
    }

    //Player moves onto this ring
    public void Select()
    {
        IsSelected = true;
        renderer.material = hoverMaterial;
    }

    //Player leaves this ring
    public void Deselect()
    {
        IsSelected = false;
        renderer.material = defaultMaterial;
    }

    public void Lock()
    {
        collider.enabled = false;
    }

    public bool PlayerInCenter()
    {
        //really dirty!
        Vector3 playerPosition = GameObject.Find("Player").transform.position;

        if (Vector3.Distance(playerPosition, transform.position) < maxCenterDistance)
        {
            innerRenderer.material = highlightMaterial;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnEnable()
    {
        collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Select();
    }

    private void OnTriggerExit(Collider other)
    {
        Deselect();
    }

    public IEnumerator Fall(float duration)
    {
        float currentDuration = 0;
        while (currentDuration < duration)
        {
            currentDuration += Time.deltaTime;
            float lerpAmount = currentDuration / duration;
            FallRing.transform.position = Vector3.Lerp(fallRingStartPosition, transform.position, lerpAmount);
            yield return null;
        }

        //uncomment this to have the rings not disappear when flying through
        /*while (currentDuration < duration + 1)
        {
            currentDuration += Time.deltaTime;
            float lerpAmount = currentDuration / duration;
            FallRing.transform.position = Vector3.LerpUnclamped(FallRingStartPosition, transform.position, lerpAmount);
            yield return null;
        }*/

        FallRing.transform.position = fallRingStartPosition;
    }
}
