using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Combat_System;
using WizardGame.Combat_System.Cooldown_System;
using WizardGame.Item_System.Item_Containers;

namespace WizardGame.ManualTestStuff
{
    public class TurretTest : MonoBehaviour
    {
        [SerializeField] private LocalInventoryBehaviour localInventory = default;
        [SerializeField] private SpellCastHandler spellCastHandler = default;
        [SerializeField] private CooldownSystem cdSys = default;
        
        private void Start()
        {
            var firstSpell = localInventory.LocalItemContainer[0];
            cdSys.AddCooldown(firstSpell.invItem.CooldownData);

            spellCastHandler.TryEquipSpell(firstSpell.invItem);
        }

        private void Update()
        {
            if (Keyboard.current.lKey.wasPressedThisFrame)
            {
                spellCastHandler.TryCastSpell();
            }
        }
    }
}