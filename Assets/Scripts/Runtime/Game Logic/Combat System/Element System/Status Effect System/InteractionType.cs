namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public enum InteractionType
    {
        // the ones that remove themselves should come first as they're practically removing themselves 
        RemoveBoth,
        RemoveAndCombine,
        // can be either damage or speed, positive or negative
        ModifySpellEffectiveness
    }
}