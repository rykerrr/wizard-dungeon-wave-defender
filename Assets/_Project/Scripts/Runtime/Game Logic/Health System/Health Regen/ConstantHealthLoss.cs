using UnityEngine;
using WizardGame.Utility.Timers;

// Technically could count as negative health regeneration
namespace WizardGame.Health_System.HealthRegeneration
{
	[RequireComponent(typeof(IDamageable))]
	public class ConstantHealthLoss : MonoBehaviour
	{
		[SerializeField] private int damagePerTick;
		[SerializeField] private float delayBetweenDamage;

		private ITimer timer;
		private IDamageable damageable;

		private void Awake()
		{
			damageable = GetComponent<IDamageable>();
			
			InitTimer();
		}

		private void InitTimer()
		{
			timer = new DownTimer(delayBetweenDamage);

			((DownTimer) timer).OnTimerEnd += () =>
			{
				// TODO: change to use Element.Empty once implemented
				damageable.TakeDamage(damagePerTick, null, gameObject);
				timer.Reset();
			};

			timer.EnableTimer();
		}

		private void Update()
		{
			timer.TryTick(Time.deltaTime);
		}

		private void OnEnable()
		{
			timer.Reset();
			timer.EnableTimer();
		}

		private void OnDisable()
		{
			timer.DisableTimer();
		}
	}
}
