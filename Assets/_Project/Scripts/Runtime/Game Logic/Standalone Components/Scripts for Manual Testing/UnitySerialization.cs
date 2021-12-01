#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Serializable_Data_Types;
using WizardGame.MainMenu;

namespace WizardGame.ManualTestStuff
{
    public class UnitySerialization : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<GameObject, GameObject> objDict;
        [SerializeField] private SerializableDictionary<int, int> intDict;
        [SerializeField] private SerializableDictionary<ICollection, int> testDict1;
        [SerializeField] private SerializableDictionary<CharacterData, int> charDict2;
        [SerializeField] private SerializableDictionary<ICollection, ICollection> collectDict;
    }
}

#endif