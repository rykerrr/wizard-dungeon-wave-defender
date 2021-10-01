using UnityEngine;

namespace WizardGame.Item_System.World_Interaction
{
    public interface IInteractable
    {
        string InteractableDescription { get; }
        
        void OnCharacterEnter(Transform plr);
        void OnCharacterExit(Transform plr);
    }
}