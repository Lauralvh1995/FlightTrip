using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WBBStatus : MonoBehaviour
{
    [SerializeField]
    bool wbbConnected = false;

    [SerializeField]
    TextMeshProUGUI connectedText;

    [SerializeField]
    Button characterCreationButton;
    [SerializeField]
    Button startGameButton;

    public void ConnectWBB()
    {
        Wii.StartSearch();

        if (Wii.GetExpType(0) == 3)
        {
            wbbConnected = true;
        }

        UpdateConnectedText(wbbConnected);
    }

    private void UpdateConnectedText(bool status)
    {
        if (status)
        {
            connectedText.text = "Balance Board verbonden!";
            EnableUIButtons();
        }
        else
        {
            connectedText.text = "Geen Balance Board verbonden. Sluit het spel af en verbind het bord." +
                "\n Vraag je leraar om hulp als je er niet uit komt.";
        }
    }

    private void EnableUIButtons()
    {
        startGameButton.interactable = true;
        characterCreationButton.interactable = true;
    }
}
