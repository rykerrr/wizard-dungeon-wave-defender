using UnityEngine;

namespace SelectionWindow
{
    public class SetWindowElementInOverviewOnClick : MonoBehaviour
    {
        [SerializeField] private RotatingDisplayWindowHandler handler = default;

        private int ind = 0;

        public void Init(RotatingDisplayWindowHandler handler)
        {
            this.handler = handler;
        }
        
        private void Awake()
        {
            ind = transform.GetSiblingIndex();
        }

        public void OnClick_SetInOverviewWindow()
        {
            handler.SelectElement(ind);
        }
    }
}
