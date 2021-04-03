using UnityEngine;
using NUnit.Framework;
using WizardGame.Movement;
using WizardGame.Utility.Infrastructure;

namespace WizardGame.Testing
{
    public class MovementTests : MonoBehaviour
    {
        public class MovementModifierTests
        {
            public class MovementInputProcessor_Tests
            {
                public class PreviousRawInputProperty
                {
                    [Test]
                    [TestCase(1, 3), TestCase(2, 9), TestCase(-4, 24), TestCase(3, -2)]
                    public void Property_Changes_Correctly_When_Calling_SetMovementInput(float h, float v)
                    {
                        var input = new Vector2(h, v);
                        var movementProcessor = (MovementInputProcessor) A.MovementInputProcessor();
                        
                        movementProcessor.SetMovementInput(input);

                        Assert.AreEqual(input, movementProcessor.previousRawInput);
                    }
                }

                public class MovementValueProperty
                {
                    [Test]
                    // integration test because i'm depending on an "external" library, Mathf
                    public void Property_Changes_When_Setting_Input_And_Calling_CalculateMovement()
                    {
                        var input = new Vector2(1f, 2f);
                        var movementProcessor = (MovementInputProcessor) A.MovementInputProcessor();
                        var prevValue = movementProcessor.Value;

                        movementProcessor.SetMovementInput(input);
                        movementProcessor.CalculateMovement(1);

                        Assert.AreNotEqual(prevValue, movementProcessor.Value);
                    }
                }
            }

            public class JumpInputProcessor_Tests
            {
                public class JumpValueProperty
                {
                    [Test]
                    public void Property_Changes_When_Setting_Input_And_Calling_CalculateMovement()
                    {
                        
                    }
                }
            }
        }
    }
}