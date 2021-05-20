using System.Text;
using TMPro;
using UnityEngine;
using WizardGame.Item_System.Items;

namespace ItemSystem.World_Interaction
{
    public class PhysicalItem : MonoBehaviour
    {
        [SerializeField] private InventoryItem targetItem = default;
        
        [SerializeField] private GameObject billboardTextGui = default;
        [SerializeField] private TextMeshProUGUI itemText = default;
        
        public InventoryItem TargetItem => targetItem;

        public void Init(InventoryItem itemToInitialize)
        {
            targetItem = itemToInitialize;
            UpdateUI();
            // loading logic, changing the bilboard/mesh/particles/glow/etc...
        }

        private void UpdateUI()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(targetItem.ColouredName).AppendLine();
            sb.Append("Rarity: ").Append(targetItem.Rarity).AppendLine();
            sb.Append(targetItem.GetInfoDisplayText()).AppendLine();
            sb.Append("Sell price: ").Append(targetItem.SellPrice).AppendLine();

            itemText.text = sb.ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerInteractionBehavior>()) return;
            
            UpdateUI();
            billboardTextGui.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.GetComponent<PlayerInteractionBehavior>()) return;
            
            billboardTextGui.SetActive(false);
        }
    }
}