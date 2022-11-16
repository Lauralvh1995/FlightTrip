using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DifficultySelector : MonoBehaviour
{
    [SerializeField]
    Difficulty setDifficulty;

    [SerializeField]
    bool selected;

    [SerializeField]
    Image button;

    [SerializeField]
    UnityEvent<Difficulty> OnDifficultySelect;

    private void Awake()
    {
        button = GetComponent<Image>();
    }

    public void ButtonClicked()
    {
        if (!selected)
        {
            selected = true;
            OnDifficultySelect.Invoke(setDifficulty);
            button.color = Color.green;
        }
    }

    public void DisableThisChoice()
    {
        selected = false;
        button.color = Color.white;
    }
}
