using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonBehaviour : MonoBehaviour
{
    public GameObject BarekaTopicsMenu;
    [SerializeField]
    private Button[] buttons;

    public Button thisButton;
    public Button btnAdditionTill10;
    public Button BtnSubtractionTill10;
    public Button BtnAddition10Till20;
    public Button BtnSubtraction10Till20;
    public Button BtnBuildingBlocksTill100;
    public Button BtnMultiplicationTables;
    public Button BtnDivisionTables;

    AdditionTill10 additionTill10;

    public bool ButtonPressed = false;

    private void Start()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i];

            //change it 
            button.onClick.AddListener(BeenClicked);
        }
    }

    private void GenerateEquationAdditionTill10()
    {

        additionTill10.GenerateEquation();

    }

    public void BeenClicked()
    {
        ButtonPressed = !ButtonPressed;

        if(ButtonPressed)
        {
            thisButton.image.color = Color.grey;
        }
        else
        {
            thisButton.image.color = Color.white;
        }
    }

    


}
