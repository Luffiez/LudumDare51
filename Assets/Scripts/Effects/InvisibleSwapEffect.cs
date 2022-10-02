using System.Collections.Generic;
using UnityEngine;

public class InvisibleSwapEffect : Effect
{
    [SerializeField] int swapAmount;
    CardHandler cardHandler;

    void Start()
    {
        cardHandler = GetComponent<CardHandler>();
    }

    public override bool StartEffect()
    {
        var availableCards = GetAvailableCards();
        var selectedCards = GetRandomCards(availableCards);

        SwapParents(selectedCards);

        SoundManager.instance.PlaySfx("Shuffle");
        return true;
    }

    private static void SwapParents(List<GameObject> selectedCards)
    {
        for (int i = selectedCards.Count - 1; i > 0; i -= 2)
        {
            var card = selectedCards[i];

            int rand = Random.Range(0, selectedCards.Count-1);
            var otherCard = selectedCards[rand];

            Transform cardParent = card.transform.parent;
            card.transform.SetParent(otherCard.transform.parent);
            otherCard.transform.SetParent(cardParent);

            card.transform.localPosition = Vector3.zero;
            otherCard.transform.localPosition = Vector3.zero;

            selectedCards.RemoveAt(selectedCards.Count - 1);
            selectedCards.RemoveAt(rand);
        }
    }

    private List<GameObject> GetRandomCards(List<GameObject> availableCards)
    {
        List<GameObject> selectedCards = new List<GameObject>();

        for (int i = 0; i < swapAmount; i++)
        {
            if (availableCards.Count <= 0)
                break;

            int randomIndex = Random.Range(0, availableCards.Count);
            var card = availableCards[randomIndex];
            availableCards.RemoveAt(randomIndex);
            selectedCards.Add(card);
        }

        return selectedCards;
    }

    private List<GameObject> GetAvailableCards()
    {
        List<GameObject> cards = new List<GameObject>();

        for (int i = 0; i < cardHandler.CardObjects.Count; i++)
        {
            var card = cardHandler.CardObjects[i];
            CardBehaviourScript behaviour = card.GetComponent<CardBehaviourScript>();
            if (!behaviour.Completed && !behaviour.Selected)
            {
                cards.Add(card);
            }
        }

        return cards;
    }
}