using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardIntoQueue : Signal
{
    public CardController cardController;

    public AddCardIntoQueue(CardController cardController)
    {
        this.cardController = cardController;
    }
}

public class ReleaseCardFromBoard : Signal
{
    public CardController cardController;

    public ReleaseCardFromBoard(CardController cardController)
    {
        this.cardController = cardController;
    }
}

public class Reset : Signal
{ }