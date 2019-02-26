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
    }


}
