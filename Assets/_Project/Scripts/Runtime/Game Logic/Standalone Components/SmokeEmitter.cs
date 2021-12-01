using UnityEngine;
using WizardGame.Managers;
using Random = System.Random;

namespace WizardGame
{
	public class SmokeEmitter : MonoBehaviour
	{
		[SerializeField] private int maxSmokeParticles = 30;
		[SerializeField] private int minSmokeParticles = 15;

		private Random rand;
		private Random Rand => rand ??= new Random((int) Time.realtimeSinceStartup);

		public void Emit(Vector3 worldPosition)
		{
			var smoke = ((ParticleEffectsManager) ManagerManager.Instance[typeof(ParticleEffectsManager)])
				.SmokeSystem;

			var smokeShape = smoke.shape;
			smokeShape.position = worldPosition;

			var particleCount = Rand.Next(minSmokeParticles, maxSmokeParticles);

			smoke.Emit(particleCount);
		}
	}
}
