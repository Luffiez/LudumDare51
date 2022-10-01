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
        [SerializeField] Image iconImage;
        [SerializeField] Image backgroundImage;

        ElementScaler scaler;
        ElementBouncer bouncer;
        Transform element;
        bool isFlipping = false;
        bool isFacingUp = false;

        public bool IsFacingUp { get { return isFacingUp; } }
        
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
            iconImage.sprite = sprite;
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
            {
                // TODO: Remove self from match check
                StartCoroutine(FlipDown());
            }
        }

        IEnumerator FlipUp()
        {
            isFlipping = true;
            float currentX = element.localRotation.x;
            float targetX = 180;
            float newScale = hoverScale * 1.1f;

            //scaler.SetTargetScale(newScale, hoverSpeed * hoverMultiplier);

            while (currentX + 1 <= targetX)
            {
                currentX = Mathf.Lerp(currentX, targetX, Time.deltaTime * flipSpeed);
                element.localRotation = Quaternion.Euler(currentX, 0, 0);

                if (!isFacingUp && currentX > 90)
                {
                    element.localScale = new Vector3(element.localScale.x, -element.localScale.y, element.localScale.z);
                    backgroundImage.enabled = false;
                    iconImage.enabled = true;
                    iconImage.transform.localScale = new Vector3(1, 1, 1);
                    isFacingUp = true;

                    yield return new WaitForSeconds(0.05f);

                    //backgroundImage.transform.localScale = new Vector3(1, -1, 1);
                }

                yield return new WaitForEndOfFrame();
            }

            element.localRotation = Quaternion.Euler(0, 0, 0);
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

            element.localRotation = Quaternion.Euler(currentX, 0, 0);

            while (currentX - 1 >= targetX)
            {
                currentX = Mathf.Lerp(currentX, targetX, Time.deltaTime * flipSpeed * 1.5f);
                element.localRotation = Quaternion.Euler(currentX, 0, 0);

                if (isFacingUp && currentX < 90)
                {
                    yield return new WaitForSeconds(0.05f);
                    
                    isFacingUp = false;
                    iconImage.enabled = false;
                    backgroundImage.enabled = true;
                }

                yield return new WaitForEndOfFrame();
            }

            backgroundImage.transform.localScale = new Vector3(1, 1, 1);

            scaler.SetTargetScale(1, hoverSpeed * hoverMultiplier);
            element.localRotation = Quaternion.Euler(0, 0, 0);
            scaler.SetTargetScale(1, 5);
            isFlipping = false;
        }
    }
}