using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Talent_Tree.UI
{
    public class DynamicTalentLinkPartUI : MonoBehaviour
    {
        [Header("Optional properties")]
        [SerializeField] private float tweenDur = 1f;
        [SerializeField] private Ease easeType = Ease.Linear;
        
        [Header("Required properties and references")]
        [SerializeField] private Transform barFillImage = default;
        [SerializeField] private bool isY = false;
        
        [SerializeField] private float partLength = 0f;
        
        public Transform BarFillImage => barFillImage;

        public bool IsY
        {
            get => isY;
            set => isY = value;
        }
        
        public float PartLength => partLength;

        public bool IsFilled =>
            isY ? barFillImage.localScale.y + Mathf.Epsilon > 1f : barFillImage.localScale.x + Mathf.Epsilon > 1f;

        private void Awake()
        {
            DOTween.Init(false, false, LogBehaviour.Verbose);
            DOTween.defaultAutoPlay = AutoPlay.None;
            
            if (isY) partLength = ((RectTransform) barFillImage).rect.height;
            else partLength = ((RectTransform) barFillImage).rect.width;
        }

        public Tweener SetFill(float fill)
        {
            Tweener tweener = null;

            if (isY) tweener = barFillImage.transform.DOScaleY(fill, tweenDur);
            else tweener = barFillImage.transform.DOScaleX(fill, tweenDur).SetSpeedBased();

            tweener.SetSpeedBased().SetEase(easeType);
            
            return tweener;
        }

        public void SetFillInstant(float fill)
        {
            var fillImgScale = barFillImage.localScale;

            if (isY) fillImgScale = new Vector3(fillImgScale.x, fill, fillImgScale.z);
            else fillImgScale = new Vector3(fill, fillImgScale.y, fillImgScale.z);

            barFillImage.localScale = fillImgScale;
        }
    }
}
