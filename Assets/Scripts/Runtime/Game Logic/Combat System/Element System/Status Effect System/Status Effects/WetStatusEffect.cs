namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class WetStatusEffect : StatusEffect
    {
        public override void Tick()
        {
            // throw out water particles? though it'd be better if we created a timer that ran it every x seconds instead
            // of each frame
            
            return;
        }

        public override void OnRemove()
        {
            
        }
    }
}