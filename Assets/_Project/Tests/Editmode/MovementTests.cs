using UnityEngine;
using NUnit.Framework;
using WizardGame.Movement.Position;
using WizardGame.Infrastructure;

namespace WizardGame.Testing.Movement
{
    public class MovementTests
    {
        public class JumpingMovement_Tests
        {
            // Arrange Act Assert
            public class Tick_Method
            {
                [Test]
                public void _0_DeltaForceAndInput_DoesntSet_GroundedCharacter_Value()
                {
                    JumpingMovement jumping = A.JumpingMovement();
                    
                    jumping.Tick(1, true);
                    
                    Assert.AreEqual(Vector3.zero, jumping.Value);
                }

                [Test]
                [TestCase(1), TestCase(2), TestCase(3.5f), TestCase(5f)]
                public void _1_DeltaAndInput_Positive_Force_Sets_GroundedCharacter_Value_To_x0yXz0(float force)
                {
                    JumpingMovement jumping = A.JumpingMovement().WithForce(force).WithInput(1);
                    
                    jumping.Tick(1, true);
                    
                    Assert.AreEqual(new Vector3(0, force, 0), jumping.Value);
                }

                [Test]
                [TestCase(-3, 4), TestCase(-2, -4), TestCase(-8, 2)]
                public void _Negative_Delta_1_Input_Positive_Force_DoesntSet_GroundedCharacter_Value(float delta,
                    float force)
                {
                    JumpingMovement jumping = A.JumpingMovement().WithForce(force).WithInput(1);
                    var prevValue = jumping.Value;
                    
                    jumping.Tick(delta, true);
                    
                    Assert.AreEqual(prevValue, jumping.Value);
                }
                
                [Test]
                [TestCase(1), TestCase(2), TestCase(3.5f), TestCase(5f)]
                public void _1_Delta_Positive_Force_0_Input_DoesntSet_GroundedCharacter_Value(float force)
                {
                    JumpingMovement jumping = A.JumpingMovement().WithForce(force);
                    var prevVal = jumping.Value;
                    
                    jumping.Tick(1, true);
                    
                    Assert.AreEqual(prevVal, jumping.Value);
                }

                [Test]
                [TestCase(1), TestCase(2), TestCase(3.5f), TestCase(5f)]
                public void _0_Delta_Positive_Force_1_Input_DoesntSet_GroundedCharacter_Value(float force)
                {
                    JumpingMovement jumping = A.JumpingMovement().WithForce(force).WithInput(1);
                    var prevVal = jumping.Value;
                    
                    jumping.Tick(0, true);
                    
                    Assert.AreEqual(prevVal, jumping.Value);
                }

                [Test]
                public void _1_Delta_0_Force_1_Input_DoesntSet_GroundedCharacter_Value()
                {
                    JumpingMovement jumping = A.JumpingMovement().WithInput(1);
                    var prevVal = jumping.Value;
                    
                    jumping.Tick(1, true);
                    
                    Assert.AreEqual(prevVal, jumping.Value);
                }
                
                [Test]
                [TestCase(1), TestCase(2), TestCase(3.5f), TestCase(5f)]
                public void _1_DeltaAndInput_Positive_Force_DoesntSet_NotGroundedCharacter_Value(float force)
                {
                    JumpingMovement jumping = A.JumpingMovement().WithForce(force).WithInput(1);
                    var prevVal = jumping.Value;
                    
                    jumping.Tick(1, false);
                    
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
                    LocomotionMovement locomotion = A.LocomotionMovement().WithInput(Vector2.one).WithSpeed(1);
                    var prevVal = locomotion.Value;
                    
                    locomotion.Tick(delta);
                    
                    Assert.AreEqual(prevVal, locomotion.Value);
                }

                [Test]
                public void _0_DeltaMvSpeedAndInput_DoesntSet_Value()
                {
                    LocomotionMovement locomotion = A.LocomotionMovement();
                    var prevVal = locomotion.Value;
                    
                    locomotion.Tick(0);
                    
                    Assert.AreEqual(prevVal, locomotion.Value);
                }
                
                [Test]
                [TestCase(4f), TestCase(2f), TestCase(-4f), TestCase(-224f)]
                public void _1_Delta_X_MvSpeed_1_Input_Sets_To_xXy0zX(float mvSpeed)
                {
                    LocomotionMovement locomotion = A.LocomotionMovement().WithInput(Vector2.one).WithSpeed(mvSpeed);
                    var expectedValue = new Vector3(mvSpeed, 0f, mvSpeed);
                    
                    locomotion.Tick(1);
                    
                    Assert.AreEqual(expectedValue, locomotion.Value);
                }

                [Test]
                [TestCase(4f), TestCase(2f), TestCase(-4f), TestCase(-224f)]
                public void _1_Delta_X_MvSpeed_0_Input_DoesntSet_Value(float mvSpeed)
                {
                    LocomotionMovement locomotion = A.LocomotionMovement().WithSpeed(mvSpeed);
                    var prevVal = locomotion.Value;
                    
                    locomotion.Tick(1);
                    
                    Assert.AreEqual(prevVal, locomotion.Value);
                }

                [Test]
                [TestCase(4f), TestCase(2f), TestCase(-4f), TestCase(-224f)]
                public void _0_Delta_X_MvSpeed_1_Input_DoesntSet_Value(float mvSpeed)
                {
                    LocomotionMovement locomotion = A.LocomotionMovement().WithInput(Vector2.one);
                    var prevVal = locomotion.Value;
                    
                    locomotion.Tick(0);
                    
                    Assert.AreEqual(prevVal, locomotion.Value);
                }

                [Test]
                public void _1_Delta_0_MvSpeed_1_Input_DoesntSet_Value()
                {
                    LocomotionMovement locomotion = A.LocomotionMovement().WithInput(Vector2.one);
                    var prevVal = locomotion.Value;
                    
                    locomotion.Tick(1);
                    
                    Assert.AreEqual(prevVal, locomotion.Value);
                }

                [Test]
                [TestCase(4f, 2f), TestCase(2f, -3f)
                 , TestCase(-4f, 1f), TestCase(-224f, 64f)
                , TestCase(3f, 0f), TestCase(0f, 4f)]
                public void _1_Delta_X_MvSpeed_Y_Input_Sets_Value_To_xXYy0zXY(float mvSpeed, float inputUnitMagnitude)
                {
                    LocomotionMovement locomotion = A.LocomotionMovement().WithInput(Vector2.one * inputUnitMagnitude).WithSpeed(mvSpeed);
                    var expectedVal = new Vector3(mvSpeed * inputUnitMagnitude, 0f, mvSpeed * inputUnitMagnitude);
                    
                    locomotion.Tick(1);
                    
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
            public class Tick_Method
            {
                [Test]
                [TestCase(2, 4), TestCase(3, 5)
                ,TestCase(-4, -6), TestCase(244, 3)]
                public void _1_Delta_Value_Set_To_GroundedPull_When_Grounded(float gravMagnitude, float groundedPullMagnitude)
                {
                    GravityMovement gravity = A.GravityMovement().WithGrav(gravMagnitude)
                        .WithGroundPullMag(groundedPullMagnitude);
                    var expectedValue = new Vector3(0, -groundedPullMagnitude, 0);
                    
                    gravity.Tick(1, true);
                    
                    Assert.AreEqual(expectedValue, gravity.Value);
                }

                [Test]
                public void _1_Delta_2_Ticks_Of_5_Lower_Value_By_10_When_NotGrounded()
                {
                    GravityMovement gravity = A.GravityMovement().WithGrav(5).WithGroundPullMag(10);
                    var expectedValue = new Vector3(0, -10, 0);

                    gravity.Tick(1, false);
                    gravity.Tick(1, false);
                    
                    Assert.AreEqual(expectedValue, gravity.Value);
                }
                
                [Test]
                [TestCase(2), TestCase(4)]
                public void _1_Delta_Value_Set_To_GroundedPull_After_X_Ticks(float tickAmn)
                {
                    GravityMovement gravity =  A.GravityMovement().WithGrav(4).WithGroundPullMag(5);
                    var expectedValue = new Vector3(0, -5, 0);
                    
                    for (int i = 0; i < tickAmn; i++)
                    {
                        gravity.Tick(1, false);
                    }
                    gravity.Tick(1, true);
                    
                    Assert.AreEqual(expectedValue, gravity.Value);
                }

                [Test]
                [TestCase(true), TestCase(false)]
                public void Value_Set_To_0_If_Delta_0_Regardless_Of_GroundedState(bool isGrounded)
                {
                    GravityMovement gravity = A.GravityMovement().WithGrav(3).WithGroundPullMag(10);
                    
                    gravity.Tick(0, isGrounded);
                    
                    Assert.AreEqual(Vector3.zero, gravity.Value);
                }
            }
        }

        public class ForceReceiver_Tests
        {
            public class AddForce_Method
            {
                [Test]
                [TestCase(4), TestCase(244), TestCase(-43), TestCase(0)]
                public void Value_Changed_To_X_When_X_Force_Added(float forceUnitMagnitude)
                {
                    ForceReceiverMovement forceReceiver = A.ForceReceiver();
                    var expectedValue = Vector3.one * forceUnitMagnitude;
                    
                    forceReceiver.AddForce(expectedValue);
                    
                    Assert.AreEqual(expectedValue, forceReceiver.Value);
                }
            }
            
            public class Tick_Method
            {
                [Test]
                [TestCase(0.1f), TestCase(0.05f), TestCase(0.2f)]
                public void _1_DeltaAndDrag_Sets_Value_To_0_If_AddedForce_BelowOrEqual_To_Minimum(float forceUnitMagnitude)
                {
                    ForceReceiverMovement forceReceiver = A.ForceReceiver().WithDrag(1);
                    
                    forceReceiver.AddForce(Vector3.one * forceUnitMagnitude);
                    forceReceiver.Tick(1);
                    
                    Assert.AreEqual(Vector3.zero, forceReceiver.Value);
                }

                [Test]
                [TestCase(-5f), TestCase(42f), TestCase(-400f), TestCase(320f)]
                public void _1_DeltaAndDrag_Set_Value_To_0_For_Force_Higher_Than_Minimum(float forceUnitMagnitude)
                {
                    ForceReceiverMovement forceReceiver = A.ForceReceiver().WithDrag(1);
                    
                    forceReceiver.AddForce(Vector3.one * forceUnitMagnitude);
                    forceReceiver.Tick(1);
                    
                    Assert.AreEqual(Vector3.zero, forceReceiver.Value);
                }

                [Test]
                [TestCase(-5f), TestCase(42f), TestCase(-400f), TestCase(320f)]
                public void _0_DeltaAndDrag_Doesnt_Set_Value_For_Force_Higher_Than_Minimum(float forceUnitMagnitude)
                {
                    ForceReceiverMovement forceReceiver = A.ForceReceiver();
                    var expectedValue = Vector3.one * forceUnitMagnitude;
                    
                    forceReceiver.AddForce(expectedValue);
                    forceReceiver.Tick(0);
                    
                    Assert.AreEqual(expectedValue, forceReceiver.Value);
                }

                [Test]
                [TestCase(300), TestCase(20), TestCase(5), TestCase(40), TestCase(-30)]
                public void _1_Delta_Half1_Drag_Sets_Value_To_Half(float forceUnitMagnitude)
                {
                    ForceReceiverMovement forceReceiver = A.ForceReceiver().WithDrag(0.5f);
                    var expectedValue = Vector3.one * forceUnitMagnitude / 2;
                    
                    forceReceiver.AddForce(Vector3.one * forceUnitMagnitude);
                    forceReceiver.Tick(1);
                    
                    Assert.AreEqual(expectedValue, forceReceiver.Value);
                }

                [Test]
                [TestCase(300), TestCase(20), TestCase(5), TestCase(40), TestCase(-30)]
                public void Half1_Delta_1_Drag_Sets_Value_To_Half(float forceUnitMagnitude)
                {
                    ForceReceiverMovement forceReceiver = A.ForceReceiver().WithDrag(1);
                    var expectedValue = Vector3.one * forceUnitMagnitude / 2;
                    
                    forceReceiver.AddForce(Vector3.one * forceUnitMagnitude);
                    forceReceiver.Tick(0.5f);
                    
                    Assert.AreEqual(expectedValue, forceReceiver.Value);
                }
            }
        }
    }
}