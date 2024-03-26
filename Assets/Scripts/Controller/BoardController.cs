using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : Singleton<BoardController>
{
    private List<CardController> CardList = new List<CardController>();

    private void Awake()
    {
        SignalBus.I.Register<ReleaseCardFromBoard>(ReleseCardAndSetupCardClickAble);
        SignalBus.I.Register<Reset>(ResetCardList);
    }

    private void OnDestroy()
    {
        SignalBus.I.Unregister<ReleaseCardFromBoard>(ReleseCardAndSetupCardClickAble);
        SignalBus.I.Unregister<Reset>(ResetCardList);
    }

    private void ResetCardList(Reset signal)
    {
        foreach (CardController card in CardList)
        {
            Destroy(card.gameObject);
        }
        CardList.Clear();
    }

    private void ReleseCardAndSetupCardClickAble(ReleaseCardFromBoard signal)
    {
        CardList.Remove(signal.cardController);
        if (CardList.Count == 0)
        {
            GameManager.I.IncreLevel();
            return;
        }
        SetupCardClickAble();
    }

    public void AddCardList(CardController card)
    {
        CardList.Add(card);
    }

    public void SetupCardClickAble()
    {
        foreach (var card in CardList)
        {
            card.SetCardClickAble(!CheckCardOverLap(card));
        }
    }

    private bool CheckCardOverLap(CardController card)
    {
        foreach (var otherCard in CardList)
        {
            if (otherCard.CardLayer() <= card.CardLayer())
                continue;
            Collider2D otherCollider2D = otherCard.GetComponent<Collider2D>();
            float distanceToCorner = otherCollider2D.bounds.size.x / Mathf.Sqrt(2);
            float distanceBetweenSquares = Vector2.Distance(card.transform.position, otherCard.transform.position);
            if (distanceBetweenSquares <= distanceToCorner)
            {
                return true;
            }
        }
        return false;
    }
}