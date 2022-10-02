using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlipDirection
{
    X,Y
}

public class FlipEffect : Effect
{

    [SerializeField]
    GameObject BoardObject;
    bool effectInAction;
    [SerializeField]
    float lerptime;
    [SerializeField]
    FlipDirection FlipDir;
    float lerpTimer = 0;
    Vector3 targetScale;
    Vector3 startScale;
    public override void StartEffect()
    {
        if (effectInAction)
            return;
        startScale = BoardObject.transform.localScale;
        if (FlipDir==FlipDirection.X)
        {
            targetScale = new Vector3(-BoardObject.transform.localScale.x, BoardObject.transform.localScale.y, BoardObject.transform.localScale.z);
        }
        else
        {
            targetScale = new Vector3(BoardObject.transform.localScale.x, -BoardObject.transform.localScale.y, BoardObject.transform.localScale.z);
        }
        effectInAction = true;
        lerpTimer = 0;
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (effectInAction)
        {
            lerpTimer += Time.deltaTime;
            if (lerpTimer < lerptime)
            {
                Vector3 newScale = Vector3.Lerp(startScale, targetScale, lerpTimer / lerptime);
                BoardObject.transform.localScale = newScale;
            }
            else
            {
                BoardObject.transform.localScale = targetScale;
                effectInAction=false;
            }
            
        }
    }
}
