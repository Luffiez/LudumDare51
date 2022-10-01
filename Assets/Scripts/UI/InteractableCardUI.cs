using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LDJAM51.UI
{
    public class InteractableCardUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Hover Settings")]
        [SerializeField] ElementScaler scaler;
        [SerializeField] float hoverScale = 1.1f;
        [SerializeField] float hoverSpeed = 5f;

        [Header("Flip Settings")]
        [SerializeField] float flipSpeed;
        [SerializeField] float hoverMultiplier = 2f;
        [SerializeField] Image iconImage;
        [SerializeField] Image backgroundImage;

        Transform element;
        bool isFlipping = false;
        bool isFacingUp = false;

        private void Start()
        {
            element = transform.GetChild(0);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            FlipCard();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isFlipping || isFacingUp)
                return;

            scaler.SetTargetScale(hoverScale, hoverSpeed);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isFlipping || isFacingUp)
                return;

            scaler.SetTargetScale(1, hoverSpeed);
        }

        public void SetSprite(Sprite sprite)
        {
            iconImage.sprite = sprite;
        }

        public void FlipCard()
        {
            if (isFlipping)
                return;

            scaler.StopAllCoroutines();

            if (!isFacingUp)
                StartCoroutine(FlipUp());
            else
            {
                // TODO: Remove self from match check
                StartCoroutine(FlipDown());
            }
        }

        IEnumerator FlipUp()
        {
            isFlipping = true;
            float currentX = element.rotation.x;
            float targetX = 180;
            float newScale = hoverScale * 1.1f;

            //scaler.SetTargetScale(newScale, hoverSpeed * hoverMultiplier);

            while (currentX + 1 <= targetX)
            {
                currentX = Mathf.Lerp(currentX, targetX, Time.deltaTime * flipSpeed);
                element.rotation = Quaternion.Euler(currentX, 0, 0);

                if (!isFacingUp && currentX > 90)
                {
                    element.localScale = new Vector3(element.localScale.x, -element.localScale.y, element.localScale.z);
                    backgroundImage.enabled = false;
                    iconImage.enabled = true;
                    isFacingUp = true;

                    yield return new WaitForSeconds(0.05f);

                    //iconImage.transform.localScale = new Vector3(1, -1, 1);
                    //backgroundImage.transform.localScale = new Vector3(1, -1, 1);
                }

                yield return new WaitForEndOfFrame();
            }

            element.rotation = Quaternion.Euler(0, 0, 0);
            iconImage.transform.localScale = new Vector3(1, 1, 1);
            backgroundImage.transform.localScale = new Vector3(1, 1, 1);
            element.localScale = new Vector3(element.localScale.x, -element.localScale.y, element.localScale.z);

            isFlipping = false;
            //scaler.StopAllCoroutines();
            //scaler.SetTargetScale(hoverScale, hoverSpeed * hoverMultiplier);

            // TODO: add self to match check
        }

        IEnumerator FlipDown()
        {
            isFlipping = true;
            float targetX = 0;
            float currentX = 180;
            iconImage.transform.localScale = new Vector3(1, -1, 1);

            element.rotation = Quaternion.Euler(currentX, 0, 0);

            while (currentX - 1 >= targetX)
            {
                currentX = Mathf.Lerp(currentX, targetX, Time.deltaTime * flipSpeed * 1.5f);
                element.rotation = Quaternion.Euler(currentX, 0, 0);

                if (isFacingUp && currentX < 90)
                {
                    yield return new WaitForSeconds(0.05f);

                    iconImage.enabled = false;
                    backgroundImage.enabled = true;
                    isFacingUp = false;
                }

                yield return new WaitForEndOfFrame();
            }

            backgroundImage.transform.localScale = new Vector3(1, 1, 1);

            scaler.SetTargetScale(1, hoverSpeed * hoverMultiplier);
            element.rotation = Quaternion.Euler(0, 0, 0);
            scaler.SetTargetScale(1, 5);
            isFlipping = false;
        }
    }
}