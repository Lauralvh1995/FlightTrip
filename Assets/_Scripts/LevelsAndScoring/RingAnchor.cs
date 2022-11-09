using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingAnchor : MonoBehaviour
{
    [SerializeField]
    Vector2 bounds;
    [SerializeField]
    Ring ring;
    public Ring Ring { get => ring; set => ring = value; }
    public Vector2 Bounds { get => bounds; set => bounds = value; }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, new Vector3(bounds.x*2, bounds.y*2, 0f));
    }
}
