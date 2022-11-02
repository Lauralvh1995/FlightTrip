using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    CinemachineDollyCart dollyCart;

    [SerializeField]
    float currentSpeedModifier = 100f;
    private void Update()
    {
        //updating the dolly
        currentSpeedModifier += Random.Range(-5f, 6f);
        currentSpeedModifier = Mathf.Clamp(currentSpeedModifier, 80f, 120f);
        dollyCart.m_Speed = currentSpeedModifier;
    }
}
