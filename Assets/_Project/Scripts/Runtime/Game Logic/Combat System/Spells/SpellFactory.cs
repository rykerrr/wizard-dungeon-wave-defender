using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using WizardGame.Combat_System.Element_System;

namespace WizardGame.Combat_System
{
    public static class SpellFactory
    {
        private static string spellLocationsInResources = "Prefabs/Combat System/Spells/Elemental";

        private static StringBuilder sb = new StringBuilder();

        private static Dictionary<(Type, Element), SpellBase> spells = new Dictionary<(Type, Element), SpellBase>();
        public static Dictionary<(Type, Element), SpellBase> Spells => spells;

        static SpellFactory()
        {
            InitSpells();
        }

        private static void InitSpells()
        {
            LoadSpellsByElement();
            DumpElementNamesAndSpells();
        }

        private static void LoadSpellsByElement()
        {
            var elemNames = ElementDB.Elements.Select(x => x.Name).ToList();
            
            if (elemNames.Count == 0) return;

            List<SpellBase> spellBases = new List<SpellBase>();
            
            foreach (var elName in elemNames)
            {
                var spellsOfElement = Resources.LoadAll<SpellBase>(Path.Combine(spellLocationsInResources, elName));
                
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
            spells.Clear();
            
            InitSpells();
        }
    }
}