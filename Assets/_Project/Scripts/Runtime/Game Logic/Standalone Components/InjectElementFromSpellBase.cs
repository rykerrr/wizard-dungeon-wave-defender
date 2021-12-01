using UnityEngine;
using WizardGame.Combat_System;
using WizardGame.Combat_System.Spell_Effects;

namespace WizardGame
{
    public class InjectElementFromSpellBase : MonoBehaviour
    {
        [SerializeField] private SpellBase spellBase;
        [SerializeField] private OnParticleCollisionDamageWaterPuddle objToInjectInto;

        private void Awake()
        {
            Inject();
        }

        private void Inject()
        {
            objToInjectInto.Init(spellBase.SpellElement);
        }
    }
}