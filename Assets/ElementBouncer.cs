using System.Collections;
using UnityEngine;

public class ElementBouncer : MonoBehaviour
{
    float startHeight;
    private void Start()
    {
        float startHeight = transform.localPosition.y;

    }
    public void StartBounce(float height, float speed = 1)
    {
        StartCoroutine(Bounce(height, speed));
    }
    public void StopBounce()
    {
        transform.localPosition = new Vector3(startHeight, startHeight, startHeight);
        StopAllCoroutines();
    }

    IEnumerator Bounce(float height, float speed = 1f)
    {   
        float current = startHeight;
        float targetHeight = startHeight + height;

        while (current + 0.1f < targetHeight)
        {
            current = Mathf.Lerp(current, targetHeight, Time.deltaTime * speed);
            transform.localPosition = new Vector3(0, current, 0);
            yield return new WaitForEndOfFrame();
        }

        while (current - 0.1f > startHeight)
        {
            current = Mathf.Lerp(current, startHeight, Time.deltaTime * speed);
            transform.localPosition = new Vector3(0, current, 0);
            yield return new WaitForEndOfFrame();
        }

        transform.localPosition = new Vector3(0, startHeight, 0);
    }
}
