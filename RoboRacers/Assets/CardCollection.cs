using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollection : MonoBehaviour
{

    public List<Card> Cards;
    List<CardScript> cardScripts;
    private bool isGrabbed = false;

    public delegate void OnCardChanged();
    public OnCardChanged OnCardChangedCallback;


    // Start is called before the first frame update
    void Start()
    {
        cardScripts = new List<CardScript>();
        for(int i = 0; i < transform.childCount; i++)
        {
            CardScript cardScript = transform.GetChild(i).GetComponent<CardScript>();
            if (cardScript != null)
            {
                cardScript.collection = this;
                cardScripts.Add(cardScript);
                cardScript.index = i;
                Cards.Add(cardScript.card);
            }
        }

        OnCardChangedCallback += UpdateUI;
    }

    // Update is called once per frame


    void UpdateUI()
    {
        for(int i = 0; i < Cards.Count; i++)
        {
            cardScripts[i].card = Cards[i];
            cardScripts[i].UpdateUI();
        }
    }


    public bool AddAtIndex(Card card, int index)
    {

        if (Cards[index] != null)
        {
            return false;
        }
        else
        {
            Cards[index] = card;
            if (OnCardChangedCallback != null)
                OnCardChangedCallback.Invoke();
            return true;
        }
    }


    public void RemoveAtIndex(int index)
    {

        Cards[index] = null;

        if (OnCardChangedCallback != null)
            OnCardChangedCallback.Invoke();

    }




}
