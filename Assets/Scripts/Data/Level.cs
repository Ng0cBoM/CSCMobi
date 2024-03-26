using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Level")]
public class Level : ScriptableObject
{
    public List<WhiteCard> WhiteCards = new List<WhiteCard>();
}