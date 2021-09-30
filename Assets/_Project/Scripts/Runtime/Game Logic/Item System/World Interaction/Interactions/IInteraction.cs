namespace WizardGame.Item_System.World_Interaction
{
    public interface IInteraction
    {
        bool TryInteract(IInteractable obj);
    }
}