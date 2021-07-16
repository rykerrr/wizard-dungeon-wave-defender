using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WizardGame.Combat_System;
using WizardGame.Item_System;
using WizardGame.Item_System.Item_Containers;
using WizardGame.Item_System.Items;

namespace WizardGame.Spell_Creation
{
    public class SpellCreationEnergyBlast : SpellCreationMenuBase
    {
        [SerializeField] private TextMeshProUGUI impactDamageLabel = default;
        [SerializeField] private TextMeshProUGUI explosionDamageLabel = default;
        
        [SerializeField] private SpellBookItem baseItem = default;
        [SerializeField] private EnergyBlastData data = default;

        public override SpellBookItem SpellFoundation => baseItem; 
        public override BaseSpellCastData Data => data;
        
        public void InputField_SetAmount(string amn)
        {
            if(int.TryParse(amn, out int res))
            {
                data.BlastAmount = res;
                UpdateLabels();
            }
        }

        public void InputField_SetImpactSize(string size)
        {
            if(float.TryParse(size, out float res))
            {
                data.ImpactSize = res;
                UpdateLabels();
            }
        }

        public void InputField_SetExplosionSize(string size)
        {
            if(float.TryParse(size, out float res))
            {
                data.ExplosionSize = res;
                UpdateLabels();
            }
        }

        public override void UpdateLabels()
        {
            manaCostLabel.text = $"Mana Cost: {data.ManaCost}";
            impactDamageLabel.text = $"Impact Damage: {data.ImpactDamage}";
            explosionDamageLabel.text = $"Explosion Damage: {data.ExplosionDamage}";
        }
    }
}
