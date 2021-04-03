using UnityEngine;

namespace WizardGame.Movement
{
    [System.Serializable]
    public class MovementInputProcessor : IMovementModifier
    {
        [Header("Movement Input Processor Settings")] 
        [SerializeField] private float smoothingMultiplier;
        [SerializeField] private float speedMultiplier;

        public Vector3 Value { get; private set; } = Vector3.zero;
        public Vector2 previousRawInput { get; private set; } = Vector2.zero;

        private float curSpeed;

        public MovementInputProcessor(float speedMultiplier, float smoothingMultiplier)
        {
            this.speedMultiplier = speedMultiplier;
            this.smoothingMultiplier = smoothingMultiplier;
        }

        public void SetMovementInput(Vector2 input)
        {
            previousRawInput = input;
        }

        public void CalculateMovement(float deltaTime)
        {
            var targetSpeed = previousRawInput.magnitude * speedMultiplier;
            var smoothing = smoothingMultiplier * deltaTime;

            curSpeed = Mathf.MoveTowards(curSpeed, targetSpeed, smoothing);

            Value = previousRawInput.normalized * curSpeed;
        }
    }
}