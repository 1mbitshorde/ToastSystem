using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace OneM.ToastSystem
{
    /// <summary>
    /// Toast component representing a single toast message in the Toast System.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class Toast : MonoBehaviour
    {
        [SerializeField] private TMP_Text message;
        [SerializeField] private LocalizeStringEvent localization;

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
        public void SetMessage(string key, string table) =>
            localization.StringReference.SetReference(table, key);

        /// <summary>
        /// Clears the Toast Message.
        /// </summary>
        public void Clear()
        {
            SetMessage(string.Empty);
            localization.StringReference.Clear();
            localization.StringReference.SetReference(string.Empty, string.Empty);
        }
    }
}