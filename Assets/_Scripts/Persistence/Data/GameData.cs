using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public PlayerData playerData;
    public Session latestSession;

    public GameData()
    {
        playerData = new PlayerData();
        latestSession = new Session();
    }
}
