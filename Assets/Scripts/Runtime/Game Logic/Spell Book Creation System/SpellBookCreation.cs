using System;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WizardGame.Combat_System;
using WizardGame.Item_System;
using WizardGame.Item_System.Item_Containers;
using WizardGame.Item_System.Items;

namespace WizardGame.Spell_Book_Creation_System
{
    public class SpellBookCreation : MonoBehaviour
    {
        [Header("Scene Or Asset References")] 
        [SerializeField] private Inventory targetInventory = default;
        
        [SerializeField] private TextMeshProUGUI gravMagLabelText = default;
        [SerializeField] private TextMeshProUGUI speedMultLabelText = default;
        [SerializeField] private TextMeshProUGUI dmgMultLabelText = default;
        [SerializeField] private TextMeshProUGUI castSpeedMultLabelText = default;
        [SerializeField] private TextMeshProUGUI castCdMultLabelText = default;
        [SerializeField] private TextMeshProUGUI castAmnLabelText = default;
        [SerializeField] private TMP_Dropdown spellFoundationDropdown = default;

        [Header("Properties")] [SerializeField]
        private SpellBookItem[] spellDropdownChoices = default;

        [Header("Displayed for debug")] [SerializeField]
        private SpellCastData spellCastData;

        [SerializeField] private SpellBookItem createdItem;

        private SpellBookItem selectedSpellFoundation = default;

        private void Awake()
        {
            if (spellDropdownChoices.Length <= 0)
            {
                Debug.LogWarning("Spell choices weren't loaded, perhaps you forgot to press the ContextMenu" +
                                 "button for it?", this);
                ReloadDropdownData();
            }
            
            UpdateGravityLabelDisplay();

            SetSpellFoundation(0);
        }

        public void SetSpeedMult(string speedMult)
        {
            if (string.IsNullOrEmpty(speedMult)) return;
            
            spellCastData.SpeedMultiplier = Convert.ToSingle(speedMult);
            speedMultLabelText.text = speedMult;
        }

        public void SetStrengthMultiplier(string strMult)
        {
            if (string.IsNullOrEmpty(strMult)) return;
            
            spellCastData.SpellStrength = Convert.ToSingle(strMult);
            dmgMultLabelText.text = strMult;
        }

        public void SetCastSpeedMult(string castSpeedMult)
        {
            if (string.IsNullOrEmpty(castSpeedMult)) return;
            
            spellCastData.CastingSpeed = Convert.ToSingle(castSpeedMult);
            castSpeedMultLabelText.text = castSpeedMult;
        }

        public void SetCastCdMult(string castCdMult)
        {
            if (string.IsNullOrEmpty(castCdMult)) return;
            
            spellCastData.CastCooldownMultiplier = Convert.ToSingle(castCdMult);
            castCdMultLabelText.text = castCdMult;
        }

        public void SetCastAmount(string castAmn)
        {
            if (string.IsNullOrEmpty(castAmn)) return;
            
            spellCastData.CastAmn = Convert.ToInt32(castAmn);
            castAmnLabelText.text = castAmn;
        }
        
        public void SetSpellFoundation(int index)
        {
            if (index > spellDropdownChoices.Length)
            {
#if UNITY_EDITOR
                Debug.LogWarning("There is actually less choices on the dropdown than the currently" +
                                 " selected index");
#endif

                return;
            }

            selectedSpellFoundation = spellDropdownChoices[index];
        }

        public void UpdateGravityLabelDisplay()
        {
            gravMagLabelText.text = $"Gravity Magnitude: {spellCastData.GravMagnitude}";
        }

        public void CreateSpellBook()
        {
            var spell = selectedSpellFoundation;
            var createdItem = (SpellBookItem) ScriptableObject.CreateInstance(spell.GetType());
            
            createdItem.Init(spell.SpellCastPrefab, spell.SpellCirclePrefab, SpellCastData);
            createdItem.Init(spell.Rarity, spell.SellPrice, spell.MaxStack);
            createdItem.Init(spell.name, spell.Icon);
            createdItem.ItemUseEvent = spell.ItemUseEvent;
           
            if (createdItem == null) return;

            this.createdItem = createdItem;
            targetInventory.ItemContainer.AddItem(new ItemSlot(createdItem, 1));
        }

        public SpellCastData SpellCastData => spellCastData;

        #region debug

        [ContextMenu("Reload dropdown data")]
        private void ReloadDropdownData()
        {
            spellFoundationDropdown.ClearOptions();
            
            List<TMP_Dropdown.OptionData> data = spellDropdownChoices
                .Select(item => new TMP_Dropdown.OptionData(item.Name, item.Icon)).ToList();

            spellFoundationDropdown.AddOptions(data);
        }

        [ContextMenu("Try auto create spell")]
        private void TryAutoCreateSpell()
        {
            SetSpeedMult("15");
            SetStrengthMultiplier("5");
            SetCastSpeedMult("5");
            SetCastCdMult("2");
            
            CreateSpellBook();
        }

        [ContextMenu("Try auto create and dump")]
        private void TryAutoCreateAndDump()
        {
            TryAutoCreateSpell();
            DumpItemInfo();
        }
        
        [ContextMenu("Dump item info")]
        private void DumpItemInfo()
        {
            if (ReferenceEquals(createdItem, null))
            {
                Debug.LogWarning("the created item is null you doofus");
                return;
            }

            Debug.Log(createdItem.ToString());
        }

        #endregion
    }
}
