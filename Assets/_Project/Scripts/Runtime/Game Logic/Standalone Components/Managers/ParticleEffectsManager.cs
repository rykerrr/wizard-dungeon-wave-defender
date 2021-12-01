using UnityEngine;

namespace WizardGame.Managers
{
    public class ParticleEffectsManager : MonoBehaviour, IManagerObject
    {
        [SerializeField] private ParticleSystem smokeSystem;

        public ParticleSystem SmokeSystem => smokeSystem;
    }
}