using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace WizardGame.MainMenu
{
    public class SceneLoadController : MonoBehaviour
    {
        [SerializeField] private int sceneToLoadIndex = 1;
        [SerializeField] private Transform loadingBarFill = default;

        [SerializeField] private GameObject pressAnyKeyText = default;
        
        private AsyncOperation sceneLoadOperation = null;

        private string thisDataSaveFileName = "";
        
        public string ThisDataSaveFileName
        {
            get => thisDataSaveFileName;
            set => thisDataSaveFileName = value;
        }
        
        private void Update()
        {
            if (sceneLoadOperation == null || sceneLoadOperation.isDone)
            {
                Debug.Log("Operatin null or done");
                
                return;
            }

            var loadBarScale = loadingBarFill.localScale;
            var loadProgress = sceneLoadOperation.progress;
            
            loadingBarFill.localScale = new Vector3(loadProgress + 0.1f
                , loadBarScale.y, loadBarScale.z);

            if (loadProgress + Mathf.Epsilon >= 0.9f)
            {
                EnablePressAnyKey();
            }
        }

        private void LoadScene()
        {
            sceneLoadOperation.allowSceneActivation = true;
        }
        
        private void EnablePressAnyKey()
        {
            pressAnyKeyText.SetActive(true);
            
            // enabled = false;
        }

        public void OnAnyKey_LoadScene(InputAction.CallbackContext ctx)
        {
            if (ctx.phase != InputActionPhase.Performed) return;
            
            Debug.Log("Scene Load");
            LoadScene();
        }

        public void StartAsyncSceneLoad()
        {
            sceneLoadOperation = SceneManager.LoadSceneAsync(sceneToLoadIndex);
            sceneLoadOperation.allowSceneActivation = false;

            gameObject.SetActive(true);
        }
    }
}