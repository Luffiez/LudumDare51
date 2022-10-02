using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementExpandFade : MonoBehaviour
{
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void Play(float scale, float speed = 1)
    {
        StartCoroutine(ExpandAndFade(scale, speed));
    }

    IEnumerator ExpandAndFade(float scale, float speed = 1f)
    {
        float current = transform.localScale.x;
        float targetScale = current * scale;

        float startAlpha = image.color.a;

        while (current + 0.1f < targetScale)
        {
            current = Mathf.Lerp(current, targetScale, Time.deltaTime * speed);
            float alpha = startAlpha * (1 - (current / targetScale));
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            transform.localScale = new Vector3(current, current, current);
            yield return new WaitForEndOfFrame();
        }

        image.enabled = false;
    }
}
