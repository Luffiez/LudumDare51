using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleEffect : Effect
{
    public override void StartEffect()
    {
        Debug.Log("test");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("test2");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("test3");
    }
}
