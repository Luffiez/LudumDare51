using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LDJAM51.UI
{
    public class InteractableCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Hover Settings")]
        [SerializeField] float hoverScale = 1.1f;
        [SerializeField] float hoverSpeed = 5f;

        [Header("Bounce Settings")]
        [SerializeField] float bounceHeight = 5f;
        [SerializeField] float bounceSpeed = 1f;

        [Header("Flip Settings")]
        [SerializeField] float flipSpeed;
        [SerializeField] float hoverMultiplier = 2f;
        [SerializeField] Image cardIcon;
        [SerializeField] Image cardFront;
        [SerializeField] Image cardBack;

        ElementScaler scaler;
        ElementBouncer bouncer;
        Transform element;
        bool isFlipping = false;
        bool isFacingUp = false;

        public bool IsFacingUp { get { return isFacingUp; } }
        public bool IsFlipping { get { return isFlipping; } }

        private void Start()
        {
            element = transform.GetChild(0);
            scaler = element.GetComponentInChildren<ElementScaler>();
            bouncer = element.GetComponentInChildren<ElementBouncer>();
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
            cardIcon.sprite = sprite;
        }

        public Sprite GetSprite()
        {
            return cardIcon.sprite;
        }

        public void FlipCard()
        {
            if (isFlipping)
                return;

            scaler.StopAllCoroutines();
            bouncer.StopBounce();
            bouncer.StartBounce(bounceHeight, bounceSpeed);
            if (!isFacingUp)
                StartCoroutine(FlipUp());
            else
                StartCoroutine(FlipDown());
        }

        IEnumerator FlipUp()
        {
            StopCoroutine(FlipDown());
            isFlipping = true;
            float currentX = element.localRotation.x;
            float targetX = 180;

            while (currentX + 1 <= targetX)
            {
                currentX = Mathf.Lerp(currentX, targetX, Time.deltaTime * flipSpeed);
                element.localRotation = Quaternion.Euler(currentX, 0, 0);

                if (!isFacingUp && currentX > 90)
                {
                    element.localScale = new Vector3(element.localScale.x, -element.localScale.y, element.localScale.z);
                    cardFront.enabled = true;
                    cardIcon.enabled = true;

                    cardBack.enabled = false;
                    
                    cardIcon.transform.localScale = new Vector3(1, 1, 1);
                    isFacingUp = true;

                    yield return new WaitForSeconds(0.05f);
                }

                yield return new WaitForEndOfFrame();
            }

            element.localRotation = Quaternion.Euler(0, 0, 0);
            cardIcon.transform.localScale = new Vector3(1, 1, 1);
            cardFront.transform.localScale = new Vector3(1, 1, 1);
            element.localScale = new Vector3(element.localScale.x, -element.localScale.y, element.localScale.z);

            isFlipping = false;
        }

        IEnumerator FlipDown()
        {
            isFlipping = true;
            float targetX = 0;
            float currentX = 180;
            cardIcon.transform.localScale = new Vector3(1, -1, 1);

            element.localRotation = Quaternion.Euler(currentX, 0, 0);

            while (currentX - 10 >= targetX)
            {
                currentX = Mathf.Lerp(currentX, targetX, Time.deltaTime * flipSpeed * 1.5f);
                element.localRotation = Quaternion.Euler(currentX, 0, 0);

                if (isFacingUp && currentX < 90)
                {
                    yield return new WaitForSeconds(0.05f);
                    
                    isFacingUp = false;
                    cardIcon.enabled = false;
                    cardBack.enabled = true;
                    cardFront.enabled = false;
                }

                yield return new WaitForEndOfFrame();
            }

            cardFront.transform.localScale = new Vector3(1, 1, 1);
            isFlipping = false;
            scaler.SetTargetScale(1, hoverSpeed * hoverMultiplier);
            element.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}