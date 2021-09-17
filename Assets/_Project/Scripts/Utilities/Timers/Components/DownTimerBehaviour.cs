using System;
using UnityEngine;

namespace WizardGame.Utility.Timers
{
	public class DownTimerBehaviour : MonoBehaviour
	{
		private DownTimer timer = default;

		public float Time => timer.Time;

		public Action OnTimerEnd
		{
			get => timer.OnTimerEnd;
			set => timer.OnTimerEnd = value;
		}

		private void Update()
		{
			timer.TryTick(UnityEngine.Time.deltaTime);
		}

		public void CreateTimer(float time)
		{
			timer = new DownTimer(time);
		}

		public void SetNewTime(float time)
		{
			timer.SetNewDefaultTime(time);
			
			ResetTimer();
		}
		
		public void ResetTimer()
		{
			timer.Reset();
			
			EnableTimer();
		}
		
		public void EnableTimer()
		{
			timer.EnableTimer();
		}

		public void DisableTimer()
		{
			timer.DisableTimer();
		}

		private void OnDisable()
		{
			DisableTimer();
		}
	}
}
