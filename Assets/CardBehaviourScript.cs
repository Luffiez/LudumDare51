using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CardBehaviourScript : MonoBehaviour, IPointerDownHandler
{
    int pairId = -1;
    bool selected = false;
    bool completed = false;
    CardHandler CardHandler;
    public bool Selected { get { return selected; } set { selected = value; } }
    public int PairId { get { return pairId; } set { pairId = value; } }
    public bool Completed { get { return completed; } set { completed = value; } }

    public void SetCardHandler(CardHandler cardHandler)
    {
        this.CardHandler = cardHandler;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (selected == false && completed == false)
        {
            CardHandler.AddToMatch(this);
        }
    }
}
