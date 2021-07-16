using System.IO;
using System.Text;
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

        protected StringBuilder sb = new StringBuilder();
        
        public string Name => name;
        public abstract string ColouredName { get; }
        public Sprite Icon => icon;
        public virtual HotbarItemGameEvent ItemUseEvent { get => itemUseEvent; set => itemUseEvent = value; }
        
        public void Init(string name, Sprite icon)
        {
            this.name = name;
            this.icon = icon;
        }
        
        public abstract string GetInfoDisplayText();

        public HotbarItem() => sb = new StringBuilder();
        
        protected virtual void OnValidate()
        {
            var assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
            
            name = Path.GetFileNameWithoutExtension(assetPath);
        }
        
        public virtual void UseItem()
        {
            itemUseEvent.Raise(this);
        }

        public override string ToString()
        {
            // Can't figure out why StringBuilder isn't being initialized in the field, I assume it's due to the 
            // unity's serialization, but if you don't initialize it here in one way or another it'll throw
            // a null ref exception
            //  (sb ??= new StringBuilder()).Clear();

            sb.Append("Hotbar Item | Name: ").Append(Name).Append(", Icon: ").Append(Icon.ToString())
                .Append(", Use Event: ").Append(itemUseEvent.ToString()).AppendLine();
            
            return sb.ToString();
        }
    }
}