using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(CursorMovement))]
public class CursorInput : MonoBehaviour
{
    [SerializeField]
    private int clickCounter = 0;
    [SerializeField]
    private bool allowedToClick= false;
    [SerializeField]
    private LayerMask uiMask;

    //[SerializeField] GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] RectTransform rectTransform;

    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        //raycaster = GetComponentInParent<GraphicRaycaster>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (allowedToClick)
        {
            //get the position of the cursor
            Vector3 screenPos = rectTransform.position;
            //Set up the new Pointer Event
            pointerEventData = new PointerEventData(eventSystem);
            //Set the Pointer Event Position to that of the cursor
            pointerEventData.position = screenPos;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and simulated mouse click position
            EventSystem.current.RaycastAll(pointerEventData, results);

            if (results.Count > 0) Debug.Log("Hit " + results[0].gameObject.name);
            foreach(RaycastResult result in results)
            {
                if (result.gameObject?.GetComponent<Button>())
                {
                    Button button = result.gameObject.GetComponent<Button>();
                    allowedToClick = false;
                    clickCounter = 0;
                    //actually click
                    button.onClick.Invoke();
                }
            }
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
