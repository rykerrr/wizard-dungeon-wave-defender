using System.IO;
using UnityEditor;
using UnityEngine;
using WizardGame.CustomEventSystem;

namespace WizardGame.Item_System.Items
{
    public abstract class HotbarItem : ScriptableObject
    {
        [SerializeField] private new string name = "New Item";
        [SerializeField] private Sprite icon = default;

        [SerializeField] protected HotbarItemGameEvent itemUseEvent;
        
        public string Name { get; private set; }
        public abstract string ColouredName { get; }
        
        public Sprite Icon => icon;

        public abstract string GetInfoDisplayText();

        protected virtual void OnValidate()
        {
            var assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
            Name = Path.GetFileNameWithoutExtension(assetPath);
        }
        
        public virtual void UseItem()
        {
            itemUseEvent.Raise(this);
        }
    }
}