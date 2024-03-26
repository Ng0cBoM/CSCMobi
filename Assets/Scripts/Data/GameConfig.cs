using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/GameConfig")]
public class GameConfig : ScriptableObject
{
    public List<Level> Levels;
    public int NumberOfQueueSlot = 0;
    public List<GameObject> Cards;
}