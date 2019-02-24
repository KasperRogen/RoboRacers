using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardReader : MonoBehaviour
{
    public Card[] cards = new Card[5];
    int index = 0;


    private void Start()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>().Players.Add(this);
    }

    public void RunCard(int _index)
    {
        index = _index;
        cards[index].Execute(gameObject);
    }


}
