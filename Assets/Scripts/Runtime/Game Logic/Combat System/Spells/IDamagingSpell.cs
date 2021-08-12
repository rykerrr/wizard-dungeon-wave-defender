namespace WizardGame.Combat_System
{
    public interface IDamagingSpell
    {
        // TODO: Either remove this entirely or add IHealingSpell, IBuffingSpell
        // as to why it'd be removed, this forces ProcessOnHit() to be public
        // an abstract class could be used to circumvent it but the whole point of this
        // loses it's purpose then, a spell could be both damaging and/or healing and/or buffing...
        
        void CreateOnHitEffect();
    }
}