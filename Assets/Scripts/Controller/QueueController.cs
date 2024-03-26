using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueController : MonoBehaviour
{
    [SerializeField] private List<Transform> _queueSlot = new List<Transform>();
    public List<CardController> CardInQueue = new List<CardController>();

    private void Awake()
    {
        SignalBus.I.Register<AddCardIntoQueue>(AddCardIntoQueue);
        SignalBus.I.Register<Reset>(ResetQueue);
    }

    private void OnDestroy()
    {
        SignalBus.I.Unregister<AddCardIntoQueue>(AddCardIntoQueue);
        SignalBus.I.Unregister<Reset>(ResetQueue);
    }

    public void AddCardIntoQueue(AddCardIntoQueue signal)
    {
        int slotIndex = SimilarCardIndex(signal.cardController);
        CardInQueue.Insert(slotIndex, signal.cardController);
        CheckAndUpdateQueue();
    }

    private int SimilarCardIndex(CardController card)
    {
        foreach (CardController cardController in CardInQueue)
        {
            if (cardController._cardId == card._cardId)
            {
                return CardInQueue.IndexOf(cardController);
            }
        }
        return CardInQueue.Count;
    }

    private void CheckAndUpdateQueue()
    {
        CheckSimilarCardAndRemove();
        UpdateQueueSlot();
    }

    private void CheckSimilarCardAndRemove()
    {
        if (CardInQueue.Count < 2)
            return;
        int i = 1;
        while (i < CardInQueue.Count - 1)
        {
            CardController currentCard = CardInQueue[i];
            CardController previousCard = CardInQueue[i - 1];
            CardController nextCard = CardInQueue[i + 1];
            if (currentCard._cardId == previousCard._cardId && currentCard._cardId == nextCard._cardId)
            {
                for (int j = 0; j < 3; j++)
                {
                    CardInQueue[i - 1].DestroyCard();
                    CardInQueue.RemoveAt(i - 1);
                }
            }
            i++;
        }
    }

    private void UpdateQueueSlot()
    {
        if (CardInQueue.Count == 0) return;
        for (int i = 0; i < CardInQueue.Count; i++)
        {
            CardInQueue[i].SetUpIntoQueueSlot(_queueSlot[i]);
        }
        CheckGameOver();
    }

    private void ResetQueue(Reset signal)
    {
        foreach (CardController card in CardInQueue)
        {
            Destroy(card.gameObject);
        }
        CardInQueue.Clear();
    }

    private void CheckGameOver()
    {
        if (CardInQueue.Count == DataManager.I.GameConfig.NumberOfQueueSlot)
        {
            UIManager.I.ShowMenu<GameOverUI>();
        };
    }
}