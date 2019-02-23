using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReader : MonoBehaviour
{
    public Card[] cards = new Card[5];
    int index = 0;

    void Start()
    {
        cards[0].Execute(gameObject);
    }

    private void Update()
    {

        cards[index].Tick();
        
        if (cards[index].IsDone)
        {
            index++;
            cards[index].Execute(gameObject);
        }
    }

}
