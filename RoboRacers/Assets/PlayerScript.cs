using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class PlayerScript : PlayerCardsBehavior
{
    public List<Card> cards;
    CardReader cardReader;

    public override void UploadCards(RpcArgs args)
    {
        Debug.Log(args.GetNext<string>());
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
            networkObject.SendRpc(RPC_UPLOAD_CARDS, Receivers.Server, JsonUtility.ToJson(cards[0]));
        }
    }
}
