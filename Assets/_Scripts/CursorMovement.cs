using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    [SerializeField]
    private float screenAspect;
    [SerializeField]
    private bool invertYAxis = false;
    [SerializeField]
    private float verticalBounds = 300f;
    [SerializeField]
    private float horizontalBounds;
    [SerializeField]
    private int speedModifier = 2;

    [SerializeField]
    RectTransform rectTransform;
    [SerializeField]
    RectTransform parentCanvas;
    private void Awake()
    {
        screenAspect = Camera.main.aspect;
        verticalBounds = parentCanvas.rect.height/2 - rectTransform.rect.height/2;
        horizontalBounds = verticalBounds * screenAspect;
    }

    public void UpdatePosition(Vector2 pos)
    {
        int invert = invertYAxis ? -1 : 1;
        Vector2 localPos = new Vector3(pos.x * verticalBounds * speedModifier * screenAspect,
            pos.y * horizontalBounds * invert);

        rectTransform.anchoredPosition = new Vector3(Mathf.Clamp(localPos.x, -horizontalBounds, horizontalBounds)
            , Mathf.Clamp(localPos.y, -verticalBounds, verticalBounds), 0f);
    }
}
