using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Talent_Tree
{
	public class ResizeContentBackgroundByContent : MonoBehaviour
	{
		[Header("Preferences")]
		[SerializeField] private RectTransform content = default;
		[SerializeField] private RectTransform contentBackground;

		[SerializeField] private float multiplier;
		
	    public void Resize()
	    {
		    var rect = content.rect;
		    var hw = Math.Max(rect.width, rect.height);
		    
		    contentBackground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hw * multiplier);
		    contentBackground.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, hw * multiplier);
	    }
	}
}
