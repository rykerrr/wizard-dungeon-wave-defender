using UnityEngine;

namespace WizardGame.ItemSystem
{
    public abstract class HotbarItem : ScriptableObject
    {
        [SerializeField] private new string name = "New Item";
        [SerializeField] private Sprite icon = default;

        public string Name => name;
        public abstract string ColouredName { get; }
        
        public Sprite Icon => icon;

        public abstract string GetInfoDisplayText();
    }
}