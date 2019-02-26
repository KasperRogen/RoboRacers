using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public List<PlayerScript> Players;
    public List<BotMovementController> botControllers;
    int cardIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnGUI()
    {
        if(GUI.Button(new Rect(10, 10, 50, 50), "Start"))
        {
            Players.ForEach(x => x.RunCard(cardIndex));
            cardIndex += 1;
        }
    }


    // Update is called once per frame
    void Update()
    {

        if(botControllers.Count > 0 && botControllers.TrueForAll(x => x.IsDone))
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