using TMPro;
using UnityEngine;
using UnityEngine.Events;
using WizardGame.Combat_System;
using WizardGame.Item_System.Items;

namespace WizardGame.Spell_Creation
{
    public class SpellCreationEnergyPillar : SpellCreationMenuBase
    {
        [Header("Spell creation helper data")]
        [SerializeField] private SpellCreationHandler spellCreationHandler = default;
        [SerializeField] private UnityEvent onSpellCreatedSelectThis = default;
        
        [Header("Spell data")]
        [SerializeField] private TextMeshProUGUI shockwaveDamageLabel = default;

        [SerializeField] private SpellBookItem baseItem = default;
        [SerializeField] private EnergyPillarData data = default;
        
        public override SpellBookItem SpellFoundation => baseItem;
        public override BaseSpellCastData Data => data;

        protected override void Awake()
        {
            base.Awake();
            
            spellCreationHandler.onSpellCreated += (spellDataType) =>
            {
                if (spellDataType != data.GetType()) return;
                
                data = new EnergyPillarData(data);
                
                onSpellCreatedSelectThis?.Invoke();
                
                UpdateLabels();
            };
        }
        
        public void InputField_ShockwaveAmount(string amn)
        {
            if(int.TryParse(amn, out int res))
            {
                data.ShockwaveAmount = res;
                UpdateLabels();
            }
        }

        public void InputField_SpellSize(string size)
        {
            if(int.TryParse(size, out int res))
            {
                data.SpellSize = res;
                UpdateLabels();
            }
        }

        public void InputField_DelayBetweenWaves(string delay)
        {
            if(int.TryParse(delay, out int res))
            {
                data.DelayBetweenWaves = res;
                UpdateLabels();
            }
        }
        
        public override void UpdateLabels()
        {
            manaCostLabel.text = $"Mana Cost: {data.ManaCost}";
            shockwaveDamageLabel.text = $"Damage per shockwave: {data.BaseShockwaveDamage}";
        }
    }
}
