using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneParticleController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem correctParticles;
    [SerializeField]
    ParticleSystem answeredParticles;

    public void PlayParticles(bool correct)
    {
        if (correct)
            correctParticles.Play();
        else
        {
            answeredParticles.Play();
        }
    }
}
