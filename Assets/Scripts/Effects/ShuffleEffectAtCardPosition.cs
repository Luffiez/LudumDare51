using System.Collections.Generic;
using UnityEngine;

public class ShuffleEffectAtCardPosition : Effect
{
    CardHandler cardHandler;
    List<GameObject> cardObjects = new List<GameObject>();
    [SerializeField]
    int NumberOfCards;
    Transform OffScreenTransform;
    [SerializeField]
    float LerpTime;
    float LerpTimer;
    List<GameObject> activeCards = new List<GameObject>();
    List<Vector3> activeCardsStartPositions = new List<Vector3>();
    List<Transform> parentList = new List<Transform>();
    List<Vector3> StartPositions = new List<Vector3>();
    bool doingEffect = false;
    public override bool StartEffect()
    {
        if (doingEffect)
            return true;
        List<GameObject> activeCardsTemps = new List<GameObject>();
        activeCards = new List<GameObject>();
        activeCardsStartPositions = new List<Vector3>();
        parentList = new List<Transform>();
        StartPositions = new List<Vector3>();
        for (int i = 0; i < cardObjects.Count; i++)
        {
            CardBehaviourScript cardBehaviour =  cardObjects[i].GetComponent<CardBehaviourScript>();
            if (!cardBehaviour.Completed && !cardBehaviour.Selected)
            {
                activeCardsTemps.Add(cardObjects[i]);
            }
        }

        for (int i = 0; i < NumberOfCards; i++)
        {
            if (activeCardsTemps.Count <= 0)
                break;
            int randomIndex = Random.Range(0, activeCardsTemps.Count);
            activeCardsStartPositions.Add(activeCardsTemps[randomIndex].GetComponent<RectTransform>().position);
            CardBehaviourScript cardBehaviour = activeCardsTemps[randomIndex].GetComponent<CardBehaviourScript>();
            cardBehaviour.Selected = true;
            activeCards.Add(activeCardsTemps[randomIndex]);
            StartPositions.Add(activeCardsTemps[randomIndex].GetComponent<RectTransform>().position);
            parentList.Add(activeCardsTemps[randomIndex].transform.parent);
            activeCardsTemps.RemoveAt(randomIndex);
        }
        SoundManager.instance.PlaySfx("Shuffle");
        LerpTimer = 0;
        doingEffect = true;

        int listLength = activeCards.Count;
        List<GameObject> cardClones = new List<GameObject>(activeCards);
        List<Transform> parentClones = new List<Transform>(parentList);
        for (int i = 0; i < listLength; i++)
        {
            int randomParent = Random.Range(0, parentClones.Count);
            int randomCardIndex = Random.Range(0, cardClones.Count);
            if (cardClones[randomCardIndex].transform.parent == parentClones[randomParent])
            {
                randomCardIndex++;
                randomCardIndex = randomCardIndex % cardClones.Count;
            }
            cardClones[randomCardIndex].transform.parent = parentClones[randomParent];
            parentClones.RemoveAt(randomParent);
            cardClones.RemoveAt(randomCardIndex);
        }
        SoundManager.instance.PlaySfx("Shuffle");
        return true;
    }

    void Start()
    {
        cardHandler = GetComponent<CardHandler>();
        OffScreenTransform = GameObject.Find("OffScreenTransform").transform;
        cardObjects = cardHandler.CardObjects;

        if (OffScreenTransform == null)
        {
            throw new System.Exception($"{typeof(ShuffleEffect)} requires a Scene Object with the name 'OffScreenTransform'"!);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (doingEffect)
        {
            LerpTimer += Time.deltaTime;
            if (LerpTimer < LerpTime)
            {
                for (int i = 0; i < activeCards.Count; i++)
                {
                    Vector3 NewCardPosition = Vector3.Lerp(StartPositions[i], activeCards[i].transform.parent.GetComponent<RectTransform>().position, LerpTimer / LerpTime);
                    activeCards[i].GetComponent<RectTransform>().position = NewCardPosition;

                }
            }
            else
            {
                for (int i = 0; i < activeCards.Count; i++)
                {
                    activeCards[i].GetComponent<RectTransform>().localPosition = Vector3.zero;
                    activeCards[i].GetComponent<CardBehaviourScript>().Selected = false;
                }
                doingEffect = false;
            }
        }
    }
}
