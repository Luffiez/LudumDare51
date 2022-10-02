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
    float endScreenTime = 2f;
    float endScreenTimer;

    [SerializeField]
    CardSpawner cardSpawner;
    List<GameObject> cards = new List<GameObject>();
    [SerializeField]
    List<Sprite> CardSprites;
    [SerializeField]
    MatchHandler matchHandler;
    bool gameCompleted = false;
    [SerializeField] LevelCompleteScreen winScreen;
   

    int flips = 0;

    public List<GameObject> CardObjects { get { return cards; } }

    // Start is called before the first frame update
    private void Start()
    {
        cards = cardSpawner.SpawnCards(numberOfPairs, CardSprites, this);
    }

    public bool AddSingleCard(out GameObject card)
    {
        card = null;
        if (CardSprites.Count <= 0)
            return false;
        card = cardSpawner.SpawnSingleCard(CardSprites, this);
        CardObjects.Add(card);
        return true;
    }

    public bool AddSingleCard(out GameObject card, List<Sprite> sprite)
    {
        card = null;
        if (sprite.Count <= 0)
            return false;
        card = cardSpawner.SpawnSingleCard(sprite, this);
        CardObjects.Add(card);
        return true;
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
