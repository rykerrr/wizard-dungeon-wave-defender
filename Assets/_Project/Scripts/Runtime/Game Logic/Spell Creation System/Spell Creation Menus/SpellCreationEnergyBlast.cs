using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using WizardGame.Combat_System;
using WizardGame.Item_System.Items;

namespace WizardGame.Spell_Creation
{
    public class SpellCreationEnergyBlast : SpellCreationMenuBase
    {
        [Header("Spell creation helper data")]
        [SerializeField] private SpellCreationHandler spellCreationHandler = default;
        [SerializeField] private UnityEvent onSpellCreatedSelectThis = default;
        
        [Header("Spell data")]        
        [SerializeField] private TextMeshProUGUI impactDamageLabel = default;
        [SerializeField] private TextMeshProUGUI explosionDamageLabel = default;

        [SerializeField] private SpellBookItem baseItem = default;
        [SerializeField] private EnergyBlastData data = default;

        public override SpellBookItem SpellFoundation => baseItem;
        public override BaseSpellCastData Data => data;

        protected override void Awake()
        {
            base.Awake();

            spellCreationHandler.onSpellCreated += (spellDataType) =>
            {
                if (spellDataType != data.GetType()) return;
                
                Debug.Log(Data + " | " + Activator.CreateInstance(Data.GetType()));
                
                data = new EnergyBlastData(data);
                
                onSpellCreatedSelectThis?.Invoke();
                
                UpdateLabels();
            };
        }
        
        public void InputField_SetAmount(string amn)
        {
            if (int.TryParse(amn, out int res))
            {
                data.BlastAmount = res;
                UpdateLabels();
            }
        }

        public void InputField_SetImpactSize(string size)
        {
            if (float.TryParse(size, out float res))
            {
                data.ImpactSize = res;
                UpdateLabels();
            }
        }

        public void InputField_SetExplosionSize(string size)
        {
            if (float.TryParse(size, out float res))
            {
                data.ExplosionSize = res;
                UpdateLabels();
            }
        }

        public override void UpdateLabels()
        {
            manaCostLabel.text = $"Mana Cost: {data.ManaCost}";
            impactDamageLabel.text = $"Impact Damage: {data.BaseImpactDamage}";
            explosionDamageLabel.text = $"Explosion Damage: {data.BaseExplosionDamage}";
        }
    }
}