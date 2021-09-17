using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Combat_System.Element_System;

namespace WizardGame.SelectionWindow
{
    public class CreateWindowElements : MonoBehaviour
    {
        [SerializeField] private SelectionWindowElement elementPrefab = default;
        [SerializeField] private RotatingDisplayWindowHandler handler = default;

        [SerializeField] private Transform elementContainer = default;

        private List<Element> elementData = new List<Element>();

        private void Start()
        {
            Debug.Log("Creating SelectionWindow elements");
            
            CreateElementUis();
        }

        public void SetDataForUICreation(List<Element> datas)
        {
            elementData = datas;
        }
        
        public void CreateElementUis()
        {
            var elemList = new List<SelectionWindowElement>();

            foreach (var data in elementData)
            {
                var elemClone = Instantiate(elementPrefab, elementContainer);

                elemClone.Element = data;
                elemList.Add(elemClone);

                var button = elemClone.GetComponent<SetWindowElementInOverviewOnClick>();
                button.Init(handler);
            }

            handler.Init(elemList);
        }
    }
}