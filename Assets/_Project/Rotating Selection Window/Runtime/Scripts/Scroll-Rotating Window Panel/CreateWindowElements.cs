using System.Collections.Generic;
using UnityEngine;

namespace SelectionWindow
{
    public class CreateWindowElements : MonoBehaviour
    {
        [SerializeField] private SelectionWindowElement elementPrefab = default;
        [SerializeField] private RotatingDisplayWindowHandler handler = default;

        [SerializeField] private Transform elementContainer = default;

        public void CreateElementUis(List<WindowElementData> datas)
        {
            var elemList = new List<SelectionWindowElement>();

            foreach (var data in datas)
            {
                var elemClone = Instantiate(elementPrefab, elementContainer);

                elemClone.Data = data;
                elemList.Add(elemClone);

                var button = elemClone.GetComponent<SetWindowElementInOverviewOnClick>();
                button.Init(handler);
            }

            handler.Init(elemList);
        }
    }
}