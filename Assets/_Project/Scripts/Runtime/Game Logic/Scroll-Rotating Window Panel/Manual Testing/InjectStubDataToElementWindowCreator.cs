using System.Collections.Generic;
using UnityEngine;
using WizardGame.Combat_System.Element_System;

namespace WizardGame.SelectionWindow.ManualTesting
{
    public class InjectStubDataToElementWindowCreator : MonoBehaviour
    {
        [SerializeField] private CreateWindowElements windowCreator = default;

        [SerializeField] private List<Element> elements = default;

        private void Awake()
        {
            windowCreator.SetDataForUICreation(elements);
        }
    }
}
