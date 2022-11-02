using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField]
    private float screenAspect;
    [SerializeField]
    private float speedMultiplier = 10f;
    [SerializeField]
    private bool invertYAxis = false;

    [SerializeField]
    private float verticalBounds = 3f;
    [SerializeField]
    private float horizontalBounds;

    private void Awake()
    {
        screenAspect = Camera.main.aspect;
        horizontalBounds = verticalBounds * screenAspect;
    }

    public void UpdatePosition(Vector2 pos)
    {
        int invert = invertYAxis ? -1 : 1;
        Vector3 localPos = new Vector3(pos.x * speedMultiplier * screenAspect,
            pos.y * speedMultiplier * invert,
            transform.localPosition.z);


        transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, -horizontalBounds, horizontalBounds)
            , Mathf.Clamp(localPos.y, -verticalBounds, verticalBounds)
            , transform.localPosition.z);
    }
}
