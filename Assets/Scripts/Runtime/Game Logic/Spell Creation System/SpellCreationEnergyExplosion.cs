using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WizardGame.Combat_System;
using WizardGame.Item_System;
using WizardGame.Item_System.Item_Containers;
using WizardGame.Item_System.Items;

namespace WizardGame.Spell_Creation
{
    public class SpellCreationEnergyExplosion : SpellCreationMenuBase
    {
        [SerializeField] private TextMeshProUGUI explosionDamageLabel = default;
        
        [SerializeField] private TMP_Dropdown locationDropdown = default;
        [SerializeField] private bool loadDropdownOptionsOnAwake = true;
        
        [SerializeField] private SpellBookItem baseItem = default;
        [SerializeField] private DirectedEnergyExplosionData data = default;

        public override SpellBookItem SpellFoundation => baseItem; 
        public override BaseSpellCastData Data => data;
        
        protected override void Awake()
        {
            base.Awake();
            
            if (!loadDropdownOptionsOnAwake) return;
            LoadDropdownOptions();
        }

        private void LoadDropdownOptions()
        {
            var enumVals = Enum.GetValues(typeof(DirectedEnergyExplosionData.ExplosionLocationType));
            List<TMP_Dropdown.OptionData> optionData = new List<TMP_Dropdown.OptionData>();

            foreach (var enumVal in enumVals)
            {
                optionData.Add(new TMP_Dropdown.OptionData(enumVal.ToString()));
            }

            locationDropdown.options = optionData;
        }

        public void Dropdown_SetLocation(int option)
        {
            data.Location = (DirectedEnergyExplosionData.ExplosionLocationType) option;
        }
        
        public void InputField_SetAmount(string amn)
        {
            if(int.TryParse(amn, out int res))
            {
                data.ExplosionAmount = res;
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
            explosionDamageLabel.text = $"Explosion Damage: {data.ExplosionDamage}";
        }
    }
}