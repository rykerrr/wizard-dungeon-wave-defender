using System.Collections.Generic;
using System.Text;

namespace WizardGame.Stats_System
{
    public class DependantStat : StatBase
    {

        private List<StatDependency> statsDependingOn = new List<StatDependency>();
        public List<StatDependency> StatsDependingOn => statsDependingOn;

        private StringBuilder sb = new StringBuilder();

        public DependantStat(StatType defType) : base(defType)
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
                value += statDep.StatDependingOn.ActualValue;
                // Debug.Log("Step 1: " + statDep.StatDependingOn.ActualValue);
            }
            
            // Debug.Log("Step 2: " + value);
            value = ApplyModifiers(value);
            // Debug.Log("Step 3: " + value);
            
            return value;
        }

        public void AddStatDependency(StatBase statToAdd)
        {
            var statInDepList = StatsDependingOn.Find(x => x.StatDependingOn == statToAdd);
            var createNewStatDep = ReferenceEquals(statInDepList, null);
            
            if (createNewStatDep)
            {
                var statDep = new StatDependency(statToAdd, default);
                isDirty = true;

                StatsDependingOn.Add(statDep);
            }
            
            statToAdd.StatWasModified += DependenciesWereModified;
        }

        public bool RemoveStatDependency(StatBase statToRemove)
        {
            var statDep = StatsDependingOn.Find(x => x.StatDependingOn == statToRemove);
            var statIsntInDependencies = ReferenceEquals(statDep, null);
            if (statIsntInDependencies) return false;

            isDirty = true;
            
            statToRemove.StatWasModified -= DependenciesWereModified;
            
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