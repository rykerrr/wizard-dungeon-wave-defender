using UnityEngine;

namespace SelectionWindow
{
    public class SelectElementFromOverviewOnClick : MonoBehaviour
    {
        [SerializeField] private RotatingWindowElementOverview overviewWindow = default;

        public void OnClick_ProcessSelect()
        {
            Debug.Log($"selected {overviewWindow.SelectedElement.Data.Name}");
        }
    }
}