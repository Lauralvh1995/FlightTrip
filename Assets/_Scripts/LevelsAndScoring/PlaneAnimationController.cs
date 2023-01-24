using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class PlaneAnimationController : MonoBehaviour
{
    [SerializeField]
    private new Animation animation;

    private void Start()
    {
        animation = GetComponent<Animation>();
    }

    public void PlayAnimation(AnimationClip clip)
    {
        if (!animation.isPlaying)
        {
            animation.clip = clip;
            animation.Play();
        }
    }
}
