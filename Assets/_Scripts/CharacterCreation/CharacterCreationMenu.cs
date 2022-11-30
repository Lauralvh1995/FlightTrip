using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCreationMenu : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    PlayerData playerData;

    [SerializeField]
    bool editingPlayer = true;

    [SerializeField]
    Camera playerCamera;
    [SerializeField]
    Camera planeCamera;

    [SerializeField]
    Button editPlayerButton;
    [SerializeField]
    Button editPlaneButton;

    [SerializeField]
    UnityEvent<Material> planeChange;
    [SerializeField]
    UnityEvent<Material> playerChange;

    [SerializeField]
    List<Material> colorMaterials;
    private void Awake()
    {
        EnableDisablePlayer(editingPlayer);
    }

    public void ChangeColor(Material mat)
    {
        string matName = mat.name;
        int colorIndex = 0;
        switch (matName)
        {
            case "Red":
                colorIndex = 1;
                break;
            case "Blue":
                colorIndex = 2;
                break;
            case "Green":
                colorIndex = 3;
                break;
            case "Yellow":
                colorIndex = 4;
                break;
            case "Orange":
                colorIndex = 5;
                break;
            case "Purple":
                colorIndex = 6;
                break;
        }
        if (editingPlayer)
        {
            playerData.playerColor = colorIndex;
            playerChange.Invoke(mat);
        }
        else
        {
            playerData.planeColor = colorIndex;
            planeChange.Invoke(mat);
        }
    }

    public void EnableDisablePlayer(bool status)
    {
        editingPlayer = status;
        if (editingPlayer)
        {
            playerCamera.gameObject.SetActive(true);
            planeCamera.gameObject.SetActive(false);
            editPlayerButton.gameObject.SetActive(false);
            editPlaneButton.gameObject.SetActive(true);
        }
        else
        {
            playerCamera.gameObject.SetActive(false);
            planeCamera.gameObject.SetActive(true);
            editPlayerButton.gameObject.SetActive(true);
            editPlaneButton.gameObject.SetActive(false);
        }
    }

    public void Done()
    {
        DataPersistanceManager.instance.SaveGame();
        SceneManager.LoadScene("TitleScreen");
    }

    public void LoadData(GameData gameData)
    {
        playerData = gameData.playerData;
        SetUpGraphics(playerData.planeColor, playerData.playerColor);
    }

    public void SaveData(GameData gameData)
    {
        gameData.playerData = playerData;
    }

    public void SetUpGraphics(int planeColor, int playerColor)
    {
        planeChange.Invoke(colorMaterials[planeColor]);
        playerChange.Invoke(colorMaterials[playerColor]);
    }
}
