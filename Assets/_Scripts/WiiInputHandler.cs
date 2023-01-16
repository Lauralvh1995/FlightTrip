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

    void FixedUpdate()
    {
        Vector2 input = Wii.GetCenterOfBalance(0); //Change this if you want a different source of COG input than WiiBuddy
        currentCOM = input;
        //currentCOM = Vector2.Lerp(currentCOM, input, 0.9f * Time.deltaTime);
        OnInputUpdate.Invoke(currentCOM);
    }
}
