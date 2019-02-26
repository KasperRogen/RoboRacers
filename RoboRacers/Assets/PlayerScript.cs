using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class PlayerScript : PlayerCardsBehavior
{
    public List<Card> cards;

    public List<Card> allCards;
    public GameObject botPrefab;
    public GameObject bot;
    CardCollection Program;

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
        Program = GameObject.FindGameObjectWithTag("ProgramPanel").GetComponent<CardCollection>();
        Program.OnCardChangedCallback += UpdateCards;
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>().Players.Add(this);
        bot = Instantiate(botPrefab, transform.position, Quaternion.identity);
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


}
