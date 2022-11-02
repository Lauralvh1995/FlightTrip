using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MainMenuManager : MonoBehaviour
{
    ////*GAMEPLAY RELATED STUFF*////

    public GameObject gameManager;
    public GameObject txtEquation;

    ////*UI RELATED STUFF*////

    //holder for all UI elements concerning the Bareka Topics
    public GameObject barekaTopicsMenu;

    //these buttons are concerned with the arithmetic topics for automization based on the "Bareka rekenmuurtje" model 
    public Button btnAdditionTill10;
    public Button btnSubtractionTill10;
    public Button btnAddition10Till20;
    public Button btnSubtraction10Till20;
    public Button btnBuildingBlocksTill100;
    public Button btnMultiplicationTables;
    public Button btnDivisionTables;

    ////holder for all UI elements concerning the arithmetic session
    //public GameObject arithmeticSession;
    //public TMP_Text txtEquation;
    //public Button btnGenerateEquation;
    //[SerializeField] private Button[] answerButtons;

    //holder for all UI elements after clicking multiplication or division tables
    public GameObject numberSelectionMenu;

    //this set concerns the selected numbers for the multiplication and division tables 
    [SerializeField] private Button[] numberButtons;
    [SerializeField] private List<int> selectedNumbers;
    [SerializeField] private TMP_Text selectedNumbersText;
    [SerializeField] private Button btnMultiplyDivide;


    // Start is called before the first frame update
    void Start()
    {
        //create new List of integers on startup
        selectedNumbers = new List<int>();

        //for every numberButton, an onClick listener method is added
        for (int i = 0; i < numberButtons.Length; i++)
        {
            //the value of the loop index must be captured before assigning it to a delegate. This is apparently called the "closure problem"
            int closureIndex = i;

            //the method must be delegate, because the method uses an argument "ToggleButtonClicked(Button clickedButton)"
            //otherwise, it cannot work via button click listener
            numberButtons[closureIndex].onClick.AddListener(delegate { ToggleNumberButtonClicked(numberButtons[closureIndex]); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateAdditionTill10()
    {
        txtEquation.SetActive(true);
        barekaTopicsMenu.SetActive(false);
        gameManager.GetComponent<TempUI>().AdditionTill10 = true;
    }

    public void GenerateSubtractionTill10()
    {
        txtEquation.SetActive(true);
        barekaTopicsMenu.SetActive(false);
        gameManager.GetComponent<TempUI>().SubtractionTill10 = true;
    }

    public void GenerateAddition10Till20()
    {
        txtEquation.SetActive(true);
        barekaTopicsMenu.SetActive(false);
        gameManager.GetComponent<TempUI>().Addition10Till20 = true;
    }

    public void GenerateSubtraction10Till20()
    {
        txtEquation.SetActive(true);
        barekaTopicsMenu.SetActive(false);
        gameManager.GetComponent<TempUI>().Subtraction10Till20 = true;
    }

    public void GenerateBuildingBlocksTill100()
    {
        txtEquation.SetActive(true);
        barekaTopicsMenu.SetActive(false);
        gameManager.GetComponent<TempUI>().BuildingBlocksTill100 = true;
    }

    public void GenerateMultiplicationTables()
    {
        //if there are integers in the list, generate equations
        if (selectedNumbers.Count > 0)
        {
            txtEquation.SetActive(true);
            numberSelectionMenu.SetActive(false);
            MultiplicationTables multiplicationTables = gameManager.GetComponent<TempUI>().multiplicationTablesSession;

            //the script "MultiplicationTables.cs" has a method that will pass along a List of <Integers> that the user selected in the UI
            multiplicationTables.UpdateListSelectedNumbers(selectedNumbers);
            gameManager.GetComponent<TempUI>().MultiplicationTables = true;
        }
        else
        {
            selectedNumbersText.text = "Kies een nummer!";
        }
    }

    public void GenerateDivisionTables()
    {
        //if there are integers in the list, generate equations
        if (selectedNumbers.Count > 0)
        {
            txtEquation.SetActive(true);
            numberSelectionMenu.SetActive(false);
            DivisionTables divisionTables = gameManager.GetComponent<TempUI>().divisionTablesSession;
            
            //the script "DivisionTables.cs" has a method that will pass along a List of <Integers> that the user selected in the UI
            divisionTables.UpdateListSelectedNumbers(selectedNumbers);
            gameManager.GetComponent<TempUI>().DivisionTables = true;
        }
        else
        {
            selectedNumbersText.text = "Kies een nummer!";
        }
    }

    //go to the Number Selection UI
    public void GoToNumberSelectionMenu()
    {
        barekaTopicsMenu.SetActive(false);
        numberSelectionMenu.SetActive(true);
        string clickedButtonName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(EventSystem.current.currentSelectedGameObject.name + " was clicked");
        if (clickedButtonName == "BtnMultiplicationTables")
        {
            btnMultiplyDivide.onClick.AddListener(delegate { GenerateMultiplicationTables(); });
        }
        if (clickedButtonName == "BtnDivisionTables")
        {
            btnMultiplyDivide.onClick.AddListener(delegate { GenerateDivisionTables(); });
        }
    }


    public void BtnBackFromNumberSelect()
    {
        numberSelectionMenu.SetActive(false);
        barekaTopicsMenu.SetActive(true);
    }

    public void ToggleNumberButtonClicked(Button clickedButton)
    {
        TMP_Text buttonText = clickedButton.GetComponentInChildren<TMP_Text>();

        //if the color is the default white then change it to green, also add the number value of the clicked button to the List<int>, and update the text of the numbers
        if (clickedButton.image.color == Color.white)
        {
            clickedButton.image.color = Color.green;
            selectedNumbers.Add(int.Parse(buttonText.text));
            UpdateNumberText();
            return;
        }

        //otherwise, change color back to default, remove the number from the List<int> and update the text again
        clickedButton.image.color = Color.white;
        selectedNumbers.Remove(int.Parse(buttonText.text));
        UpdateNumberText();
    }

    //update the selected numbers in the text of the game
    public void UpdateNumberText()
    {
        //first sort the List<int> in numerical order
        selectedNumbers.Sort();

        string updatedNumbersString = "";

        foreach (int n in selectedNumbers)
        {
            if (n == selectedNumbers[0])
            {
                updatedNumbersString += n.ToString() + " "; ;
            }
            else
            {
                updatedNumbersString += ", " + n.ToString() + " "; ;
            }
        }
        selectedNumbersText.text = updatedNumbersString;
    }

}
