using System.Collections;
using UnityEngine;

namespace LDJAM51.UI
{
    public class ElementScaler : MonoBehaviour
    {
        public void SetTargetScale(float scale, float speed = 1)
        {
            StartCoroutine(SetScale(scale, speed));
        }

        IEnumerator SetScale(float scale, float speed = 1f)
        {
            float current = transform.localScale.x;

            while (transform.localScale.x != scale)
            {
                current = Mathf.Lerp(current, scale, Time.deltaTime * speed);
                transform.localScale = new Vector3(current, current, current);
                yield return new WaitForEndOfFrame();
            }

            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}

