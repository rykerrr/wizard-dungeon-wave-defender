using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using WizardGame.Item_System.Items;
using WizardGame.Movement.Position;
using WizardGame.Movement.Rotation;

namespace WizardGame.Item_System.World_Interaction
{
    [Serializable]
    public class PhysicalItemDisplay
    {
        [Header("References")]
        [SerializeField] private InventoryItem targetItem = default;
        [SerializeField] private GameObject billboardTextGui = default;
        [SerializeField] private TextMeshProUGUI itemText = default;

        public InventoryItem TargetItem => targetItem;

        private StringBuilder sb = new StringBuilder();

        public void Init(InventoryItem itemToInitialize)
        {
            targetItem = itemToInitialize;
            UpdateUI();
            // loading logic, changing the bilboard/mesh/particles/glow/etc...
        }

        private void UpdateUI()
        {
            sb.Append(targetItem.ColouredName).AppendLine();
            sb.Append("Rarity: ").Append(targetItem.Rarity).AppendLine();
            sb.Append(targetItem.GetInfoDisplayText()).AppendLine();
            sb.Append("Sell price: ").Append(targetItem.SellPrice).AppendLine();

            itemText.text = sb.ToString();
            sb.Clear();
        }

        public void OnPlayerEnter()
        {
            UpdateUI();
            billboardTextGui.SetActive(true);
        }

        public void OnPlayerExit()
        {
            billboardTextGui.SetActive(false);
        }
    }
}