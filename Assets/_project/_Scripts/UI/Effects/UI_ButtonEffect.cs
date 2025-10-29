using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TestGame.UI
{
    public class UI_ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float scaleFactor = 1.1f;
        [SerializeField] private float animationDuration = 0.2f;

        private Vector3 originalScale;
        private Coroutine currentAnimation;

        void Start()
        {
            originalScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (currentAnimation != null)
                StopCoroutine(currentAnimation);

            currentAnimation = StartCoroutine(AnimateButton(originalScale * scaleFactor));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (currentAnimation != null)
                StopCoroutine(currentAnimation);

            currentAnimation = StartCoroutine(AnimateButton(originalScale));
        }

        private IEnumerator AnimateButton(Vector3 targetScale)
        {
            Vector3 startScale = transform.localScale;
            float time = 0;

            while (time < animationDuration)
            {
                time += Time.unscaledDeltaTime;
                float t = time / animationDuration;
                transform.localScale = Vector3.Lerp(startScale, targetScale, t);
                yield return null;
            }

            transform.localScale = targetScale;
            currentAnimation = null;
        }
    }
}