using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(CursorInput))]
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
    private float clickTolerance = 0.01f;

    [SerializeField]
    RectTransform rectTransform;

    [SerializeField]
    UnityEvent<bool> OnAllowClick;

    private float timer = 1;
    private float currentTime;

    private bool canInvokeClick = false;

    private Vector2 currentPos;
    private Vector2 previousPos;
    private void Awake()
    {
        screenAspect = Camera.main.aspect;
        verticalBounds = Screen.height - rectTransform.rect.height;
        horizontalBounds = verticalBounds * screenAspect;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > timer)
        {
            canInvokeClick = true;
            currentTime = 0;
        }
        if (canInvokeClick)
        {
            canInvokeClick = false;
            float distance = Vector2.Distance(previousPos, currentPos);
            Debug.Log("Last difference in distance: " + distance);
            if (distance < clickTolerance)
            {
                Debug.Log("Clicky clicky");
                AllowClick();
            }
            else
            {
                Debug.Log("No clicky clicky");
                DisallowClick();
            }
        }
    }

    public void UpdatePosition(Vector2 pos)
    {
        previousPos = currentPos;
        currentPos = pos;
        int invert = invertYAxis ? -1 : 1;
        Vector2 localPos = new Vector3(currentPos.x * verticalBounds * speedModifier * screenAspect,
            currentPos.y * horizontalBounds * invert);

        rectTransform.anchoredPosition = new Vector3(Mathf.Clamp(localPos.x, -horizontalBounds, horizontalBounds)
            , Mathf.Clamp(localPos.y, -verticalBounds, verticalBounds), 0f);
    }

    private void AllowClick()
    {
        OnAllowClick.Invoke(true);
    }
    private void DisallowClick()
    {
        OnAllowClick.Invoke(false);
    }
}
