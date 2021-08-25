using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public static class StatusEffectFactory
    {
        // load all types from the assembly

        private static string locationInResources = "Game Data/Status Effects";
        private static string namespacePath = "WizardGame.Combat_System.Element_System.Status_Effects";

        private static StringBuilder sb = new StringBuilder();

        private static Dictionary<StatusEffectData, Type> statusDataTypes 
            = new Dictionary<StatusEffectData, Type>();

        static StatusEffectFactory()
        {
            var dataTypes = Resources.LoadAll<StatusEffectData>(locationInResources);

            foreach (var data in dataTypes)
            {
                var loadedType = Type.GetType($"{namespacePath}.{data.Name}StatusEffect");
                
                statusDataTypes.Add(data, loadedType);
            }
            
            // DumpStatusEffectData();
        }

        public static StatusEffect CreateStatusEffect(StatusEffectData statEffData)
        {
            if (!statusDataTypes.ContainsKey(statEffData)) return null;

            return (StatusEffect) Activator.CreateInstance(statusDataTypes[statEffData]);
        }

        public static StatusEffect CreateStatusEffect(StatusEffectData statEffData, GameObject caster,
            GameObject target)
        {
            var statEff = CreateStatusEffect(statEffData);
            if (statEff == null) return null;
            
            statEff.Init(caster, target, statEffData);

            return statEff;
        }

        public static Type GetType(StatusEffectData data)
        {
            if (!statusDataTypes.ContainsKey(data)) return null;

            return statusDataTypes[data];
        }

        #if UNITY_EDITOR
        private static void DumpStatusEffectData()
        {
            sb.Clear();

            foreach (var statEff in statusDataTypes)
            {
                sb.Append("Type: ").Append(statEff).Append(" | Name: ").Append(statEff.Value).AppendLine();
            }

            Debug.Log(sb.ToString());
        }
        #endif
    }
}