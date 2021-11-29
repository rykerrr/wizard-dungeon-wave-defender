#if UNITY_EDITOR

namespace WizardGame.ManualTestStuff
{
    public class InheritanceChild1 : InheritanceParent1
    {
        protected override string Gog => "Child";
    }
}

#endif