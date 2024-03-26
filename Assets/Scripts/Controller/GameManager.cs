using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private Transform _board;

    private GameConfig _gameConfig;

    private void Start()
    {
        _gameConfig = DataManager.I.GameConfig;
        LoadLevel();
    }

    private void LoadLevel()
    {
        Level levelData = _gameConfig.Levels[currentLevel];
        SpawnCardBaseOnLevelData(levelData);
    }

    private void SpawnCardBaseOnLevelData(Level levelData)
    {
        int cardId = 0;
        List<int> indexList = IndexList(levelData);
        while (indexList.Count > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                int randomIndex = Random.RandomRange(0, indexList.Count);
                GameObject card = Instantiate(_gameConfig.Cards[cardId], _board);
                CardController cardController = card.GetComponent<CardController>();
                cardController.SetUpCard(cardId, levelData.WhiteCards[indexList[randomIndex]]);
                BoardController.I.AddCardList(cardController);
                indexList.RemoveAt(randomIndex);
            }
            if (cardId == _gameConfig.Cards.Count - 1)
                cardId = 0;
            else cardId++;
        }
        BoardController.I.SetupCardClickAble();
    }

    private List<int> IndexList(Level levelData)
    {
        List<int> indexList = new List<int>();
        for (int i = 0; i < levelData.WhiteCards.Count; i++) indexList.Add(i);
        return indexList;
    }
}