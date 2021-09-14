using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Talent_Tree.UI
{
	public class OverviewSelectTalentButton : MonoBehaviour
	{
		[SerializeField] private TalentOverviewUI talentOverviewUI = default;
		[SerializeField] private DynamicTalentUI talentUi = default;
		[SerializeField] private Button thisButton = default;
		
		public TalentOverviewUI TalentOverviewUI
		{
			get => talentOverviewUI;
			set
			{
				thisButton.onClick.RemoveAllListeners();
				
				talentOverviewUI = value;

				thisButton.onClick.AddListener(OnClick_TrySelectThisTalent);
			}
		}

		private void Awake()
		{
			var talentUiNull = ReferenceEquals(talentUi, null);
			
			if (talentUiNull)
			{
				talentUi = GetComponent<DynamicTalentUI>();
			}
		}

		public void OnClick_TrySelectThisTalent()
		{
			talentOverviewUI.SelectTalent(talentUi);
		}
	}
}
