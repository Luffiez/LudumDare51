using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{

    [SerializeField]
    int numberOfPairs;
    [SerializeField]
    CardSpawner cardSpawner;
    List<GameObject> cards = new List<GameObject>();

    List<GameObject> cardObjects = new List<GameObject>();
    public List<GameObject> CardObjects { get { return cardObjects; } } 
         
    // Start is called before the first frame update
    void Start()
    {
        cards = cardSpawner.SpawnCards(numberOfPairs);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
