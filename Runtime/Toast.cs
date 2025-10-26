using TMPro;
using UnityEngine;
using UnityEngine.Localization;
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
        /// The localization string reference. Use to set Local Variables.
        /// </summary>
        public LocalizedString StringReference => localization.StringReference;

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
    }
}