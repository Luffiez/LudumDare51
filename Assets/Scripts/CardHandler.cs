using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardHandler : MonoBehaviour
{

    [SerializeField]
    int numberOfPairs;
    int completedPairs = 0;
    [SerializeField]
    float endScreenTime =2f;
    float endScreenTimer;

    [SerializeField]
    CardSpawner cardSpawner;
    List<GameObject> cards = new List<GameObject>();
    [SerializeField]
    List<Sprite> CardSprites;
    [SerializeField]
    MatchHandler matchHandler;
    List<GameObject> cardObjects = new List<GameObject>();
    bool gameCompleted = false;
    [SerializeField] LevelCompleteScreen winScreen;
   

    int flips = 0;

    public List<GameObject> CardObjects { get { return cardObjects; } } 
         
    // Start is called before the first frame update
    void Start()
    {
        cards = cardSpawner.SpawnCards(numberOfPairs, CardSprites,this);
    }

    public void AddToMatch(CardBehaviourScript cardBehaviourScript)
    {
        bool match = matchHandler.AddCard(cardBehaviourScript);
        flips++;
        if (match)
        {
            completedPairs++;
            if (completedPairs >= numberOfPairs)
            {
                gameCompleted=true;
                endScreenTimer = endScreenTime + Time.time;
            }
        }
    }

    public void Update()
    {
        if (gameCompleted)
        {
            if(endScreenTimer < Time.time)
            {
                winScreen.Show(flips);
                enabled = false;
            }
        }
    }
}
