using System;
using UnityEngine;
using WizardGame.Utility.Timers;

namespace WizardGame.ObjectRemovalHandling
{
	public class RemoveAfterXSeconds : MonoBehaviour, ITimedRemovalProcessor
	{
		private IRemovalProcessor removalProcessor;
		private ITimer timer;

		private void Awake()
		{
			removalProcessor = GetComponent<IRemovalProcessor>();

			timer = new DownTimer(0f);
			timer.DisableTimer();
			((DownTimer) timer).OnTimerEnd += removalProcessor.Remove;
		}
		
		private void Update()
		{
			var ticked = timer.TryTick(Time.deltaTime);
		}
		
		public void Remove(float seconds)
		{
			((DownTimer) timer).SetNewTime(seconds);
			timer.EnableTimer();
		}
	}
}
