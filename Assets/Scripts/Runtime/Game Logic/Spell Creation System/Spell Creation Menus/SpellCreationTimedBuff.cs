using TMPro;
using UnityEngine;
using UnityEngine.Events;
using WizardGame.Combat_System;
using WizardGame.Item_System.Items;
using WizardGame.Stats_System;

namespace WizardGame.Spell_Creation
{
    public class SpellCreationTimedBuff : SpellCreationMenuBase
    {
        [Header("Spell creation helper data")]
        [SerializeField] private SpellCreationHandler spellCreationHandler = default;
        [SerializeField] private UnityEvent onSpellCreatedSelectThis = default;
        
        [Header("Spell data")]
        [SerializeField] private TextMeshProUGUI buffStrengthLabel = default;

        [SerializeField] private SpellBookItem baseItem = default;
        [SerializeField] private TimedStatBuffData data = default;
        
        public override SpellBookItem SpellFoundation => baseItem;
        public override BaseSpellCastData Data => data;

        protected override void Awake()
        {
            base.Awake();
            
            // default value until i figure a way to implement this in a better way
            data.StatType = StatTypeFactory.GetType("Intelligence");
            
            spellCreationHandler.onSpellCreated += (spellDataType) =>
            {
                if (spellDataType != data.GetType()) return;
                
                data = new TimedStatBuffData(data);
                
                onSpellCreatedSelectThis?.Invoke();
                
                UpdateLabels();
            };
        }

        public void InputField_SetDuration(string dur)
        {
            if(int.TryParse(dur, out int res))
            {
                data.Duration = res;
                UpdateLabels();
            }
        }
        
        public override void UpdateLabels()
        {
            manaCostLabel.text = $"Mana Cost: {data.ManaCost}";
            buffStrengthLabel.text = $"Buff's strength: {data.BuffStrength}";
        }
    }
}
