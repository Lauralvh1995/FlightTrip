using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnButtonClicked : MonoBehaviour
{
    

    [SerializeField] private Button[] numberButtons;
    [SerializeField] private List<int> selectedNumbers;
    [SerializeField] private TMP_Text selectedNumbersText;

    public void Start()
    {
        selectedNumbers = new List<int>();
        for (int i = 0; i < numberButtons.Length; i++)
        {
            //the value of the loop index must be captured before assigning it to a delegate apparently. This is called the "closure problem"
            int closureIndex = i;

            //the method must be delegate, because the method uses an argument "ToggleButtonClicked(Button clickedButton)" and cannot work otherwise via button click listener
            numberButtons[closureIndex].onClick.AddListener(delegate { ToggleButtonClicked(numberButtons[closureIndex]); }); 
        }
    }
    public void ToggleButtonClicked(Button clickedButton)
    {
        TMP_Text buttonText = clickedButton.GetComponentInChildren<TMP_Text>();

        //if the color is the default white then change it to green, also add the number value of the clicked button to the List<int>, and update the text of the numbers
        if(clickedButton.image.color == Color.white)
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
        selectedNumbers.Sort();
        string updatedNumbersString = "";
        foreach (int n in selectedNumbers)
        {
            if(n == selectedNumbers[0])
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
