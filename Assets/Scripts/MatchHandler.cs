using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LDJAM51.UI;

public class MatchHandler : MonoBehaviour
{
    CardBehaviourScript card1;
    CardBehaviourScript card2;
    [SerializeField]
    float clearTime =2;
    float clearTimer =0;
    bool clearCards = false;
    bool flipCards = false;

    [SerializeField] AudioClip addClip;
    [SerializeField] AudioClip matchClip;
    [SerializeField] AudioClip wrongClip;

    // Update is called once per frame
    public bool AddCard(CardBehaviourScript cardState)
    {
        if (cardState == null)
            Debug.LogError("the card is missing cardstates component");
        if (cardState.Completed || cardState.Selected ||clearCards)
        {
            return false;
        }
        if (card1 == null)
        {
            card1 = cardState;
            card1.GetComponent<InteractableCardUI>().FlipCard();
            card1.Selected=true;
            SoundManager.instance.PlaySfx(addClip, 0.7f);
        }
        else
        {
            card2 = cardState;
            card2.Selected = true;
            card2.GetComponent<InteractableCardUI>().FlipCard();
            if (card1.PairId == card2.PairId)
            {
                card1.Completed = true;
                card1.Selected = false;
                card2.Completed = true;
                card2.Selected = false;
                card1 = null;
                card2 = null;
                clearCards = false;
                SoundManager.instance.PlaySfx(matchClip, 0.7f);
                return true;
            }
            else
            {
                card2.Selected = false;
                card1.Selected = false;
                clearCards = true;
                clearTimer = Time.time + clearTime;
                SoundManager.instance.PlaySfx(wrongClip, 0.7f);
                flipCards = false;
            }
        }
        return false;
    }

    private void Update()
    {
        if (clearCards == true && clearTimer < Time.time)
        {
            if (!flipCards)
            {
                card1.GetComponent<InteractableCardUI>().FlipCard();
                card2.GetComponent<InteractableCardUI>().FlipCard();
                flipCards = true;
            }
            else
            {
                if (!card1.GetComponent<InteractableCardUI>().IsFacingUp && !card1.GetComponent<InteractableCardUI>().IsFlipping && !card2.GetComponent<InteractableCardUI>().IsFacingUp && !card2.GetComponent<InteractableCardUI>().IsFlipping)
                {
                    card1 = null;
                    card2 = null;
                    clearCards = false;
                }
            }
        }
    }
}
