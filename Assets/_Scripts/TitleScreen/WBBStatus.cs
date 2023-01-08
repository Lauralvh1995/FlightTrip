using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class WBBStatus : MonoBehaviour
{
    [SerializeField]
    bool wbbConnected = false;

    [SerializeField]
    TextMeshProUGUI connectedText;

    [SerializeField]
    UnityEvent<bool> enableButtons;

    [SerializeField]
    GameObject errorPopup;

    private void Start()
    {
        ConnectWBB();
    }

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
            enableButtons.Invoke(true);
        }
        else
        {
            connectedText.text = "Geen Balance Board verbonden. Sluit het spel af en verbind het bord." +
                "\n Vraag je leraar om hulp als je er niet uit komt.";

            ShowErrorPopUp();
        }
    }

    private void ShowErrorPopUp()
    {
        errorPopup.SetActive(true);
        enableButtons.Invoke(false);
    }
    public void CloseGame()
    {
        Application.Quit();
    }

    public void ClosePopupBecauseWeHaveConnectionAfterAll()
    {
        errorPopup.SetActive(false);
        enableButtons.Invoke(true);
    }
}
