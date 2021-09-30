using UnityEngine;

namespace WizardGame.Item_System.World_Interaction
{
    public class UIStandInteractions : IInteraction
    {
        private void ShowUI(GameObject obj)
        {
            obj.SetActive(true);
        }

        public bool TryInteract(IInteractable obj)
        {
            Debug.Log($"Interacting! Obj is of given type: { obj is InteractableUIShowingStand }, {obj}");

            if (!(obj is InteractableUIShowingStand uiStand)) return false;

            ShowUI(uiStand.UIObjectToShow);

            return true;
        }
    }
}