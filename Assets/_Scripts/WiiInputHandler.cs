using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WiiInputHandler : MonoBehaviour
{
    [SerializeField]
    private Vector2 currentCOM;

    [SerializeField]
    UnityEvent<Vector2> OnInputUpdate;

    void Update()
    {
        Vector2 input = Wii.GetCenterOfBalance(0);
        currentCOM = Vector2.Lerp(currentCOM, input, 0.5f * Time.deltaTime);
        OnInputUpdate.Invoke(currentCOM);
    }
}
