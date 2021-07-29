using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Combat_System.Cooldown_System;
using WizardGame.Item_System.Items;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public class SpellCastHandler : MonoBehaviour
    {
        [Header("Spell cast dependencies")] 
        [SerializeField] private StatsSystemBehaviour statsSystemBehaviour = default;
        [SerializeField] private CooldownSystem cooldownSys;
        [SerializeField] private MonoBehaviour[] movementScripts;

        [Header("Debug")]
        [SerializeField] private SpellBookItem prewarmSpellBook = default;
        [SerializeField] private SpellCastBase equippedSpellCastBase = default;

        private Dictionary<SpellBookItem, SpellCastBase> existingSpellCasts =
            new Dictionary<SpellBookItem, SpellCastBase>();
        
        // will have a method to create spellcaster via prefab (which will also instantiate its spell circle)
        // ties input to firing, calls spell casty
        // perhaps handles the circle?
        // humble object pattern to make it modular so enemies can use it as well?

        private void Awake()
        {
            statsSystemBehaviour ??= GetComponent<StatsSystemBehaviour>();
            cooldownSys ??= GetComponent<CooldownSystem>();
            
            if (ReferenceEquals(prewarmSpellBook, null)) return;
            Equip(prewarmSpellBook);
        }

        public void TryCastSpell(InputAction.CallbackContext ctx)
        {
            if (ctx.phase != InputActionPhase.Started) return;
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
                var dataIsEqual = ReferenceEquals(equippedSpellCastBase.Data, spellItem.SpellCastData);
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
            spellCasterClone.Init(gameObject, statsSystemBehaviour.StatsSystem, cooldownSys
                , baseItem.Id, spellCircleClone, baseItem.SpellCastData, movementScripts);
            
            existingSpellCasts.Add(baseItem, spellCasterClone);
            return spellCasterClone;
        }
    }
}