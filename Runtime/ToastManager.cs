using UnityEngine;
using OneM.SerializedDictionaries;
using System.Collections;

namespace OneM.ToastSystem
{
    /// <summary>
    /// Manager for Toast Messages with time and show/hide animations.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class ToastManager : MonoBehaviour
    {
        [Tooltip("The toast message time (not influenced by DeltaTime).")]
        public float time = 5f;
        [SerializeField, Tooltip("The local Toast component.")]
        private Toast toast;
        [SerializeField, Tooltip("Optional audio source to play.")]
        private AudioSource source;

        [Space]
        [SerializeField] private SerializedDictionary<ToastPosition, RectTransform> positions;

        public static Toast Toast => Instance.toast;

        private static ToastManager Instance { get; set; }

        private void Awake() => Instance = this;
        private void Start() => Toast.Clear();
        private void OnDestroy() => Instance = null;

        /// <summary>
        /// <inheritdoc cref="Toast.SetMessage(string)"/>
        /// </summary>
        /// <param name="message">
        /// <inheritdoc cref="Toast.SetMessage(string)" path="/param[@name='message']"/>
        /// </param>
        /// <param name="position">The toast position.</param>
        public static void ShowMessage(string message, ToastPosition position = ToastPosition.Top)
        {
            Toast.SetPosition(Instance.positions[position].position);
            Toast.SetMessage(message);
            Instance.Show();
        }

        /// <summary>
        /// <inheritdoc cref="Toast.SetMessage(string, string)"/>
        /// </summary>
        /// <param name="key">
        /// <inheritdoc cref="Toast.SetMessage(string, string)" path="/param[@name='key']"/>
        /// </param>
        /// <param name="table">
        /// <inheritdoc cref="Toast.SetMessage(string, string)" path="/param[@name='table']"/>
        /// </param>
        /// <param name="position">
        /// <inheritdoc cref="ShowMessage(string, ToastPosition)" path="/param[@name='position']"/>
        /// </param>
        public static void ShowMessage(string key, string table, ToastPosition position = ToastPosition.Top)
        {
            Toast.SetPosition(Instance.positions[position].position);
            Toast.SetMessage(key, table);
            Instance.Show();
        }

        /// <summary>
        /// Set the Toast Message using the given localized key and table, 
        /// adding the given variables to the Toast Message.
        /// </summary>
        /// <param name="key">
        /// <inheritdoc cref="Toast.SetMessage(string, string)" path="/param[@name='key']"/>
        /// </param>
        /// <param name="table">
        /// <inheritdoc cref="Toast.SetMessage(string, string)" path="/param[@name='table']"/>
        /// </param>
        /// <param name="position">
        /// <inheritdoc cref="ShowMessage(string, ToastPosition)" path="/param[@name='position']"/>
        /// </param>
        /// <param name="variables">
        /// <inheritdoc cref="Toast.AddVariables(ToastVariable[])" path="/param[@name='variables']"/>
        /// </param>
        public static void ShowMessage(string key, string table, ToastPosition position = ToastPosition.Top, params ToastVariable[] variables)
        {
            Toast.AddVariables(variables);
            ShowMessage(key, table, position);
        }

        /// <summary>
        /// Hides the current Toast Message.
        /// </summary>
        public static void HideMessage() => Instance.Hide();
        public static void ClearMessage() => Instance.Clear();

        private void Show()
        {
            StopAllCoroutines();
            StartCoroutine(ShowRoutine());
        }

        private void Hide()
        {
            StopAllCoroutines();
            if (Toast.HasMessage()) StartCoroutine(HideRoutine());
            else Toast.Hide();
        }

        private void Clear()
        {
            StopAllCoroutines();
            Toast.Hide();
        }

        private IEnumerator ShowRoutine()
        {
            if (source) source.Play();
            yield return Toast.ShowRoutine();
            yield return new WaitForSecondsRealtime(time);
            yield return HideRoutine();
        }

        private IEnumerator HideRoutine()
        {
            yield return Toast.HideRoutine();
            Toast.Clear();
        }
    }
}