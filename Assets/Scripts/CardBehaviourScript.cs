using LDJAM51.UI;
using UnityEngine;
using UnityEngine.EventSystems;
public class CardBehaviourScript : MonoBehaviour, IPointerDownHandler
{
    int pairId = -1;
    bool selected = false;
    bool completed = false;
    CardHandler CardHandler;
    public bool Selected { get { return selected; } set { selected = value; } }
    public int PairId { get { return pairId; } set { pairId = value; } }
    public bool Completed { get { return completed; } private set { completed = value; } }

    public InteractableCardUI UI { get; private set; }

    void Start()
    {
        UI = GetComponent<InteractableCardUI>();
    }

    public void Complete()
    {
        completed = true;
        selected = false;
        GetComponentInChildren<ElementExpandFade>().Play(1.6f, 2.5f);
    }

    public void SetCardHandler(CardHandler cardHandler)
    {
        this.CardHandler = cardHandler;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (selected == false && completed == false)
        {
            CardHandler.AddToMatch(this);
        }
    }
}
