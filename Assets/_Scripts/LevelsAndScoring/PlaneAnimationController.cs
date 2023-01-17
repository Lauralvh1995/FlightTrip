using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class PlaneAnimationController : MonoBehaviour
{
    [SerializeField]
    private new Animation animation;

    [SerializeField]
    private AnimationClip barrelRollLeft;
    [SerializeField]
    private AnimationClip barrelRollRight;

    private void Start()
    {
        animation = GetComponent<Animation>();
    }

    public void BarrelRoll(bool invertDirection)
    {
        if (!animation.isPlaying)
        {
            if (invertDirection)
                animation.clip = barrelRollLeft;
            else
                animation.clip = barrelRollRight;

            animation.Play();
        }
    }
}
