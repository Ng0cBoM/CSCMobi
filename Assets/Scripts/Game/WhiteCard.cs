using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WhiteCard
{
    public Vector3 CardPosition;
    public int CardLayer;

    public WhiteCard(Vector3 position, int layer)
    {
        this.CardPosition = position;
        this.CardLayer = layer;
    }
}