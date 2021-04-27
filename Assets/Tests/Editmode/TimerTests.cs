using UnityEngine;
using NUnit.Framework;
using WizardGame.Timers;
using WizardGame.Utility.Infrastructure;
using Moq;

namespace WizardGame.Testing.Timers
{
    public class TimerTests
    {
        public class BaseTimer_Mocked_Tests
        {
            // Both could be technically stubs
            // Mocks are used for when you want to test the behavior on the mocks themselves
            // Stubs are used to satisfy conditions
            // Stubs and mocks are handled as fakes in Moq but named Mock for readability purposes
            // o well
            [Test]
            public void Enabling_Timer_Makes_Tick_Return_True()
            {
                var baseTimerMock = new Mock<BaseTimer>();
                var baseTimer = baseTimerMock.Object;
                
                baseTimer.EnableTimer();
                var didTickProcess = baseTimer.TryTick(3);
                
                Assert.False(didTickProcess);
            }

            [Test]
            public void Disabling_Timer_Makes_Tick_Return_False()
            {
                var baseTimerMock = new Mock<BaseTimer>();
                var baseTimer = baseTimerMock.Object;
                
                baseTimer.DisableTimer();
                var didTickProcess = baseTimer.TryTick(3);
                
                Assert.False(didTickProcess);
            }
        }
        
        public class DownTimer_Tests
        {
            public class SetTimerMethod
            {
                [Test]
                [TestCase(4), TestCase(3), TestCase(5)]
                public void Setting_Existing_Timer_With_Positive_Time_Sets_Time_To_Correct_Value(float time)
                {
                    DownTimer timer = A.DownTimer();

                    timer.SetTimer(time);

                    Assert.AreEqual(time, timer.Time);
                }

                [Test]
                [TestCase(-3), TestCase(-6), TestCase(-9)]
                public void Setting_Existing_Timer_To_Negative_Time_Sets_Time_To_0(float time)
                {
                    DownTimer timer = A.DownTimer();

                    timer.SetTimer(time);

                    Assert.AreEqual(0, timer.Time);
                }

                [Test]
                public void Setting_Existing_Timer_To_0_Sets_Time_To_0()
                {
                    DownTimer timer = A.DownTimer();

                    timer.SetTimer(0);

                    Assert.AreEqual(0, timer.Time);
                }
            }

            public class TryTickMethod
            {
                [Test]
                public void Ticking_3_From_Timer_With_Time_5_Sets_RemainingTime_To_2()
                {
                    DownTimer timer = A.DownTimer().WithTime(5);

                    timer.TryTick(3);

                    Assert.AreEqual(2, (int) timer.Time);
                }

                [Test]
                public void Ticking_10_From_Timer_With_Time_3_Sets_RemainingTime_To_0()
                {
                    DownTimer timer = A.DownTimer().WithTime(5);

                    timer.TryTick(10);

                    Assert.AreEqual(0, timer.Time);
                }

                [Test]
                [TestCase(4), TestCase(9), TestCase(24)]
                public void Ticking_Timer_With_Time_0_Returns_False(float time)
                {
                    DownTimer timer = A.DownTimer();

                    var didTickProcess = timer.TryTick(4);
                    
                    Assert.False(didTickProcess);
                }

                [Test]
                [TestCase(-4), TestCase(-9), TestCase(-24)]
                public void Ticking_Negative_Number_From_Timer_Returns_False(float time)
                {
                    DownTimer timer = A.DownTimer();

                    var didTickProcess = timer.TryTick(time);
                    
                    Assert.False(didTickProcess);
                }

                [Test]
                [TestCase(-4), TestCase(-9), TestCase(-24)]
                public void Ticking_Negative_Number_From_Timer_Does_Nothing(float time)
                {
                    DownTimer timer = A.DownTimer();
                    float prevVal = timer.Time;

                    timer.TryTick(time);
                    
                    Assert.AreEqual(prevVal, timer.Time);
                }
            }
        }

        public class NullTimer_Tests
        {
            public class TryTickMethod
            {
                [Test]
                public void Timer_Always_Processes_Tick()
                {
                    NullTimer nullTimer = A.NullTimer();

                    var didTickProcess = nullTimer.TryTick(4);

                    Assert.True(didTickProcess);
                }

                [Test]
                [TestCase(4), TestCase(-9), TestCase(24), TestCase(0)]
                public void Ticking_Timer_Doesnt_Change_Time(float time)
                {
                    NullTimer nullTimer = A.NullTimer();
                    float prevValue = nullTimer.Time;
                    
                    nullTimer.TryTick(time);
                    
                    Assert.AreEqual(prevValue, nullTimer.Time);
                }
            }
        }

        public class Stopwatch_Tests
        {
            public class TryTick_Method
            {
                [Test]
                [TestCase(4), TestCase(5), TestCase(304)]
                public void Ticking_With_Positive_Number_Returns_True(float time)
                {
                    Stopwatch stopwatch = A.Stopwatch();

                    var didTickProcess = stopwatch.TryTick(time);
                    
                    Assert.True(didTickProcess);
                }
                
                [Test]
                [TestCase(-4), TestCase(-5), TestCase(-304)]
                public void Ticking_With_Negative_Number_Returns_False(float time)
                {
                    Stopwatch stopwatch = A.Stopwatch();

                    var didTickProcess = stopwatch.TryTick(time);

                    Assert.False(didTickProcess);
                }
                
                [Test]
                [TestCase(4), TestCase(5), TestCase(24), TestCase(348)]
                public void Ticking_With_Positive_Number_Increases_Time(float time)
                {
                    Stopwatch stopwatch = A.Stopwatch();
                    var prevTime = stopwatch.Time;
                    
                    stopwatch.TryTick(time);
                    
                    Assert.True(stopwatch.Time > prevTime);
                }

                [Test]
                public void Ticking_With_Zero_Does_Nothing()
                {
                    Stopwatch stopwatch = A.Stopwatch().WithTime(4);
                    var prevTime = stopwatch.Time;

                    stopwatch.TryTick(0);
                    
                    Assert.AreEqual(0, prevTime);
                }
            }
        }
    }
}