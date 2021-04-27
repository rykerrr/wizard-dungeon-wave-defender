using UnityEngine;
using NUnit.Framework;
using WizardGame.Movement;
using WizardGame.Utility.Infrastructure;
using Moq;

namespace WizardGame.Testing.Movement
{
    public class MovementTests
    {
        public class JumpingMovement_Tests
        {
            public class Tick_Method
            {
                [Test]
                public void _0_DeltaForceAndInput_DoesntSet_GroundedCharacter_Value()
                {
                    var jumping = new JumpingMovement();
                    
                    jumping.SetPreviousInput(0);
                    jumping.Tick(0, 0, true);
                    
                    Assert.AreEqual(Vector3.zero, jumping.Value);
                }

                [Test]
                [TestCase(1), TestCase(2), TestCase(3.5f), TestCase(5f)]
                public void _1_DeltaAndInput_Positive_Force_Sets_GroundedCharacter_Value_To_x0yXz0(float force)
                {
                    var jumping = new JumpingMovement();
                    
                    jumping.SetPreviousInput(1);
                    jumping.Tick(1, force, true);
                    
                    Assert.AreEqual(new Vector3(0, force, 0), jumping.Value);
                }

                [Test]
                [TestCase(-3, 4), TestCase(-2, -4), TestCase(-8, 2)]
                public void _Negative_Delta_1_Input_Positive_Force_DoesntSet_GroundedCharacter_Value(float delta,
                    float force)
                {
                    var jumping = new JumpingMovement();
                    var prevValue = jumping.Value;
                    
                    jumping.SetPreviousInput(1);
                    jumping.Tick(delta, force, true);
                    
                    Assert.AreEqual(prevValue, jumping.Value);
                }
                
                [Test]
                [TestCase(1), TestCase(2), TestCase(3.5f), TestCase(5f)]
                public void _1_Delta_Positive_Force_0_Input_DoesntSet_GroundedCharacter_Value(float force)
                {
                    var jumping = new JumpingMovement();
                    var prevVal = jumping.Value;
                    
                    jumping.SetPreviousInput(0);
                    jumping.Tick(1, force, true);
                    
                    Assert.AreEqual(prevVal, jumping.Value);
                }

                [Test]
                [TestCase(1), TestCase(2), TestCase(3.5f), TestCase(5f)]
                public void _0_Delta_Positive_Force_1_Input_DoesntSet_GroundedCharacter_Value(float force)
                {
                    var jumping = new JumpingMovement();
                    var prevVal = jumping.Value;
                    
                    jumping.SetPreviousInput(1);
                    jumping.Tick(0, force, true);
                    
                    Assert.AreEqual(prevVal, jumping.Value);
                }

                [Test]
                public void _1_Delta_0_Force_1_Input_DoesntSet_GroundedCharacter_Value()
                {
                    var jumping = new JumpingMovement();
                    var prevVal = jumping.Value;
                    
                    jumping.SetPreviousInput(1);
                    jumping.Tick(1, 0, true);
                    
                    Assert.AreEqual(prevVal, jumping.Value);
                }
                
                [Test]
                [TestCase(1), TestCase(2), TestCase(3.5f), TestCase(5f)]
                public void _1_DeltaAndInput_Positive_Force_DoesntSet_NotGroundedCharacter_Value(float force)
                {
                    var jumping = new JumpingMovement();
                    var prevVal = jumping.Value;
                    
                    jumping.SetPreviousInput(1);
                    jumping.Tick(1, force, false);
                    
                    Assert.AreEqual(prevVal, jumping.Value);
                }
            }
        }

        public class LocomotionMovement_Tests
        {
            public class Tick_Method
            {
                [Test]
                [TestCase(-4), TestCase(-3f), TestCase(-224)]
                public void Negative_Delta_1_MvSpeedAndInput_DoesntSet_Value(float delta)
                {
                    var locomotion = new LocomotionMovement();
                    var prevVal = locomotion.Value;
                    
                    locomotion.SetPreviousMovementInput(Vector2.one);
                    locomotion.Tick(delta, 1);
                    
                    Assert.AreEqual(prevVal, locomotion.Value);
                }

                [Test]
                public void _0_DeltaMvSpeedAndInput_DoesntSet_Value()
                {
                    var locomotion = new LocomotionMovement();
                    var prevVal = locomotion.Value;
                    
                    locomotion.SetPreviousMovementInput(Vector2.zero);
                    locomotion.Tick(0, 0);
                    
                    Assert.AreEqual(prevVal, locomotion.Value);
                }

                [Test]
                [TestCase(4f), TestCase(2f), TestCase(-4f), TestCase(-224f)]
                public void _1_Delta_X_MvSpeed_1_Input_Sets_To_xXy0zX(float mvSpeed)
                {
                    var locomotion = new LocomotionMovement();
                    var expectedValue = new Vector3(mvSpeed, 0f, mvSpeed);
                    
                    locomotion.SetPreviousMovementInput(Vector2.one);
                    locomotion.Tick(1, mvSpeed);
                    
                    Assert.AreEqual(expectedValue, locomotion.Value);
                }

                [Test]
                [TestCase(4f), TestCase(2f), TestCase(-4f), TestCase(-224f)]
                public void _1_Delta_X_MvSpeed_0_Input_DoesntSet_Value(float mvSpeed)
                {
                    var locomotion = new LocomotionMovement();
                    var prevVal = locomotion.Value;
                    
                    locomotion.SetPreviousMovementInput(Vector2.zero);
                    locomotion.Tick(1, mvSpeed);
                    
                    Assert.AreEqual(prevVal, locomotion.Value);
                }

                [Test]
                [TestCase(4f), TestCase(2f), TestCase(-4f), TestCase(-224f)]
                public void _0_Delta_X_MvSpeed_1_Input_DoesntSet_Value(float mvSpeed)
                {
                    var locomotion = new LocomotionMovement();
                    var prevVal = locomotion.Value;
                    
                    locomotion.SetPreviousMovementInput(Vector2.one);
                    locomotion.Tick(0, mvSpeed);
                    
                    Assert.AreEqual(prevVal, locomotion.Value);
                }

                [Test]
                public void _1_Delta_0_MvSpeed_1_Input_DoesntSet_Value()
                {
                    var locomotion = new LocomotionMovement();
                    var prevVal = locomotion.Value;
                    
                    locomotion.SetPreviousMovementInput(Vector2.one);
                    locomotion.Tick(1, 0);
                    
                    Assert.AreEqual(prevVal, locomotion.Value);
                }

                [Test]
                [TestCase(4f, 2f), TestCase(2f, -3f)
                 , TestCase(-4f, 1f), TestCase(-224f, 64f)
                , TestCase(3f, 0f), TestCase(0f, 4f)]
                public void _1_Delta_X_MvSpeed_Y_Input_Sets_Value_To_xXYy0zXY(float mvSpeed, float inputUnitMagnitude)
                {
                    var locomotion = new LocomotionMovement();
                    var expectedVal = new Vector3(mvSpeed * inputUnitMagnitude, 0f, mvSpeed * inputUnitMagnitude);
                    
                    locomotion.SetPreviousMovementInput(Vector2.one * inputUnitMagnitude);
                    locomotion.Tick(1, mvSpeed);
                    
                    Assert.AreEqual(expectedVal, locomotion.Value);
                }
            }
        }

        public class NullMovement_Tests
        {
            // Todo: Check out whether interaction tests could be used here
            // now whether it'd be worth it is another story, but live and learn
        }

        public class GravityMovement_Tests
        {
            
        }

        public class ForceReceiver_Tests
        {
            
        }
    }
}