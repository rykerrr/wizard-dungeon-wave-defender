using UnityEngine;
using UnityEngine.SceneManagement;

namespace WizardGame.Utility.Patterns
{
    public class InheritableSingleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        private static T instance = default;

        private static object lockObj = new object();
        private static bool shuttingDown = false;

        public static T Instance
        {
            get
            {
                if (shuttingDown)
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"Shutting down is true...{instance}");
#endif
                    
                    return null;
                }

                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType<T>();

                        if (instance == null)
                        {
                            instance = new GameObject(typeof(T).ToString()).AddComponent<T>();

#if UNITY_EDITOR
                            if (instance == null)
                            {
                                Debug.LogError("Instance is still null");
                            }
#endif
                        }

                        return instance;
                    }

                    return instance;
                }
            }
        }

        protected virtual void Awake()
        {
            SceneManager.sceneUnloaded += (s) => instance = null;
        }

        private void OnEnable()
        {
            shuttingDown = false;
        }

        private void OnDisable()
        {
            shuttingDown = true;
        }

        private void OnDestroy()
        {
            shuttingDown = true;
        }
    }
}