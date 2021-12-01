using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.ObjectRemovalHandling
{
	public class SimpleDestroy : MonoBehaviour, IRemovalProcessor
	{
		public void Remove()
		{
			Destroy(gameObject);
		}
	}
}
