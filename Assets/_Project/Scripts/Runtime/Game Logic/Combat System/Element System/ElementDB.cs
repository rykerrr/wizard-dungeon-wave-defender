using UnityEngine;
using System.Collections.Generic;
using Ludiq;

namespace WizardGame.Combat_System.Element_System
{
    public static class ElementDB
    {
        private static string elementsLocationInResources = "Game Data/Elements";

        private static readonly List<Element> elements = new List<Element>();

        public static IReadOnlyList<Element> Elements => elements.AsReadOnly();

        static ElementDB()
        {
            InitElements();
        }

        private static void InitElements()
        {
            var elementsToAdd = Resources.LoadAll<Element>(elementsLocationInResources);
            
            elements.AddRange(elementsToAdd);
        }

        public static Element GetElementByName(string elName)
        {
            return elements.Find(x => x.Name == elName);
        }
    }
}