using System;
using UnityEngine;

namespace WizardGame.SelectionWindow
{
    [Serializable]
    public struct WindowElementData
    {
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Color color;
        
        public string Name => name;
        public string Description => description;
        public Sprite Sprite => sprite;
        public Color Color => color;
        
        public WindowElementData(string name, string description, Sprite sprite, Color color)
        {
            this.name = name;
            this.description = description;
            this.sprite = sprite;
            this.color = color;
        }
    }
}