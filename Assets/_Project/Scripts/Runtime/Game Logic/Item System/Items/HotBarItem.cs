using System.Text;
using UnityEngine;
using WizardGame.Combat_System.Cooldown_System;
using WizardGame.CustomEventSystem;
using WizardGame.Utility;

namespace WizardGame.Item_System.Items
{
    public abstract class HotBarItem : ScriptableObjectAutoNameSet
    {
        [SerializeField] private Sprite icon = default;

        [SerializeField] private CooldownData cooldownData = default;
        
        [Header("Properties")]
        [SerializeField] protected HotbarItemGameEvent itemUseEvent;

        protected StringBuilder sb;
        
        public abstract string ColouredName { get; }
        public Sprite Icon => icon;
        public HotbarItemGameEvent ItemUseEvent { get => itemUseEvent; set => itemUseEvent = value; }

        public CooldownData CooldownData => cooldownData;

        protected HotBarItem() => sb = new StringBuilder();

        public void Init(string name, Sprite icon)
        {
            cooldownData ??= new CooldownData();
            
            this.name = name;
            this.icon = icon;
        }

        public void InitCooldown(float cooldownDuration, bool initGuid = false)
        {
            cooldownData.Init(cooldownDuration, initGuid);
        }

        protected virtual void Awake()
        {
            Init(name, icon);
        }

        public abstract string GetInfoDisplayText();

        
		public virtual void UseItem()
        {
            itemUseEvent.Raise(this);
        }
		
        public override string ToString()
        {
            sb.Append("Hotbar Item | Name: ").Append(Name).Append(", Icon: ").Append(Icon.ToString())
                .Append(", Use Event: ").Append(itemUseEvent.ToString()).AppendLine();
            
            return sb.ToString();
        }

        [ContextMenu("Log GUID")]
        public void LogId()
        {
            Debug.Log(cooldownData.Id);
        }
    }
}