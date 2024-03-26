using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private GameConfig _config;
    private const string GAME_CONFIG_PATH = "GameConfig";

    public GameConfig GameConfig
    {
        get
        {
            if (_config == null)
            {
                _config = LoadDataFromScriptableObjectInResources<GameConfig>(GAME_CONFIG_PATH);
            }
            return _config;
        }
    }

    private T LoadDataFromScriptableObjectInResources<T>(string filePath) where T : ScriptableObject
    {
        var dataObject = Resources.Load<T>(filePath);
        if (dataObject == null)
        {
            Debug.LogWarning("Failed to load " + filePath);
        }
        return dataObject;
    }
}