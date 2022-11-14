using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingAnchor : MonoBehaviour
{
    [SerializeField]
    Bounds bounds;
    [SerializeField]
    Ring ring;
    public Ring Ring { get => ring; set => ring = value; }
    public Bounds Bounds { get => bounds; set => bounds = value; }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, new Vector3(bounds.max.x*2, bounds.max.y*2, 0f));
    }
}
