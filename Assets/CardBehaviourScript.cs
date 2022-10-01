using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CardBehaviourScript : MonoBehaviour, IPointerClickHandler
{
    int pairId = -1;
    bool selected = false;
    bool completed = false;
    public bool Selected { get { return selected; } set { selected = value; } }
    public int PairId { get { return pairId; } set { pairId = value; } }
    public bool Completed { get { return selected; } set { selected = value; } }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("test");
    }
}
