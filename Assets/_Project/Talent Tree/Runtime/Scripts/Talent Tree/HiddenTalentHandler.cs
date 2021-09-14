using System;
using System.Collections;
using System.Collections.Generic;
using Talent_Tree.Dynamic_Talent_Tree;
using Talent_Tree.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Talent_Tree
{
	public class HiddenTalentHandler : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private TalentOverviewUI talentOverviewUI = default;
		[SerializeField] private RectTransform hiddenTalentGrid = default;
		[SerializeField] private GameObject showHiddenTalentButton = default;
		
		[Header("Preferences")]
		[SerializeField] private DynamicTalentUI hiddenTalentPrefab = default;
		[SerializeField] private List<Talent> hiddenTalents = new List<Talent>();

		private int count = 0;

		private void Update()
		{
			if (Keyboard.current.spaceKey.wasPressedThisFrame)
			{
				if (count == hiddenTalents.Count) return;
				
				Debug.Log("Unlocked: " + TryUnlockHiddenTalent(hiddenTalents[count++]));
			}
		}

		public bool TryUnlockHiddenTalent(Talent talent)
		{
			if (!hiddenTalents.Contains(talent)) return false;

			if(!showHiddenTalentButton.activeSelf) showHiddenTalentButton.SetActive(true);
			
			CreateTalentUI(talent);
			
			return true;
			// create it's UI
			// unlock it
		}

		private void CreateTalentUI(Talent talent)
		{
			var talentClone = Instantiate(hiddenTalentPrefab, hiddenTalentGrid);
			talentClone.gameObject.SetActive(true);
			talentClone.GetComponent<OverviewSelectTalentButton>().TalentOverviewUI = talentOverviewUI;

			talentClone.Init(talent, null, UnlockState.Unlockable);
			
			var leveledUp = talentClone.TryLevelUp(talentClone.TalentContainer.Talent.SingleLevelWeight);

			if (!leveledUp)
			{
				Debug.LogError("Couldn't unlock hidden talent?");
				Debug.Break();
				
				return;
			}
		}
	}
}
