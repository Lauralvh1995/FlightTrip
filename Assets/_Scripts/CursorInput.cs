using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorInput : MonoBehaviour
{
    [SerializeField]
    private int clickCounter = 0;
    [SerializeField]
    private bool allowedToClick= false;
    [SerializeField]
    private Transform planeTransform;
    [SerializeField]
    private Camera uiCamera;
    [SerializeField]
    private LayerMask uiMask;

    [SerializeField] GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    [SerializeField] EventSystem eventSystem;


    [SerializeField] 
    Button toBeClicked;

    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        raycaster = GetComponentInParent<GraphicRaycaster>();
    }

    private void Update()
    {
        if (allowedToClick)
        {
            Vector3 screenPos = uiCamera.WorldToScreenPoint(planeTransform.position);
            //Set up the new Pointer Event
            pointerEventData = new PointerEventData(eventSystem);
            //Set the Pointer Event Position to that of the game object
            pointerEventData.position = screenPos;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            EventSystem.current.RaycastAll(pointerEventData, results);

            if (results.Count > 0) Debug.Log("Hit " + results[0].gameObject.name);
            foreach(RaycastResult result in results)
            {
                if (result.gameObject?.GetComponent<Button>())
                {
                    Button button = result.gameObject.GetComponent<Button>();
                    button.onClick.Invoke();
                    allowedToClick = false;
                }
            }

            //check if plane is overlapping a button
            //if input handler gives OK for input, call click
        }
    }
    public void UpdateClick(bool status)
    {
        if (status)
        {
            clickCounter++;
            if(clickCounter == 3)
            {
                allowedToClick = true;
            }
        }
        else
        {
            allowedToClick = false;
            clickCounter = 0;
        }
    }
}
