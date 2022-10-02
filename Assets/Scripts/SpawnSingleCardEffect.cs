using System.Collections.Generic;
using UnityEngine;

public class SpawnSingleCardEffect : Effect
{
    CardHandler cardHandler;
    bool effectActive = false;
    GameObject newCard;
    [SerializeField]
    float lerpTime = 1f;
    float lerpTimer =0;
    Vector3 startPosition;
    [SerializeField] Sprite sprite;

    private void Start()
    {
        cardHandler = GetComponent<CardHandler>();
    }

    public override void StartEffect()
    {
        if (cardHandler.AddSingleCard(out GameObject card, new List<Sprite>() { sprite }))
        {
            newCard = card;
            effectActive = true;
            startPosition = newCard.GetComponent<RectTransform>().position;
            card.GetComponent<CardBehaviourScript>().Selected = true;
            lerpTimer = 0;
        }
    }

    private void Update()
    {
        if (effectActive)
        {
            lerpTimer += Time.deltaTime;
            if (lerpTimer < lerpTime)
            {
                Vector3 newPosition = Vector3.Lerp(startPosition, newCard.transform.parent.GetComponent<RectTransform>().position , lerpTimer / lerpTime);
                newCard.GetComponent<RectTransform>().position = newPosition;
            }
            else
            {
                newCard.GetComponent<RectTransform>().localPosition = Vector3.zero;
                newCard.GetComponent<CardBehaviourScript>().Selected = false;
                effectActive = false;
            }
        }
    }
}
