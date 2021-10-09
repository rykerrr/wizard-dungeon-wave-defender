using UnityEngine;
using UnityEngine.InputSystem;

namespace WizardGame.Movement.Rotation.ForwardModifiers
{
    public class SetObjectForwardTowardsMouse : MonoBehaviour, IForwardModifier
    {
        [SerializeField] private SetObjectForward forwSetter = default;
        [SerializeField] private Transform target = default;

        public Vector3 Value { get; private set; }
        
        private Camera mainCam = default;

        private void Awake()
        {
            Init();
        }

        public void Init(Camera mainCam = null, Transform target = null)
        {
            this.mainCam = mainCam == null ? Camera.main : mainCam;

            var targIsNull = ReferenceEquals(target, null);
            if (targIsNull) return;
            this.target = target;
        }

        private void Update()
        {
            var pos = GetMousePointOnPlane();

            Value = pos;
            
            forwSetter.TrySetNextForwardVector(this);
        }

        public Vector3 GetMousePointOnPlane()
        {
            var mPosScreen = Mouse.current.position.ReadValue();

            var mRay = mainCam.ScreenPointToRay(mPosScreen);
            
            var targPos = target.position;
            
            var plrPlane = new Plane(target.up, targPos);
            plrPlane.Raycast(mRay, out var enter);
            
            var hitPoint = mRay.GetPoint(enter);

            var relative = (hitPoint - targPos).normalized;
            
            return new Vector3(relative.x, target.forward.y, relative.z);
        }
    }
}