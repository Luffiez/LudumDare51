using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchHandler : MonoBehaviour
{
    CardBehaviourScript card1;

    // Update is called once per frame
    public void AddCard(CardBehaviourScript cardState)
    {
        
        if (cardState == null)
            Debug.LogError("the card is missing cardstates component");
        if (cardState.Completed || cardState.Selected)
        {
            return;
        }
        if (card1 == null)
        {
            card1 = cardState;
            card1.Selected=true;
        }
        else
        {
            CardBehaviourScript card2 = cardState;
            card2.Selected = true;
            if (card1.PairId == card2.PairId)
            {
                card1.Completed = true;
                card1.Selected = false;
                card2.Completed = true;
                card2.Selected = false;
            }
            else
            {
                card2.Selected = false;
                card1.Selected = false;
                card1 = null;
            }
        }

    }
}
