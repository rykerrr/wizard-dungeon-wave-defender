using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Movement.Rotation;

public class ForwardIndicator : MonoBehaviour
{
	[Header("Preferences")] 
	[SerializeField] private KeepObjectForwardSameAsTarget keepForward = default;
	[SerializeField] private GameObject graphics = default;
	
	private Transform thisTransf;

	private void Awake()
	{
		thisTransf = transform;
	}

	// private void Update()
	// {
	// 	thisTransf.forward = keepForward.Tick();
	// }

	public void PressRelease(InputAction.CallbackContext ctx)
	{
		switch (ctx.phase)
		{
			case InputActionPhase.Performed:
				if(!graphics.activeSelf) graphics.SetActive(true);
				break;
			case InputActionPhase.Canceled:
				if(graphics.activeSelf) graphics.SetActive(false);
				break;
		}
	}
	
	private void InputSystem_EnableIndicator()
	{
		Debug.Log("press");
	}

	private void InputSystem_DisableIndicator()
	{
		Debug.Log("release");
	}
}
