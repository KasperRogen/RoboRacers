using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public List<PlayerScript> Players;
    public List<Card> possibleCards;
    public List<BotMovementController> botControllers;
    public GamePhase phase;
    int cardIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnGUI()
    {
        if(GUI.Button(new Rect(10, 10, 50, 50), "Start"))
        {
            phase = GamePhase.EXECUTING;
            Players.ForEach(x => x.RunCard(cardIndex));
            cardIndex += 1;
        }
    }


    string GenerateCards()
    {
        List<string> cardIds = new List<string>();
        for(int i = 0; i < 10; i++)
        {
            cardIds.Add(possibleCards[Random.Range(0, possibleCards.Count-1)].ID);
        }

        return string.Join(",", cardIds);
    }




    // Update is called once per frame
    void Update()
    {

        if(botControllers.Count > 0 && botControllers.TrueForAll(x => x.IsDone) && phase == GamePhase.EXECUTING)
        {
            if(cardIndex >= 5)
            {
                cardIndex = 0;
                phase = GamePhase.PLANNING;
                Players.ForEach(x => x.UpdateGamePhase(phase));
                Players.ForEach(x => x.DownloadCards(GenerateCards()));
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