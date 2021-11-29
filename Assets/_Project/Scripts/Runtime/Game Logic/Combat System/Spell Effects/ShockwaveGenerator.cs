using System;
using UnityEngine;
using WizardGame.Combat_System.Element_System;
using Object = UnityEngine.Object;

namespace WizardGame.Combat_System.Spell_Effects
{
	[Serializable]
	public class ShockwaveGenerator
	{
		[SerializeField] private PillarShockwave pillarShockwaveEffect = default;
		[SerializeField] private LayerMask entitiesLayerMask = default;

		private GameObject owner;
		private Element element;

		private Collider[] colliderHits;

		private int swDmg = default;
		private Vector3 swExtents = default;

		public void Init(GameObject owner, Element element, Vector3 swExtents, int swDmg,
			params Collider[] colliderHits)
		{
			this.owner = owner;
			this.element = element;
			this.swExtents = swExtents;
			this.swDmg = swDmg;
			this.colliderHits = colliderHits;
		}

		public void GenerateAndProcessShockwave(Vector3 pos, Vector3 up)
		{
			if (ReferenceEquals(pillarShockwaveEffect, null))
			{
				Debug.LogError("On hit effect not set.");
				Debug.Break();

				return;
			}

			var swClone = Object.Instantiate(pillarShockwaveEffect, pos, Quaternion.identity);

			swClone.transform.up = up;

			swClone.Init(swDmg, swExtents, element, owner, entitiesLayerMask, ref colliderHits);
		}
	}
}
