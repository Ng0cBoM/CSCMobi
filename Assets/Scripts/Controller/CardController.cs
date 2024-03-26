using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardController : MonoBehaviour
{
    public int _cardId { get; private set; }
    public WhiteCard _whiteCard;
    private bool Clickable = false;

    private void OnMouseDown()
    {
        if (Clickable)
        {
            SignalBus.I.FireSignal<AddCardIntoQueue>(new AddCardIntoQueue(this));
            SignalBus.I.FireSignal<ReleaseCardFromBoard>(new ReleaseCardFromBoard(this));
        }
    }

    public int CardLayer()
    {
        return _whiteCard.CardLayer;
    }

    public void SetCardClickAble(bool clickable)
    {
        this.Clickable = clickable;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (!Clickable)
        {
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    public void DestroyCard()
    {
        gameObject.SetActive(false);
    }

    public void SetUpCard(int id, WhiteCard whiteCard)
    {
        this._cardId = id;
        this._whiteCard = whiteCard;
        UpdateCardPosition(_whiteCard.CardPosition);
    }

    public void UpdateCardPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetUpIntoQueueSlot(Transform slot)
    {
        if (slot != null)
        {
            transform.parent = slot;
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
        }
    }
}