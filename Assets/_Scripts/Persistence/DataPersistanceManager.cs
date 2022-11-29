using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    [SerializeField]
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;

    public static DataPersistanceManager instance { get; private set; }
    private FileDataHandler dataHandler;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one instance in the scene!");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }


    public void NewGame()
    {
        this.gameData = new GameData();

    }
    [ContextMenu("Create Debug Data")]
    public void CreateDebugData()
    {
        NewGame();
        FindAllDataPersistenceObjects();
        SaveGame();
    }

    public void LoadGame()
    {
        gameData = dataHandler.LoadData();

        if(gameData == null)
        {
            Debug.Log("There was no game data to load. A new game data has to be created first");
            return;
        }
        foreach (IDataPersistence persistenceObj in dataPersistenceObjects)
        {
            persistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        if(this.gameData == null)
        {
            Debug.LogWarning("No game data to save. A new game data has to be created first");
            return;
        }
        foreach (IDataPersistence persistenceObj in dataPersistenceObjects)
        {
            persistenceObj.SaveData(gameData);
        }
        dataHandler.SaveData(gameData);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public bool HasGameData()
    {
        return gameData != null;
    }
}
