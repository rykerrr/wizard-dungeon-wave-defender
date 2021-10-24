using WizardGame.Combat_System.Element_System.Status_Effects;

namespace WizardGame.Health_System
{
    public class DamageResult
    {
        private bool tookDamage;
        private StatusEffectAddResult statEffAddRes;

        public bool TookDamage
        {
            get => tookDamage;
            set => tookDamage = value;
        }
        public StatusEffectAddResult StatEffAddRes
        {
            get => statEffAddRes;
            set => statEffAddRes = value;
        }

        public DamageResult(bool tookDamage)
        {
            this.tookDamage = tookDamage;
        }
        
        public DamageResult(StatusEffectAddResult statEffAddRes, bool tookDamage) : this(tookDamage)
        {
            this.statEffAddRes = statEffAddRes;
        }
    }
}