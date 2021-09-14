using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Talent_Tree.UI
{
    public class DynamicTalentLinkUI : MonoBehaviour
    {
        [SerializeField] private DynamicTalentUI destination = default;
        [SerializeField] private int weightRequiredInBase = 1;
        [SerializeField] private List<DynamicTalentLinkPartUI> parts = default;
        
        public DynamicTalentUI Destination => destination;
        public bool CanTraverse(int points) => points >= weightRequiredInBase;
        
        private float fullLength = 0;

        private readonly Queue<Tweener> tweenQueue = new Queue<Tweener>();
        private Tweener curTweener = default;

        private void Awake()
        {
            DOTween.Init(false, false, LogBehaviour.Verbose);
            DOTween.defaultAutoPlay = AutoPlay.None;
        }
        
        public void Init(DynamicTalentUI destination, TalentLink talentLink, List<DynamicTalentLinkPartUI> parts)
        {
            this.destination = destination;
            this.parts = parts;

            weightRequiredInBase = talentLink.WeightRequiredInBase;

            InitLinkUIParts();
        }
        
        private void InitLinkUIParts()
        {
            foreach (var part in parts)
            {
                fullLength += part.PartLength;

                part.SetFillInstant(0);
            }
        }
        
        public void UpdateFill(int baseLevel)
        {
            var fill = Mathf.Clamp01((float)baseLevel / weightRequiredInBase);
            var lenWithFill = fill * fullLength;
            
            foreach (var part in parts.Where(part => !part.IsFilled))
            {
                if (lenWithFill >= part.PartLength)
                {
                    lenWithFill -= part.PartLength;
                    
                    var tweener = part.SetFill(1);
                    
                    tweener.onComplete += () =>
                    {
                        if (tweenQueue.Count > 0)
                        {
                            curTweener = tweenQueue.Dequeue();
                            curTweener.Play();
                        }
                        else curTweener = null;
                    };
                    
                    tweenQueue.Enqueue(tweener);
                }
                else
                {
                    var fillLength = lenWithFill / part.PartLength;

                    var tweener = part.SetFill(fillLength);
                    
                    tweener.onComplete += () =>
                    {
                        if (tweenQueue.Count > 0)
                        {
                            curTweener = tweenQueue.Dequeue();
                            curTweener.Play();
                        }
                        else curTweener = null;
                    };
                    
                    tweenQueue.Enqueue(tweener);
                    
                    break;
                }
            }
            
            if (tweenQueue.Count > 0 && curTweener == null)
            {
                curTweener = tweenQueue.Dequeue(); 
                curTweener.Play();
            }
        }
    }
}