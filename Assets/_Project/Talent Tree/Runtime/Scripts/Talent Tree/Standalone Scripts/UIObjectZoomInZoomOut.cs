using Talent_Tree.Dynamic_Talent_Tree.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Talent_Tree.Dynamic_Talent_Tree
{
	public class UIObjectZoomInZoomOut : MonoBehaviour
	{
		[SerializeField] private RectTransform objToZoom = default;
		
		[SerializeField] private float maxZoom = 10;
		[SerializeField] private float minZoom = 1;

		private float curZoom = 1;
		
		private RectTransform parent = default;
		
		private void Awake()
		{
			parent = (RectTransform) objToZoom.parent;
		}

		private void Update()
		{
			if (Keyboard.current.qKey.isPressed)
			{
				UpdateZoom(-0.1f);
			}

			if (Keyboard.current.eKey.isPressed)
			{
				UpdateZoom(0.1f);
			}
		}

		public void On_MouseWheel(InputAction.CallbackContext ctx)
		{
			if (ctx.canceled) return;
			
			UpdateZoom(ctx.ReadValue<Vector2>().y / 480f);
		}
		
		private void UpdateZoom(float zoom)
		{
			if (curZoom + zoom > maxZoom || curZoom + zoom < minZoom)
			{
				return;
			}

			curZoom += zoom;
			
			var rawMousePos = Mouse.current.position.ReadValue();

			var newScale = new Vector3(curZoom, curZoom, objToZoom.localScale.z);

			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				parent, rawMousePos, null, out var parentLp);

			objToZoom.ScaleAround(parentLp, newScale);
		}
	}
}
