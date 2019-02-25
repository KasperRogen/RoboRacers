using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;

public class CardReader : MonoBehaviour
{
    public string[] ids = new string[5];
    public Card[] cards = new Card[5];
    public CardCollection Program;
    int index = 0;
    PlayerScript player;

    private void Start()
    {
        player = GetComponent<PlayerScript>();
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>().Players.Add(this);
        Program = GameObject.FindGameObjectWithTag("ProgramPanel").GetComponent<CardCollection>();
    }

    public void RunCard(int _index)
    {

        index = _index;
        cards[index].Execute(gameObject);

    }



}
