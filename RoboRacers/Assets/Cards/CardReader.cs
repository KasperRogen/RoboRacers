using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardReader : MonoBehaviour
{
    public string[] ids = new string[5];
    public Card[] cards = new Card[5];
    public CardCollection Program;
    int index = 0;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>().Players.Add(this);
        Program = GameObject.FindGameObjectWithTag("ProgramPanel").GetComponent<CardCollection>();
        Program.OnCardChangedCallback += UpdateCards;
    }

    public void UpdateCards()
    {

        for (int i = 0; i < Program.Cards.Count; i++)
        {
            cards[i] = Program.Cards[i];
        }
        
    }

    public void RunCard(int _index)
    {

        index = _index;
        cards[index].Execute(gameObject);

    }



}
