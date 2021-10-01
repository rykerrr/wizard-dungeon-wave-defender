using System;
using WizardGame.Item_System.Items;
using UnityEngine;
using WizardGame.Item_System.World_Interaction.PhysicalItemMovementMotor;
using WizardGame.Movement.Position;
using WizardGame.Movement.Rotation;

namespace WizardGame.Item_System.World_Interaction
{
    public class InteractablePhysicalItem : MonoBehaviour, IInteractable
    {
        [Header("References")] 
        [SerializeField] private PhysicalItemRotationBehaviour physItemRotation = default;
        [SerializeField] private GravityMovementBehaviour gravBehaviour = default;
        [SerializeField] private ForceReceiverMovementBehaviour forceBehaviour = default;
        [SerializeField] private FloatOverGroundMovementBehaviour floatBehaviour = default;
        [SerializeField] private SetMonoBehavioursActiveAfterWait monoBehavEnabler = default;

        [Header("Preferences")] [SerializeField]
        private InventoryItem targetItem = default;

        [SerializeField] private string interactableDescription = default;

        public InventoryItem TargetItem => targetItem;
        public string InteractableDescription => $"Item: {TargetItem.Name}\n{interactableDescription}";

        private int enterCount = 0;
        
        private void Awake()
        {
            physItemRotation ??= GetComponent<PhysicalItemRotationBehaviour>();
            gravBehaviour ??= GetComponent<GravityMovementBehaviour>();
            forceBehaviour ??= GetComponent<ForceReceiverMovementBehaviour>();
            floatBehaviour ??= GetComponent<FloatOverGroundMovementBehaviour>();
            monoBehavEnabler ??= GetComponent<SetMonoBehavioursActiveAfterWait>();
        }

        public void Init(InventoryItem itemToInit)
        {
            targetItem = itemToInit;
        }

        public void OnCharacterEnter(Transform plr)
        {
            enterCount++;
            
            monoBehavEnabler.WaitEnableMonoBehaviour(false, gravBehaviour, forceBehaviour);
            monoBehavEnabler.WaitEnableMonoBehaviour(true, floatBehaviour);

            var isDisabled = !physItemRotation.enabled;
            if (isDisabled) physItemRotation.enabled = true;
            
            physItemRotation.OnCharacterEnter(plr);
        }

        public void OnCharacterExit(Transform plr)
        {
            enterCount--;

            if (enterCount < 0)
            {
                Debug.LogError($"We have an issue, enterCount under 0, last to acess: {plr}");
                
                throw new ArgumentOutOfRangeException();
            }
            
            if (enterCount == 0)
            {
                monoBehavEnabler.ForceMonoBehaviourSet(false, floatBehaviour);
                monoBehavEnabler.ForceMonoBehaviourSet(true, gravBehaviour, forceBehaviour);
                
                var isEnabled = physItemRotation.enabled;
                if (isEnabled) physItemRotation.enabled = false;
            }

            physItemRotation.OnCharacterExit(plr);
        }

        private void OnDisable()
        {
            monoBehavEnabler.ForceMonoBehaviourSet(false, floatBehaviour);
            monoBehavEnabler.ForceMonoBehaviourSet(true, gravBehaviour, forceBehaviour);

            enterCount = 0;
        }

#if UNITY_EDITOR
        [Header("Debug")] [SerializeField] private Transform plr = default;

        [ContextMenu("OnPlayerEnter")]
        public void CallOnPlayerEnter() => OnCharacterEnter(plr);

        [ContextMenu("OnPlayerExit")]
        public void CallOnPlayerExit() => OnCharacterExit(plr);
#endif
    }
}