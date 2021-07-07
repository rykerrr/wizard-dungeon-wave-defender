namespace WizardGame.Stats_System
{
    public class Stat : StatBase
    {
        public Stat(StatType defType, float growthRate) : base(defType, growthRate)
        {
            
        }
        
        public override int CalculateValue() => ApplyModifiers(baseValue);
    }
}