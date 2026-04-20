using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using System.Collections;

namespace OneM.ToastSystem
{
    /// <summary>
    /// Toast component representing a single toast message in the Toast System.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(RectTransform))]
    public sealed class Toast : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvas;
        [SerializeField] private TMP_Text message;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private LocalizeStringEvent localization;
        [SerializeField, Min(0f)] private float fadeDuration = 0.2f;

        /// <summary>
        /// The localization string reference. Use to set Local Variables.
        /// </summary>
        public LocalizedString StringReference => localization.StringReference;

        private void Reset()
        {
            canvas = GetComponent<CanvasGroup>();
            message = GetComponentInChildren<TMP_Text>();
            rectTransform = GetComponent<RectTransform>();
            localization = GetComponentInChildren<LocalizeStringEvent>();
        }

        public bool HasMessage() => !string.IsNullOrEmpty(message.text);

        /// <summary>
        /// Set the Toast Message directly, without using localization.
        /// </summary>
        /// <param name="message">The toast message.</param>
        public void SetMessage(string message) => this.message.text = message;

        /// <summary>
        /// Set the Toast Message using the given localized key and table.
        /// </summary>
        /// <param name="key">The localized toast message key.</param>
        /// <param name="table">The localized toast message table where the key is.</param>
        public void SetMessage(string key, string table) => StringReference.SetReference(table, key);

        /// <summary>
        /// Clears the Toast Message.
        /// </summary>
        public void Clear()
        {
            SetMessage(string.Empty);
            StringReference.Clear();
            StringReference.SetReference(string.Empty, string.Empty);
        }

        /// <summary>
        /// Add the given variables to the Toast Message localization.
        /// </summary>
        /// <param name="variables">
        /// The name/value variable. 
        /// You cannot change its value after set.
        /// </param>
        public void AddVariables(params ToastVariable[] variables)
        {
            foreach (var variable in variables)
            {
                StringReference.Add(
                    variable.Name,
                    variable.GetStringVariable()
                );
            }
        }

        internal void Hide()
        {
            Clear();
            canvas.alpha = 0f;
        }

        internal void SetPosition(Vector2 position) => rectTransform.position = position;

        internal IEnumerator ShowRoutine() => CrossFadeRoutine(0f, 1f);
        internal IEnumerator HideRoutine() => CrossFadeRoutine(1f, 0f);

        private IEnumerator CrossFadeRoutine(float initial, float final)
        {
            var currentTime = 0F;
            while (currentTime < fadeDuration)
            {
                var step = currentTime / fadeDuration;

                canvas.alpha = Mathf.Lerp(initial, final, step);
                currentTime += Time.unscaledDeltaTime;

                yield return null;
            }

            canvas.alpha = final;
        }
    }
}