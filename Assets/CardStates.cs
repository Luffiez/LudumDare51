using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStates : MonoBehaviour
{
    int pairId =-1;
    bool selected = false;
    bool completed = false;
    public int PairId { get { return pairId; } set { pairId = value; } }
    public bool Selected { get { return selected; } set { selected = value; } }
    public bool Completed { get { return selected; } set { selected = value; } }
}
