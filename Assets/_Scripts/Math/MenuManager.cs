using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
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

    //holder for all UI elements concerning the arithmetic session
    public GameObject arithmeticSession;
    public TMP_Text txtEquation;
    public Button btnGenerateEquation;
    [SerializeField] private Button[] answerButtons;

    //holder for all UI elements after clicking multiplication or division tables
    public GameObject numberSelectionMenu;

    //this set concerns the selected numbers for the multiplication and division tables 
    [SerializeField] private Button[] numberButtons;
    [SerializeField] private List<int> selectedNumbers;
    [SerializeField] private TMP_Text selectedNumbersText;
    [SerializeField] private Button btnMultiplyDivide;

    List<int> listRandomizedValues = new List<int>();


    public void Start()
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
            numberButtons[closureIndex].onClick.AddListener(delegate { ToggleButtonClicked(numberButtons[closureIndex]); });
        }
    }

    //turns off Bareka Topics UI and turns on arithmeticSession UI
    public void GoToArithmeticSession()
    {
        barekaTopicsMenu.SetActive(false);
        numberSelectionMenu.SetActive(false);
        arithmeticSession.SetActive(true);
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
        if(clickedButtonName == "BtnDivisionTables")
        {
            btnMultiplyDivide.onClick.AddListener(delegate { GenerateDivisionTables(); });
        }
    }

    public void BtnBack()
    {
        arithmeticSession.SetActive(false);
        barekaTopicsMenu.SetActive(true);
        btnGenerateEquation.onClick.RemoveAllListeners();
        StopAllCoroutines();
    }

    public void BtnBackFromNumberSelect()
    {
        numberSelectionMenu.SetActive(false);
        barekaTopicsMenu.SetActive(true);
    }


    //Generate an equation based on the bareka topic that was selected
    public void GenerateEquationUI(Equation eq)
    {
        if (arithmeticSession.activeSelf != true)
        {
            GoToArithmeticSession();
        }
        eq.GenerateEquation();
        txtEquation.text = eq.GenerateEquationToString();

        //randomize a number between 0, 1 and 2 and save these locations
        int randomAnswerLocation1 = UniqueRandomInt(0, 3);
        int randomAnswerLocation2 = UniqueRandomInt(0, 3);
        int randomAnswerLocation3 = UniqueRandomInt(0, 3);

        //THIS IS FOR GETTING THE EQUATION TYPE
        Debug.Log(eq.GetType());


        //one of three random answerButtons have their text changed to the correct answer
        answerButtons[randomAnswerLocation1].GetComponentInChildren<TMP_Text>().text = eq.GetCorrectAnswer().ToString();

        //the remaining two buttons will have fake answers
        int fakeAnswer1 = eq.GetSimilarAnswer();
        int fakeAnswer2 = eq.GetSimilarAnswer();

        while (fakeAnswer2 == fakeAnswer1)
        {
            //if both fake answers are the same, run it again
            fakeAnswer2 = eq.GetSimilarAnswer();
        }

        //update the remaining two answerButton's UI text to fake answers
        answerButtons[randomAnswerLocation2].GetComponentInChildren<TMP_Text>().text = fakeAnswer1.ToString();
        answerButtons[randomAnswerLocation3].GetComponentInChildren<TMP_Text>().text = fakeAnswer2.ToString();

        //for every answerButton, an onClick listener method is added to check what answer the player gives
        for (int i = 0; i < answerButtons.Length; i++)
        {
            //the value of the loop index must be captured before assigning it to a delegate. This is apparently called the "closure problem"
            int closureIndex = i;
            //the method must be delegate, because the method uses two arguments for "AnswerButtonClicked(Button clickedButton, Equation equation)"
            //otherwise, it cannot work via button click listener
            answerButtons[closureIndex].onClick.AddListener(delegate { AnswerButtonClicked(answerButtons[closureIndex], eq); });

            //return all answerButtons back to normal color, if they weren't already
            answerButtons[closureIndex].image.color = Color.white;
        }
    }

    public void GenerateAdditionTill10()
    {
        AdditionTill10 additionTill10 = new AdditionTill10();
        GenerateEquationUI(additionTill10);
        btnGenerateEquation.onClick.AddListener(delegate { GenerateEquationUI(additionTill10); });
    }

    public void GenerateSubtractionTill10()
    {
        SubtractionTill10 subtractionTill10 = new SubtractionTill10();
        GenerateEquationUI(subtractionTill10);
        btnGenerateEquation.onClick.AddListener(delegate { GenerateEquationUI(subtractionTill10); });
    }

    public void GenerateAddition10Till20()
    {
        Addition10Till20 addition10Till20 = new Addition10Till20();
        GenerateEquationUI(addition10Till20);
        btnGenerateEquation.onClick.AddListener(delegate { GenerateEquationUI(addition10Till20); });
    }

    public void GenerateSubtraction10Till20()
    {
        Subtraction10Till20 subtraction10Till20 = new Subtraction10Till20();
        GenerateEquationUI(subtraction10Till20);
        btnGenerateEquation.onClick.AddListener(delegate { GenerateEquationUI(subtraction10Till20); });
    }

    public void GenerateBuildingBlocksTill100()
    {
        BuildingBlocksTill100 buildingBlocksTill100 = new BuildingBlocksTill100();
        GenerateEquationUI(buildingBlocksTill100);
        btnGenerateEquation.onClick.AddListener(delegate { GenerateEquationUI(buildingBlocksTill100); });
    }

    public void GenerateMultiplicationTables()
    {
        //if there are integers in the list, generate equations
        if(selectedNumbers.Count > 0)
        {
            MultiplicationTables multiplicationTables = new MultiplicationTables();
            //the script "MultiplicationTables.cs" has a method that will pass along a List of <Integers> that the user selected in the UI
            multiplicationTables.UpdateListSelectedNumbers(selectedNumbers);
            GenerateEquationUI(multiplicationTables);
            btnGenerateEquation.onClick.AddListener(delegate { GenerateEquationUI(multiplicationTables); });
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
            DivisionTables divisionTables = new DivisionTables();
            //the script "DivisionTables.cs" has a method that will pass along a List of <Integers> that the user selected in the UI
            divisionTables.UpdateListSelectedNumbers(selectedNumbers);
            GenerateEquationUI(divisionTables);
            btnGenerateEquation.onClick.AddListener(delegate { GenerateEquationUI(divisionTables); });
        }
        else
        {
            selectedNumbersText.text = "Kies een nummer!";
        }
    }

    public int UniqueRandomInt(int min, int max)
    {
        int val = Random.Range(min, max);
        while (listRandomizedValues.Contains(val))
        {
            val = Random.Range(min, max);
        }
        listRandomizedValues.Add(val);

        //if the list is full, clear it
        if (listRandomizedValues.Count >= max)
        {
            listRandomizedValues.Clear();
        }

        return val;
    }

    public void ToggleButtonClicked(Button clickedButton)
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

    public void AnswerButtonClicked(Button clickedButton, Equation equation)
    {
        //get the current button's text component
        TMP_Text answerText = clickedButton.GetComponentInChildren<TMP_Text>();

        //update the given answer via the text component and reveal the true answer
        equation.GivenAnswer = int.Parse(answerText.text);
        txtEquation.text = equation.GenerateEquationToString() + equation.GetCorrectAnswer().ToString();

        //change button color to reveal if it was the correct or incorrect answer
        if(equation.isCorrect() == true)
        {
            clickedButton.image.color = Color.green;
        }
        else
        {
            clickedButton.image.color = Color.red;
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            //the value of the loop index must be captured before assigning it to a delegate. This is apparently called the "closure problem"
            int closureIndex = i;
            string answerButtonText = answerButtons[closureIndex].GetComponentInChildren<TMP_Text>().text;
            if (answerButtonText == equation.GetCorrectAnswer().ToString())
            {
                answerButtons[closureIndex].image.color = Color.green;
            }
        }
        StartCoroutine(ShowAnswerForFiveSeconds(equation));
    }

    private IEnumerator ShowAnswerForFiveSeconds(Equation equation)
    {
        //remove button click functions
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].onClick.RemoveAllListeners();
        }
        yield return new WaitForSeconds(3);

        GenerateEquationUI(equation);
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
