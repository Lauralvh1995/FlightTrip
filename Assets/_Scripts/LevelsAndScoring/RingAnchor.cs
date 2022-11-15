using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingAnchor : MonoBehaviour
{
    [SerializeField]
    Bounds bounds;
    [SerializeField]
    Ring ring;
    [SerializeField]
    float verticalExtents = 2f;
    public Ring Ring { get => ring; set => ring = value; }
    public Bounds Bounds { get => bounds; set => bounds = value; }
    private void Awake()
    {
        //Make the bounding boxes adaptive. TODO: figure out a way to make the verticalExtents value adaptive as well.
        float aspect = Camera.main.aspect;
        bounds = new Bounds(Vector3.zero, 
            new Vector3(2f * verticalExtents * aspect, 2f * verticalExtents, 0f));
        
        transform.localPosition = new Vector3(Mathf.Sign(transform.localPosition.x) * verticalExtents * aspect,
            Mathf.Sign(transform.localPosition.y) * verticalExtents, 0f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, new Vector3(bounds.max.x*2, bounds.max.y*2, 0f));
    }
}
