using TMPro;
using UnityEngine;
using UnityEngine.Events;
using WizardGame.Combat_System;
using WizardGame.Item_System.Items;

namespace WizardGame.Spell_Creation
{
    public class SpellCreationEnergyHealingField : SpellCreationMenuBase
    {
        [Header("Spell creation helper data")]
        [SerializeField] private SpellCreationHandler spellCreationHandler = default;
        [SerializeField] private UnityEvent onSpellCreatedSelectThis = default;
        
        [Header("Spell data")]
        [SerializeField] private TextMeshProUGUI tickHealLabel = default;
        
        [SerializeField] private SpellBookItem baseItem = default;
        [SerializeField] private EnergyHealingFieldData data = default;

        public override SpellBookItem SpellFoundation => baseItem;
        public override BaseSpellCastData Data => data;

        protected override void Awake()
        {
            base.Awake();
            
            spellCreationHandler.onSpellCreated += (spellDataType) =>
            {
                if (spellDataType != data.GetType()) return;
                
                data = new EnergyHealingFieldData(data);
                
                onSpellCreatedSelectThis?.Invoke();
                
                UpdateLabels();
            };
        }
        
        public void InputField_SetAmount(string amn)
        {
            if(int.TryParse(amn, out int res))
            {
                data.TickAmount = res;
                UpdateLabels();
            }
        }

        public void InputField_SetFieldSize(string size)
        {
            if(int.TryParse(size, out int res))
            {
                data.FieldSize = res;
                UpdateLabels();
            }
        }

        public void InputField_SetHealStrength(string healstr)
        {
            if(int.TryParse(healstr, out int res))
            {
                data.HealStrength = res;
                UpdateLabels();
            }
        }
        
        public override void UpdateLabels()
        {
            manaCostLabel.text = $"Mana Cost: {data.ManaCost}";
            tickHealLabel.text = $"Heal per tick: {data.TickHeal}";
        }
    }
}
