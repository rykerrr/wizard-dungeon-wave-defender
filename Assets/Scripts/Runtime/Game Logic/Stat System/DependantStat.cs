using System.Collections.Generic;
using System.Text;

namespace WizardGame.Stats_System
{
    public class DependantStat : StatBase
    {
        private List<StatDependency> statsDependingOn = new List<StatDependency>();
        public List<StatDependency> StatsDependingOn => statsDependingOn;

        private StringBuilder sb = new StringBuilder();

        public DependantStat(StatType defType, float growthRate) : base(defType, growthRate)
        {
        }
        
        private void DependenciesWereModified()
        {
            isDirty = true;
            statWasModified.Invoke();
        }

        public override int CalculateValue()
        {
            int value = baseValue;
            
            foreach (var statDep in StatsDependingOn)
            {
                value += (int)(statDep.StatDependingOn.ActualValue * statDep.StatMultiplier);
            }
            
            value = ApplyModifiers(value);
            
            return value;
        }

        public void AddStatDependency(StatBase statToAdd, float multiplier)
        {
            var statInDepList = StatsDependingOn.Find(x => x.StatDependingOn == statToAdd);
            var createNewStatDep = ReferenceEquals(statInDepList, null);
            
            if (createNewStatDep)
            {
                var statDep = new StatDependency(statToAdd, multiplier);
                isDirty = true;

                StatsDependingOn.Add(statDep);
                
                statToAdd.statWasModified += DependenciesWereModified;
            }
        }

        public bool RemoveStatDependency(StatBase statToRemove)
        {
            var statDep = StatsDependingOn.Find(x => x.StatDependingOn == statToRemove);
            var statIsntInDependencies = ReferenceEquals(statDep, null);
            if (statIsntInDependencies) return false;

            isDirty = true;
            
            statToRemove.statWasModified -= DependenciesWereModified;
            
            return StatsDependingOn.Remove(statDep);
        }

        public override string ToString()
        {
            sb.Append("hello i have ").Append(StatsDependingOn.Count).Append(" dependencies").AppendLine();
                
            foreach (var statDep in StatsDependingOn)
            {
                sb.Append(statDep.StatDependingOn.ActualValue).Append(", ");
            }

            var retStr = sb.ToString();
            sb.Clear();
            
            return retStr;
        }
    }
}