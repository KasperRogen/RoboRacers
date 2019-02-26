using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class PlayerScript : PlayerCardsBehavior
{
    public List<Card> cards;
    CardReader cardReader;

    public List<Card> allCards;

    public override void UploadCards(RpcArgs args)
    {
        List<string> Ids = args.GetNext<string>().Split(',').ToList();

        for (int i = 0; i < Ids.Count; i++)
        {
            cards[i] = CardConverter.GetCard(allCards, Ids[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cardReader = GetComponent<CardReader>();
        cardReader.Program.OnCardChangedCallback += UpdateCards;
    }


    public void UpdateCards()
    {
        if(networkObject.IsOwner == false)
        {
            return;
        }

        for (int i = 0; i < cardReader.Program.Cards.Count; i++)
        {
            cards[i] = cardReader.Program.Cards[i];
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (networkObject.IsOwner == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && networkObject.IsOwner)
        {
            List<string> Ids = cards.Where(x => x != null).Select(x => x.ID).ToList();
            networkObject.SendRpc(RPC_UPLOAD_CARDS, Receivers.Server, string.Join(",", Ids));
        }
    }
}
