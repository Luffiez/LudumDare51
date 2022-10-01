using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleEffect : Effect
{
    [SerializeField]
    CardHandler cardHandler;
    List<GameObject>cardObjects = new List<GameObject> ();
    [SerializeField]
    int NumberOfCards;
    [SerializeField]
    Transform OffScreenTransform;
    [SerializeField]
    float LerpTime;
    float LerpTimer;
    List<GameObject> activeCards = new List<GameObject>();
    List<Vector3> activeCardsStartPositions = new List<Vector3>();
    List<Transform> parentList = new List<Transform>();
    bool lerpBack;
    bool doingEffect =false;
    public override void StartEffect()
    {
        if (doingEffect)
            return;
        activeCards = new List<GameObject>();
        activeCardsStartPositions = new List<Vector3>();
        parentList = new List<Transform>();

        for (int i = 0; i < cardObjects.Count; i++)
        {
            CardBehaviourScript cardBehaviour =  cardObjects[i].GetComponent<CardBehaviourScript>();
            if (!cardBehaviour.Completed && !cardBehaviour.Selected)
            {
                cardBehaviour.Selected = true;
                activeCards.Add(cardObjects[i]);
                activeCardsStartPositions.Add(cardObjects[i].GetComponent<RectTransform>().position);
                parentList.Add(cardObjects[i].transform.parent);
                LerpTimer =0;
                if (activeCards.Count >= NumberOfCards)
                    break;
            }
        }
        doingEffect = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        cardObjects = new List<GameObject>(cardHandler.CardObjects);
    }

    // Update is called once per frame
    private void Update()
    {
        if (doingEffect)
        {
            LerpTimer += Time.deltaTime;
            if (LerpTimer < LerpTime)
            {
                if (lerpBack == false)
                {
                    for (int i = 0; i < activeCards.Count; i++)
                    {
                        Vector3 NewCardPosition = Vector3.Lerp(activeCardsStartPositions[i], OffScreenTransform.position, LerpTimer / LerpTime);
                        activeCards[i].GetComponent<RectTransform>().position = NewCardPosition;

                    }
                }
                else
                {
                    for (int i = 0; i < activeCards.Count; i++)
                    {
                        Vector3 NewCardPosition = Vector3.Lerp(OffScreenTransform.position, activeCards[i].transform.parent.GetComponent<RectTransform>().position, LerpTimer / LerpTime);
                        activeCards[i].GetComponent<RectTransform>().position = NewCardPosition;

                    }
                }
                
            }
            else
            {
                if (lerpBack == false)
                {
                    int listLength = activeCards.Count;
                    List<GameObject> cardClones = new List<GameObject>(activeCards);
                    List<Transform> parentClones = new List<Transform>(parentList);
                    for (int i = 0; i < listLength; i++)
                    {
                        int randomParent = Random.Range(0, parentClones.Count);
                        int randomCardIndex = Random.Range(0, cardClones.Count);
                        cardClones[randomCardIndex].transform.parent = parentClones[randomParent];
                        parentClones.RemoveAt(randomParent);
                        cardClones.RemoveAt(randomCardIndex);
                    }
                    LerpTimer = 0;
                    lerpBack = true;
                }
                else
                {
                    for (int i = 0; i < activeCards.Count; i++)
                    {
                        activeCards[i].GetComponent<RectTransform>().localPosition = Vector3.zero;
                        activeCards[i].GetComponent<CardBehaviourScript>().Selected = false;
                    }
                    doingEffect = false;
                    lerpBack = false;
                }


            }
        }
    }
}
