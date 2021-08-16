using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public static class StatusEffectFactory
    {
        private static List<Type> statEffTypes = new List<Type>();

        private static StringBuilder sb = new StringBuilder();
        
        static StatusEffectFactory()
        {
            var types = typeof(BaseStatusEffect).Assembly.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(BaseStatusEffect)));
            
            statEffTypes.AddRange(types);
            
            DumpStatusEffectData();
        }

        public static BaseStatusEffect GetStatusEffect(Type statEffType)
        {
            if (!statEffTypes.Contains(statEffType)) return null;

            return (BaseStatusEffect) Activator.CreateInstance(statEffType);
        }

        private static void DumpStatusEffectData()
        {
            sb.Clear();
            
            foreach (var statEff in statEffTypes)
            {
                sb.Append("Type: ").Append(statEff).Append(" | Name: ").Append(statEff.Name).AppendLine();
            }
            
            Debug.Log(sb.ToString());
        }
    }
}