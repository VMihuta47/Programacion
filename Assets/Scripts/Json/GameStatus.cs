using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameStatus
{
    public List<int> gameData;

    public GameStatus()
    {
        gameData = new List<int>();
    }
}
