using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardInitializer : MonoBehaviour
{
    [SerializeField]
    int numberOfPairs;
    [SerializeField]
    List<GameObject> CardObjectPrefabs;
    public List<GameObject> CardObjects = new List<GameObject>();
    [SerializeField]
    GameObject GridLayoutObject;
    [SerializeField]
    GameObject UiCardPositionPrefab;

    void Start()
    {
        for (int i = 0; i < numberOfPairs; i++)
        {
            int randomListIndex = Random.Range(0, CardObjectPrefabs.Count);
            GameObject card1 =   Instantiate( CardObjectPrefabs[randomListIndex],null);
            GameObject card2 = Instantiate(CardObjectPrefabs[randomListIndex], null);
            CardObjects.Add(card2);
            CardObjects.Add(card1);
            CardObjectPrefabs.RemoveAt(randomListIndex);
        }
        List<GameObject>tempCardList = new List<GameObject>(CardObjects);
        for (int i = 0; i < numberOfPairs * 2; i++)
        {
          GameObject cardPositionObject = GameObject.Instantiate(UiCardPositionPrefab, GridLayoutObject.transform);
          int randomListIndex = Random.Range(0, tempCardList.Count);
          tempCardList[randomListIndex].transform.parent = cardPositionObject.transform;
          tempCardList[randomListIndex].GetComponent<RectTransform>().localPosition = Vector3.zero;
          tempCardList.RemoveAt(randomListIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
