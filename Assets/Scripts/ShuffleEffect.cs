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
    public override void StartEffect()
    {
        for (int i = 0; i < NumberOfCards; i++)
        {
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cardObjects = cardHandler.CardObjects;
    }

    // Update is called once per frame
}
