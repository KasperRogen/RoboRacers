using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public List<CardReader> Players;
    public List<BotMovementController> botControllers;
    int cardIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Players.ForEach(x => x.RunCard(cardIndex));
        cardIndex += 1;
    }

    // Update is called once per frame
    void Update()
    {

        if(botControllers.TrueForAll(x => x.IsDone))
        {
            if(cardIndex >= 5)
            {
                cardIndex = 0;
                //Start planning phase
            } else
            {
                Players.ForEach(x => x.RunCard(cardIndex));
                cardIndex += 1;
            }
        }
    }
}





public enum GamePhase
{
    PLANNING,
    EXECUTING
}