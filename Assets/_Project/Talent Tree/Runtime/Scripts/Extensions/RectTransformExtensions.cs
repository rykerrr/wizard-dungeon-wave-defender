using UnityEngine;

namespace Talent_Tree.Dynamic_Talent_Tree.Extensions
{
    public static class RectTransformExtensions
    {
        public static void ScaleAround(this RectTransform target, Vector3 pivot, Vector3 newScale)
        {
            var a = target.localPosition;
            var b = pivot;

            var c = a - b; // diff from object pivot to desired pivot/origin

            var rs = newScale.x / target.localScale.x; // relataive scale factor

            // calc final position post-scale
            var fp = b + c * rs;

            // finally, actually perform the scale/translation
            target.localScale = newScale;
            target.localPosition = fp;
        }
    }
}