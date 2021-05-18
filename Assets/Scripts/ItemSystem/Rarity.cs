﻿using UnityEngine;

namespace WizardGame.Item_System
{
    [CreateAssetMenu(fileName = "New Rarity", menuName = "Rarity/New Rarity")]
    public class Rarity : ScriptableObject
    {
        [SerializeField] private new string name = default;
        [SerializeField] private Color rarityColor = default;
        
        public string Name => name;
        public Color RarityColor => rarityColor;
    }
}