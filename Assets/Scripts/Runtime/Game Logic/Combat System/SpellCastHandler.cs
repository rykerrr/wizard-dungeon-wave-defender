using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Item_System.Items;

namespace WizardGame.Combat_System
{
    public class SpellCastHandler : MonoBehaviour
    {
        [SerializeField] private SpellBookItem prewarmSpellBook = default;
        [SerializeField] private SpellCastBase equippedSpellCastBase = default;
        [SerializeField] private MonoBehaviour[] movementScripts;

        private Dictionary<SpellBookItem, SpellCastBase> existingSpellCasts =
            new Dictionary<SpellBookItem, SpellCastBase>();
        
        // will have a method to create spellcaster via prefab (which will also instantiate its spell circle)
        // ties input to firing, calls spell casty
        // perhaps handles the circle?
        // humble object pattern to make it modular so enemies can use it as well?

        private void Awake()
        {
            if (ReferenceEquals(prewarmSpellBook, null)) return;
            Equip(prewarmSpellBook);
        }

        public void TryCastSpell()
        {
            if (ReferenceEquals(equippedSpellCastBase, null)) return;
            
            equippedSpellCastBase.CastSpell();
        }

        public void TryEquipSpell(HotbarItem hotbarItem)
        {
            SpellBookItem spellItem = default;
            
            var isSpell = !ReferenceEquals(hotbarItem, null) &&
                          !ReferenceEquals(spellItem = hotbarItem as SpellBookItem, null);

            if (!isSpell) return;

            var equippedSpellExists = !ReferenceEquals(equippedSpellCastBase, null);
            
            if (equippedSpellExists && equippedSpellCastBase.IsCasting) return;

            if (!equippedSpellExists)
            {
                Equip(spellItem);
            }
            else
            {
                var dataIsEqual = ReferenceEquals(equippedSpellCastBase.SpellCastData, spellItem.SpellCastData);
                if (dataIsEqual) return;

                UnEquip();
                Equip(spellItem);
            }
        }
        
        // on item drop or item trash
        public bool RemoveSpellCast(HotbarItem baseItem)
        {
            SpellBookItem key = null;

            if (ReferenceEquals(baseItem, null) || ReferenceEquals(key = baseItem as SpellBookItem, null)) return false;

            return existingSpellCasts.Remove(key);
        }

        private void UnEquip()
        {
            equippedSpellCastBase.gameObject.SetActive(false);
            
            equippedSpellCastBase = null;
        }

        private void Equip(SpellBookItem baseItem)
        {
            SpellCastBase castToEquip = default;
            
            if(existingSpellCasts.ContainsKey(baseItem)) castToEquip = existingSpellCasts[baseItem];
            if(castToEquip == null) castToEquip = CreateSpellCast(baseItem);
            
            castToEquip.gameObject.SetActive(true);
            equippedSpellCastBase = castToEquip;
        }

        private SpellCastBase CreateSpellCast(SpellBookItem baseItem)
        {
            var spellCasterClone = Instantiate(baseItem.SpellCastPrefab, Vector3.zero, Quaternion.identity, transform);
            var spellCircleClone = Instantiate(baseItem.SpellCirclePrefab, Vector3.zero, Quaternion.identity);
            
            spellCircleClone.gameObject.SetActive(false);
            spellCasterClone.Init(gameObject, spellCircleClone, baseItem.SpellCastData, movementScripts);

            existingSpellCasts.Add(baseItem, spellCasterClone);
            return spellCasterClone;
        }
    }
}
