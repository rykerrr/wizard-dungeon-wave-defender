using UnityEngine;
using WizardGame.Timers;

namespace WizardGame.Stats_System
{
    public class StatsSystemBehaviour : MonoBehaviour
    {
        [SerializeField] public EntityStats statsToInject = default;
        [SerializeField] private StatsSystem statsSystem = default;

        public StatsSystem StatsSystem => statsSystem ?? (statsSystem = new StatsSystem(statsToInject
            , TimerTickerSingleton.Instance));

        private void Awake()
        {
            StatsSystem.Init(statsToInject);

            DebugTextDump();
        }
        
        [ContextMenu("Debug text dump")]
        public void DebugTextDump()
        {
            Debug.Log(statsSystem.ToString());
        }
    }
}