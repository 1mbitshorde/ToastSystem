using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace OneM.ToastSystem
{
    /// <summary>
    /// Structure representing a localized name/value variable for Toast Messages.
    /// </summary>
    public readonly struct ToastVariable
    {
        private readonly string name;
        private readonly string value;

        public string Name => name;

        public ToastVariable(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public readonly StringVariable GetStringVariable() => new() { Value = value };
    }
}