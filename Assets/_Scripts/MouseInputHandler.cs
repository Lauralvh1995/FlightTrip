using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseInputHandler : MonoBehaviour
{
    [SerializeField]
    private Vector2 currentCOM;

    [SerializeField]
    UnityEvent<Vector2> OnInputUpdate;

    float screenWidth;
    float screenHeight;

    private void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void FixedUpdate()
    {
        Vector3 rawMousePosition = Input.mousePosition;
        float xInput = (rawMousePosition.x - screenWidth / 2) / screenWidth * 2;
        float yInput = (rawMousePosition.y - screenHeight / 2) / screenHeight * 2;
        Vector2 input = new Vector2(xInput, yInput);
        Debug.Log(input);
        currentCOM = input;
        OnInputUpdate.Invoke(currentCOM);
    }
}
