namespace WizardGame.Item_System.World_Interaction
{
    public interface IInteractable
    {
        string InteractableDescription { get; }
        
        void OnPlayerEnter();
        void OnPlayerExit();
    }
}