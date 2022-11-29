using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorChanger : MonoBehaviour
{
    [SerializeField]
    Material materialToSet;

    [SerializeField]
    UnityEvent<Material> materialChange;

    public void OnButtonClicked()
    {
        materialChange.Invoke(materialToSet);
    }
}
