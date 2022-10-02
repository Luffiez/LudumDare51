using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    // Start is called before the first frame update

    List<Effect> Effects;
    [SerializeField]
    float effectTime = 10f;
    float effectTimer;
    void Start()
    {
        Effects.AddRange(GetComponents<Effect>());
        effectTimer = Time.time + effectTime;   
    }

    // Update is called once per frame
    void Update()
    {
        if (effectTimer < Time.time)
        {
            if (Effects.Count <= 0)
                return;
            effectTimer = Time.time + effectTime;
            int randomIndex = Random.Range(0, Effects.Count);
            Effects[randomIndex].StartEffect();
        }
    }
}
