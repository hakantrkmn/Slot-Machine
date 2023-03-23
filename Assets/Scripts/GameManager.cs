using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    
    
    private void OnEnable()
    {
        EventManager.GetGameData += GetGameData;
    }

    private GameData GetGameData()
    {
        return gameData;
    }
}
