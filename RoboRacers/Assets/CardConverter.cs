using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardConverter
{
   

    public static Card GetCard(List<Card> cards, string ID)
    {
        return cards.Find(x => x.ID == ID);
    }
}
