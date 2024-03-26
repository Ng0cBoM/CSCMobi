using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateLevels : MonoBehaviour
{
    [SerializeField] private List<GameObject> _layerContain;
    [SerializeField] private int levelIndex = 0;

    private void Start()
    {
        Level level = ScriptableObject.CreateInstance<Level>();
        for (int i = 1; i <= _layerContain.Count; i++)
        {
            foreach (Transform chill in _layerContain[i - 1].transform)
            {
                WhiteCard newCard = new WhiteCard(chill.position, i);
                level.WhiteCards.Add(newCard);
            }
        }
        SaveLevelData(level);
    }

    private void SaveLevelData(Level asset)
    {
        AssetDatabase.CreateAsset(asset, $"Assets/Resources/Levels/level{levelIndex}.asset");
        AssetDatabase.SaveAssets();
    }
}