using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;

public class PlayerScript : PlayerCardsBehavior
{
    public List<Card> cards;
    GamePhase phase;
    public List<Card> allCards;
    public GameObject botPrefab;
    public GameObject bot;
    CardCollection Program;
    CardCollection CardSelection;

    public override void UploadCards(RpcArgs args)
    {
        List<string> Ids = args.GetNext<string>().Split(',').ToList();

        for (int i = 0; i < Ids.Count; i++)
        {
            cards[i] = CardConverter.GetCard(allCards, Ids[i]);
        }
    }



    public void RunCard(int _index)
    {
        cards[_index].Execute(bot);
    }




    // Start is called before the first frame update
    void Start()
    {
        if(NetworkManager.Instance.IsServer == false) { 
            Program = GameObject.FindGameObjectWithTag("ProgramPanel").GetComponent<CardCollection>();
            CardSelection = GameObject.FindGameObjectWithTag("CardSelection").GetComponent<CardCollection>();
            Program.OnCardChangedCallback += UpdateCards;
        } else
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>().Players.Add(this);
            bot = Instantiate(botPrefab, transform.position, Quaternion.identity, transform);
        }

        
    }


    public void UpdateCards()
    {
        if(networkObject.IsOwner == false)
        {
            return;
        }

        for (int i = 0; i < Program.Cards.Count; i++)
        {
            cards[i] = Program.Cards[i];
        }


        List<string> Ids = cards.Where(x => x != null).Select(x => x.ID).ToList();
        networkObject.SendRpc(RPC_UPLOAD_CARDS, Receivers.Server, string.Join(",", Ids));


    }

    public void UpdateGamePhase(GamePhase phase)
    {
        networkObject.SendRpc(RPC_SET_GAME_PHASE, Receivers.Owner, (int)phase );
    }

    public void DownloadCards(string cardIds)
    {
        networkObject.SendRpc(RPC_DOWNLOAD_CARDS, Receivers.Owner, cardIds);
    }


    public override void SetGamePhase(RpcArgs args)
    {
        phase = (GamePhase)args.GetNext<int>();
    }




    public override void DownloadCards(RpcArgs args)
    {
        List<string> Ids = args.GetNext<string>().Split(',').ToList();

        if (networkObject.IsServer == false)
        {
            for (int i = 0; i < Program.Cards.Count; i++)
            {
                Program.Cards[i] = null;
            }


            for (int i = 0; i < Ids.Count; i++)
            {
                CardSelection.Cards[i] = CardConverter.GetCard(allCards, Ids[i]);
            }

            if (Program.OnCardChangedCallback != null)
                Program.OnCardChangedCallback.Invoke();

            if (CardSelection.OnCardChangedCallback != null)
                CardSelection.OnCardChangedCallback.Invoke();



        }
        else
        {
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i] = null;
            }
        }


    }
}
