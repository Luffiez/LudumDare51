using System.Collections.Generic;
using UnityEngine;

public class SwapPairImagesEffect : Effect
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

        var cardPairs = GetPairs(selectedCards);

        SwapPairImages(cardPairs);

        SoundManager.instance.PlaySfx("Shuffle");
        return true;
    }

    private void SwapPairImages(List<CardPair> cardPairs)
    {
        for (int i = cardPairs.Count - 1; i > 0; i -= 2)
        {
            var pair = cardPairs[i];

            int rand = Random.Range(0, cardPairs.Count - 1);
            var otherPair = cardPairs[rand];

            Sprite sprite = pair.Card1.UI.GetSprite();
            Sprite otherSprite = otherPair.Card1.UI.GetSprite();

            pair.Card1.UI.SetSprite(otherSprite);
            pair.Card2.UI.SetSprite(otherSprite);

            otherPair.Card1.UI.SetSprite(sprite);
            otherPair.Card2.UI.SetSprite(sprite);

            cardPairs.Remove(pair);
            cardPairs.Remove(otherPair);
        }
    }

    private List<CardPair> GetPairs(List<CardBehaviourScript> selectedCards)
    {
        var pairs = new List<CardPair>();
        if (selectedCards.Count == 0)
            return pairs;
        
        for (int i = 0; i < selectedCards.Count; i++)
        {
            var card = selectedCards[i];
            bool added = false;
            for (int j = 0; j < pairs.Count; j++)
            {
                if (pairs[j].Card1.PairId == card.PairId)
                {
                    pairs[j].Card2 = card;
                    added = true;
                }
            }
            if (!added && i != selectedCards.Count-1)
            {
                pairs.Add(new CardPair
                {
                    Card1 = card
                });
            }
        }

        for (int i = pairs.Count-1; i > 0; i--)
        {
            var pair = pairs[i];
            if (pair.Card1 == null || pair.Card2 == null)
                pairs.Remove(pair);
        }

        return pairs;
    }
    private List<CardBehaviourScript> GetRandomCards(List<GameObject> availableCards)
    {
        List<CardBehaviourScript> selectedCards = new List<CardBehaviourScript>();

        if (swapAmount > availableCards.Count)
            swapAmount = availableCards.Count;

        if ((swapAmount % 2) == 0)
            swapAmount--;

        for (int i = 0; i < swapAmount; i++)
        {
            if (availableCards.Count <= 0)
                break;

            int randomIndex = Random.Range(0, availableCards.Count);
            var card = availableCards[randomIndex];
            availableCards.RemoveAt(randomIndex);
            selectedCards.Add(card.GetComponent<CardBehaviourScript>());
        }

        return selectedCards;
    }

    private List<GameObject> GetAvailableCards()
    {
        List<GameObject> cards = new List<GameObject>(cardHandler.CardObjects);

        for (int i = cards.Count-1; i > 0 ; i--)
        {
            var card = cardHandler.CardObjects[i];
            CardBehaviourScript behaviour = card.GetComponent<CardBehaviourScript>();
            if (behaviour.Completed || behaviour.Selected)
            {
                cards.Remove(card);
            }
        }

        return cards;
    }
}

public class CardPair
{
    public CardBehaviourScript Card1;
    public CardBehaviourScript Card2;
}

