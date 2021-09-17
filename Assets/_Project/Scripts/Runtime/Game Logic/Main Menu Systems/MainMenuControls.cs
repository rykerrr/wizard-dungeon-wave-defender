using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class MainMenuControls : MonoBehaviour
	{
		public void OnClick_ToggleGameObject(GameObject go)
		{
			go.SetActive(!go.activeSelf);
		}
	}
