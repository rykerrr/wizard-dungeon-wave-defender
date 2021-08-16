using System;
using UnityEngine;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    [Serializable]
    public class ElementStatusEffectData
    {
        private static string namespacePath = "WizardGame.Combat_System.Element_System.Status_Effects";
        
        // Most serializable System.Type variations I've seen convert the name to a type
        // Seems a bit overkill to add an entire class for that as of now
        [Header("Not the type name itself, but just the prefix (e.g \"Wet\", \"OnFire\")")]
        [SerializeField] private string statusEffectTypeNamePrefix = "";
        
        [Header("Stat eff data")]
        [SerializeField] private float duration;
        [SerializeField] private int damagePerFrame = 0;

        private Type statusEffectType = null;
        private BaseStatusEffect statusEffect = null;

        public string StatusEffectName => statusEffectTypeNamePrefix;
        public float Duration => duration;
        public int DamagePerFrame => damagePerFrame;

        public Type StatEffectType
        {
            get
            {
                statusEffectType ??= Type.GetType($"{namespacePath}.{statusEffectTypeNamePrefix}StatusEffect");

                return statusEffectType;
            }
        }

        public BaseStatusEffect StatusEffect
        {
            get
            {
                statusEffect ??= StatusEffectFactory.GetStatusEffect(StatEffectType);
                
                return statusEffect;
            }
        }
    }
}