using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Talent_Tree;
using UnityEngine;
using WizardGame.Spell_Creation;
using WizardGame.Stats_System;

public class TalentLevelUpActionInjector : MonoBehaviour
{
    [Header("Dependency references")] [SerializeField]
    private StatsSystemBehaviour statsSystemBehav = default;

    [SerializeField] private List<ChangeSpellCreationData> spellCreationPanels = default;

    [Header("Preferences")] [SerializeField]
    private TalentHandler talentHandler = default;

    [SerializeField] private HiddenTalentHandler hiddenTalentHandler = default;

    public void InjectDependencies()
    {
        var normalTalents = talentHandler.Talents.Values;
        var hiddenTalents = hiddenTalentHandler.HiddenTalents;

        foreach (var talent in normalTalents)
        {
            InjectDependenciesIntoActionList(talent.TalentContainer.Talent.TalentLevelUpActions);
            InjectDependenciesIntoActionList(talent.TalentContainer.Talent.TalentUnlockActions);
        }

        foreach (var talent in hiddenTalents)
        {
            InjectDependenciesIntoActionList(talent.TalentLevelUpActions);
            InjectDependenciesIntoActionList(talent.TalentUnlockActions);
        }
    }

    private void InjectDependenciesIntoActionList(List<TalentLevelUpAction> actions)
    {
        foreach (var action in actions)
        {
            InjectDependenciesIntoAction(action);
        }
    }

    private void InjectDependenciesIntoAction(TalentLevelUpAction action)
    {
        switch (action)
        {
            case StatTalentLevelUpAction statLevelUp:
            {
                statLevelUp.StatsSystem = statsSystemBehav.StatsSystem;

                break;
            }
            case SpellUnlockLevelUpAction spellUnlock:
            {
                spellUnlock.SpellCreationPanel = spellCreationPanels.Find(x => spellUnlock.SpellWeUnlock.GetType()
                                                                               == x.ThisMenu.SpellFoundation.SpellPrefab
                                                                                   .GetType());

                // Debug.Log(
                    // $"SpellUnlock: {spellUnlock.Name}, Its spell: {spellUnlock.SpellWeUnlock} Type: {spellUnlock.SpellWeUnlock.GetType()}  ");
                // Debug.Log(
                    // $"menu: {spellUnlock.SpellCreationPanel} Its spell: {spellUnlock.SpellCreationPanel.ThisMenu.SpellFoundation}" +
                    // $" Type: {spellUnlock.SpellCreationPanel.ThisMenu.SpellFoundation.GetType()}");

                break;
            }
        }
    }
}