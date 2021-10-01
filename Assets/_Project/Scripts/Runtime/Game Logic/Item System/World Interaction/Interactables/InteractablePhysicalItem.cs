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

        private void OnEnable()
        {
            // May not need to be implemented, OnPlayerEnter/Exit will still be called
        }

        private void OnDisable()
        {
            // May not need to be implemented, OnPlayerEnter/Exit will still be called
        }

        public void OnPlayerEnter(Transform plr)
        {
            monoBehavEnabler.WaitEnableMonoBehaviour(false, gravBehaviour, forceBehaviour);
            monoBehavEnabler.WaitEnableMonoBehaviour(true, floatBehaviour);

            physItemRotation.OnPlayerEnter(plr);
        }

        public void OnPlayerExit(Transform plr)
        {
            monoBehavEnabler.ForceMonoBehaviourSet(false, floatBehaviour);
            monoBehavEnabler.ForceMonoBehaviourSet(true, gravBehaviour, forceBehaviour);

            physItemRotation.OnPlayerExit(plr);
        }

#if UNITY_EDITOR
        [Header("Debug")] [SerializeField] private Transform plr = default;

        [ContextMenu("OnPlayerEnter")]
        public void CallOnPlayerEnter() => OnPlayerEnter(plr);

        [ContextMenu("OnPlayerExit")]
        public void CallOnPlayerExit() => OnPlayerExit(plr);
#endif
    }
}