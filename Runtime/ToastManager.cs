using UnityEngine;
using OneM.AwaitableSystem;

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

        [Header("ANIMATIONS")]
        [SerializeField] private Animation _animation;
        [SerializeField] private string showAnimationName = "Toast@Show";
        [SerializeField] private string hideAnimationName = "Toast@Hide";

        public static Toast Toast => Instance.toast;

        private static ToastManager Instance { get; set; }

        private bool isShowing;

        private void Awake() => Instance = this;
        private void OnDestroy() => Instance = null;

        /// <summary>
        /// <inheritdoc cref="Toast.SetMessage(string)"/>
        /// </summary>
        /// <param name="message">
        /// <inheritdoc cref="Toast.SetMessage(string)" path="/param[@name='message']"/>
        /// </param>
        public static void ShowMessage(string message)
        {
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
        public static void ShowMessage(string key, string table)
        {
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
        /// <param name="variables">
        /// <inheritdoc cref="Toast.AddVariables(ToastVariable[])" path="/param[@name='variables']"/>
        /// </param>
        public static void ShowMessage(string key, string table, params ToastVariable[] variables)
        {
            Toast.AddVariables(variables);
            ShowMessage(key, table);
        }

        /// <summary>
        /// Hides the current Toast Message.
        /// </summary>
        public static void HideMessage() => _ = Instance.HideAsync();

        private async void Show()
        {
            if (isShowing) return;

            isShowing = true;

            if (source) source.Play();
            _animation.Play(showAnimationName);

            await AwaitableUtility.WaitWhilePlayingAsync(_animation);
            await AwaitableUtility.WaitForSecondsRealtimeAsync(time);
            await HideAsync();

            isShowing = false;
        }

        private async Awaitable HideAsync()
        {
            _animation.Play(hideAnimationName);
            await AwaitableUtility.WaitWhilePlayingAsync(_animation);
            Toast.Clear();
        }
    }
}