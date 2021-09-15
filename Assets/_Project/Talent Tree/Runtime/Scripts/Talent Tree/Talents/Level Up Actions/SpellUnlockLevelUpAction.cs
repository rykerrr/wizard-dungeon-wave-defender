using UnityEngine;
using WizardGame.Combat_System;
using WizardGame.Spell_Creation;

namespace Talent_Tree
{
    [CreateAssetMenu(menuName = "Talents/New Talent Spell Unlock Levelup Action", fileName = "New Talent Spell Unlock Levelup Action")]
    public class SpellUnlockLevelUpAction : TalentLevelUpAction
    {
        [SerializeField] private SpellBase spellWeUnlock;

        public SpellBase SpellWeUnlock => spellWeUnlock;
        public ChangeSpellCreationData SpellCreationPanel { get; set; }
        
        public override void LevelUp()
        {
            var missingRef = ReferenceEquals(SpellCreationPanel, null);
            if (missingRef)
            {
                Debug.LogError("Missing SpellCreationPanel Reference", this);

                return;
            }
            
            if(SpellCreationPanel.gameObject.activeSelf) Debug.LogWarning($"SpellCreationPanel {SpellCreationPanel} is already active (activeSelf)");
            
            SpellCreationPanel.gameObject.SetActive(true);
        }
    }
}