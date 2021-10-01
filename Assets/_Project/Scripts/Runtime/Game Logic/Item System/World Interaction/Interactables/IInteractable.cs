using UnityEngine;

namespace WizardGame.Item_System.World_Interaction
{
    public interface IInteractable
    {
        string InteractableDescription { get; }
        
        void OnPlayerEnter(Transform plr);
        void OnPlayerExit(Transform plr);
    }
}