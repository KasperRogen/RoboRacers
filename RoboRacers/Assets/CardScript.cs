using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Card card;
    private bool isGrabbed = false;
    public CardCollection collection;
    public TextMeshProUGUI text;
    public Image image;
    public int index;
    Vector3 prevPos;

    public void ClearSlot()
    {
        card = null;

            image.sprite = null;
            image.enabled = false;
    }

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (card == null)
        {
            text.text = "";
            image.sprite = null;
            image.enabled = false;
        } else
        {
            text.text = card.cardName;
            text.enabled = true;
            image.enabled = true;
        }


        
    }

    void Update()
    {
        if (isGrabbed)
        {
            image.rectTransform.position = Input.mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        prevPos = image.rectTransform.anchoredPosition;
        isGrabbed = true;
        image.raycastTarget = false;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        CardScript slot = null;

        foreach (RaycastResult result in raycastResults)
        {
            if (result.gameObject.GetComponent<CardScript>() != null)
            {
                slot = result.gameObject.GetComponent<CardScript>();
            }
        }

        isGrabbed = false;


        if (!EventSystem.current.IsPointerOverGameObject())
        {
            image.rectTransform.anchoredPosition = prevPos;
        }

        else if (slot != null && slot.card == null)
        {
            slot.collection.AddAtIndex(card, slot.index);

            collection.RemoveAtIndex(index);
        }
        image.rectTransform.anchoredPosition = prevPos;
        //image.rectTransform.anchoredPosition = transform.parent.GetComponent<RectTransform>().anchoredPosition;
        image.raycastTarget = true;
    }


}
