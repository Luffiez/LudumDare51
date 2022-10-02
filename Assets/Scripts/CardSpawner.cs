using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LDJAM51.UI;
using UnityEngine.UI;

public class CardSpawner : MonoBehaviour
{
  
    [SerializeField]
    GameObject CardObjectPrefab;
    [SerializeField]
    GameObject GridLayoutObject;
    [SerializeField]
    GameObject UiCardPositionPrefab;
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    Transform SingleCardSpawnPosition;
    int pairId = 0;

    public GameObject SpawnSingleCard(List<Sprite> cardSprites, CardHandler cardHandler)
    {
            int randomListIndex = Random.Range(0, cardSprites.Count);
            GameObject card1 = Instantiate(CardObjectPrefab, null);
            card1.GetComponent<CardBehaviourScript>().PairId = pairId;
           
            card1.GetComponent<CardBehaviourScript>().SetCardHandler(cardHandler);
            card1.GetComponent<InteractableCardUI>().SetSprite(cardSprites[randomListIndex]);
            cardSprites.RemoveAt(randomListIndex);
        
        
            GameObject cardPositionObject = GameObject.Instantiate(UiCardPositionPrefab, GridLayoutObject.transform);

            card1.transform.parent = cardPositionObject.transform;
            card1.GetComponent<RectTransform>().position=SingleCardSpawnPosition.GetComponent<RectTransform>().position;
            card1.transform.localScale = new Vector3(1, 1, 1);
        pairId++;
        LayoutRebuilder.ForceRebuildLayoutImmediate(canvas.GetComponent<RectTransform>());
        return card1;
    }

    public List<GameObject> SpawnCards(int numberOfPairs, List<Sprite> cardSprites, CardHandler cardHandler)
    {
        
        List<GameObject> cardObjects = new List<GameObject>();
        for (int i = 0; i < numberOfPairs; i++)
        {
            
            int randomListIndex = Random.Range(0, cardSprites.Count);
            GameObject card1 =   Instantiate(CardObjectPrefab, null);
            GameObject card2 = Instantiate(CardObjectPrefab, null);
            cardObjects.Add(card2);
            cardObjects.Add(card1);
            card1.GetComponent<CardBehaviourScript>().PairId = pairId;
            card2.GetComponent<CardBehaviourScript>().PairId = pairId;
            card2.GetComponent<CardBehaviourScript>().SetCardHandler(cardHandler);
            card1.GetComponent<CardBehaviourScript>().SetCardHandler(cardHandler);
            card1.GetComponent<InteractableCardUI>().SetSprite(cardSprites[randomListIndex]);
            card2.GetComponent<InteractableCardUI>().SetSprite(cardSprites[randomListIndex]);
            cardSprites.RemoveAt(randomListIndex);
            pairId++;
        }
        List<GameObject>tempCardList = new List<GameObject>(cardObjects);
        for (int i = 0; i < numberOfPairs * 2; i++)
        {
          GameObject cardPositionObject = GameObject.Instantiate(UiCardPositionPrefab, GridLayoutObject.transform);
          int randomListIndex = Random.Range(0, tempCardList.Count);
          tempCardList[randomListIndex].transform.parent = cardPositionObject.transform;
          tempCardList[randomListIndex].GetComponent<RectTransform>().localPosition = Vector3.zero;
          tempCardList[randomListIndex].GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
          tempCardList.RemoveAt(randomListIndex);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(canvas.GetComponent<RectTransform>()); 
        return cardObjects;
    }

}
