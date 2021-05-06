using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WizardGame.ItemSystem.UI
{
    public abstract class ItemSlotUI : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}