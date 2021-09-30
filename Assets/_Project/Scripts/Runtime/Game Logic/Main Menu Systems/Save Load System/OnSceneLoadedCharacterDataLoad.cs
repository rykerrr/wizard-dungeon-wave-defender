using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Combat_System;

namespace WizardGame.MainMenu
{
    public class OnSceneLoadedCharacterDataLoad : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Character thisScenePlayer = default;

        [Header("Preferences")] [SerializeField] [Tooltip("This is used if this scene was started directly (no data was loaded)")]
        private CharacterData surrogateCharData = default;
        
        public static CharacterData LoadedCharacterData { get; set; } = null;
        
        private void Awake()
        {
            SetData(LoadedCharacterData ?? surrogateCharData);

            var loadedDataNotNull = !ReferenceEquals(LoadedCharacterData, null);
            Debug.Log("Data loaded: " + loadedDataNotNull);
        }

        private void SetData(CharacterData dataToSet)
        {
            thisScenePlayer.InitCharacterData(dataToSet);
        }
    }
}