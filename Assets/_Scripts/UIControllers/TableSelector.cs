using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TableSelector : MonoBehaviour
{
    [SerializeField]
    int baseNumber;
    [SerializeField]
    UnityEvent<int> addBaseToSetup;
    [SerializeField]
    UnityEvent<int> removeBaseFromSetup;

    [SerializeField]
    bool selected;

    [SerializeField]
    Image button;

    private void Awake()
    {
        button = GetComponent<Image>();
    }

    public void ButtonClicked()
    {
        if (!selected)
        {
            selected = true;
            addBaseToSetup.Invoke(baseNumber);
            button.color = Color.green;
        }
        else
        {
            selected = false;
            removeBaseFromSetup.Invoke(baseNumber);
            button.color = Color.white;
        }
    }
}
