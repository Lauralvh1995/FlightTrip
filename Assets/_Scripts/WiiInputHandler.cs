using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WiiInputHandler : MonoBehaviour
{
    [SerializeField]
    private Vector2 currentCOM;
    [SerializeField]
    private Vector2 previousCOM;

    [SerializeField]
    private float clickTolerance = 0.01f;

    [SerializeField]
    UnityEvent<Vector2> OnInputUpdate;
    [SerializeField]
    UnityEvent<bool> OnAllowClick;

    private float timer = 1;
    private float currentTime;

    private bool canInvokeClick = false;

    void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        if(currentTime > timer)
        {
            canInvokeClick = true;
            currentTime = 0;
        }
        Vector2 input = Wii.GetCenterOfBalance(0);
        previousCOM = currentCOM;
        currentCOM = Vector2.Lerp(currentCOM, input, 0.5f * Time.deltaTime);
        OnInputUpdate.Invoke(currentCOM);

        if (canInvokeClick)
        {
            canInvokeClick = false;
            float distance = Vector2.Distance(previousCOM, currentCOM);
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

    private void AllowClick()
    {
        OnAllowClick.Invoke(true);
    }
    private void DisallowClick()
    {
        OnAllowClick.Invoke(false);
    }
}
