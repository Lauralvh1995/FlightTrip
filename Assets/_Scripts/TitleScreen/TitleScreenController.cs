using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleScreenController : MonoBehaviour, IDataPersistence
{
    [Header("UI Buttons")]
    [SerializeField]
    Button characterCreationButton;
    [SerializeField]
    Button startGameButton;
    [SerializeField]
    Button instructionsButton;

    [Header("ScreenText")]
    [SerializeField]
    TextMeshProUGUI welcomeText;

    [Header("Popups")]
    [SerializeField]
    GameObject instructionsPopUp;

    [Header("Player Data")]
    [SerializeField]
    PlayerData playerData;

    private void Start()
    {
        if (DataPersistanceManager.instance.HasGameData())
        {
            SetScreenTextAndAvatar(playerData);
        }
        else
        {
            DataPersistanceManager.instance.NewGame();
            //direct them to character creation first
        }
    }

    private void SetScreenTextAndAvatar(PlayerData playerData)
    {
        //Put in a plane and pilot flying around, resembling the avatar
        UpdateScreenText(playerData.playerName);
    }

    public void EnableUIButtons(bool status)
    {
        startGameButton.interactable = status;
        characterCreationButton.interactable = status;
        instructionsButton.interactable = status;
    }

    public void LoadData(GameData gameData)
    {
        this.playerData = gameData.playerData;
    }

    public void SaveData(GameData gameData)
    {
        //This scene doesn't need to save anything
    }

    private void UpdateScreenText(string playername)
    {
        welcomeText.text = "Welkom, \n" + playername;
    }

    public void GoToScene(string sceneName)
    {
        DataPersistanceManager.instance.SaveGame();
        SceneManager.LoadScene(sceneName);
    }

    public void ShowInstructions()
    {
        instructionsPopUp.SetActive(true);
    }
}
