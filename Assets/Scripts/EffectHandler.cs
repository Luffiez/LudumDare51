using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Effect> Effects;
    float effectTime = 10f;
    float effectTimer;
    void Start()
    {
        effectTimer = Time.time + effectTime;   
    }

    // Update is called once per frame
    void Update()
    {
        if (effectTimer < Time.time)
        {
            int randomIndex = Random.Range(0, Effects.Count);
            Effects[randomIndex].StartEffect();
        }
    }
}
