using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerID;
    public string playerName;
    public int playerColor;
    public int planeColor;
    public List<Session> playedSessions;

    //Default data the game will load with if there is no data to load
    public PlayerData()
    {
        playerID = Guid.NewGuid().ToString();
        playerName = "Koos Naamloos";
        playerColor = (int)PlayerColor.WHITE;
        planeColor = (int)PlaneColor.WHITE;
        playedSessions = new List<Session>();
    }
}

public enum PlayerColor
{
    WHITE,
    RED,
    BLUE,
    GREEN,
    YELLOW,
    ORANGE,
    PURPLE
}
public enum PlaneColor
{
    WHITE,
    RED,
    BLUE,
    GREEN,
    YELLOW,
    ORANGE,
    PURPLE
}
