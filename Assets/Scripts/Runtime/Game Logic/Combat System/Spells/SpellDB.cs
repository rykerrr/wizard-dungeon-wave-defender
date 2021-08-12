using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using WizardGame.Combat_System.Element_System;

namespace WizardGame.Combat_System
{
    public static class SpellDB
    {
        private static string elementsLocationInResources = "Game Data/Elements";
        private static string spellLsocationsInResources = "Prefabs/Combat System/Spells/Elemental";

        private static List<string> elementNames = new List<string>();

        private static StringBuilder sb = new StringBuilder();

        private static Dictionary<(Type, Element), SpellBase> spells = new Dictionary<(Type, Element), SpellBase>();
        public static Dictionary<(Type, Element), SpellBase> Spells => spells;

        static SpellDB()
        {
            InitSpells();
        }

        private static void InitSpells()
        {
            LoadElementNames();
            LoadSpellsByElement();
            DumpElementNamesAndSpells();
        }

        private static void LoadElementNames()
        {
            var elements = Resources.LoadAll<Element>(elementsLocationInResources);

            elementNames = elements.Select(x => x.Name).ToList();
        }

        private static void LoadSpellsByElement()
        {
            if (elementNames.Count == 0) return;

            List<SpellBase> spellBases = new List<SpellBase>();
            
            foreach (var elName in elementNames)
            {
                var spellsOfElement = Resources.LoadAll<SpellBase>(Path.Combine(spellLsocationsInResources, elName));
                
                spellBases.AddRange(spellsOfElement);
            }

            foreach (var spell in spellBases)
            {
                var key = (spell.GetType(), spell.SpellElement);

                try
                {
                    spells.Add(key, spell);
                }
                catch (ArgumentException e)
                {
                    Debug.LogError(key.Item1 + " | " + key.Item2 + " | " + spell.name + "\n" + e);
                }
            }
        }

        private static void DumpElementNamesAndSpells()
        {
            foreach (var spellKvp in spells)
            {
                var key = spellKvp.Key;
                
                // ebug.Log("Spell type: " + key.Item1 + " | Spell Element: " + key.Item2.Name + " | Spell: " + spellKvp.Value);
            }
        }
        
        public static void ReloadSpells()
        {
            elementNames.Clear();
            spells.Clear();
            
            InitSpells();
        }
        
        public static void CallToLoad()
        {
            
        }
    }
}