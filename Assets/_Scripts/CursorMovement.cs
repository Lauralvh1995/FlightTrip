using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    [SerializeField]
    private float screenAspect;
    [SerializeField]
    private float speedMultiplier = 100f;
    [SerializeField]
    private bool invertYAxis = false;
    [SerializeField]
    private float verticalBounds = 300f;
    [SerializeField]
    private float horizontalBounds;

    [SerializeField]
    RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        screenAspect = Camera.main.aspect;
        verticalBounds = Camera.main.scaledPixelHeight * 0.3f;
        horizontalBounds = verticalBounds * screenAspect;
    }

    public void UpdatePosition(Vector2 pos)
    {
        int invert = invertYAxis ? -1 : 1;
        Vector2 localPos = new Vector3(pos.x * speedMultiplier * screenAspect,
            pos.y * speedMultiplier * invert);

        rectTransform.anchoredPosition = new Vector3(Mathf.Clamp(localPos.x, -horizontalBounds, horizontalBounds)
            , Mathf.Clamp(localPos.y, -verticalBounds, verticalBounds), 0f);
    }
}
