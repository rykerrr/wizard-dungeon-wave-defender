using System.Collections.Generic;
using UnityEngine;

namespace SelectionWindow.ManualTesting
{
    public class InjectStubDataToElementWindowCreator : MonoBehaviour
    {
        [SerializeField] private CreateWindowElements windowCreator = default;

        [SerializeField] private List<WindowElementData> elemDatas = default;

        private void Awake()
        {
            windowCreator.SetDataForUICreation(elemDatas);
        }
    }
}
