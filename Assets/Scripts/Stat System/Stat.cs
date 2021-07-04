namespace WizardGame.Stats_System
{
    public class Stat : StatBase
    {
        public Stat(StatType defType) : base(defType)
        {
            
        }
        
        public override int CalculateValue() => ApplyModifiers(baseValue);
    }
}