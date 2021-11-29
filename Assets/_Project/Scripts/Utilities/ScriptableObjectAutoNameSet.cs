using System.IO;
using UnityEditor;
using UnityEngine;

namespace WizardGame.Utility
{
	/// <summary>
	/// Use this asset to have the name set automatically as the file is renamed, this includes the first name set
	/// (By name set, it means the serialized string "name")
	/// </summary>
	public abstract class ScriptableObjectAutoNameSet : ScriptableObject
	{
		[SerializeField] protected new string name = "New Asset";
		public string Name => name;

		private void SetNameAsAssetFileName()
		{
			var instanceId = GetInstanceID();
			var assetPath = AssetDatabase.GetAssetPath(instanceId);

			name = Path.GetFileNameWithoutExtension(assetPath);
		}
		
		private void OnValidate()
		{
			OnRename();
			OnValidateUtility();
		}
		
		private void OnRename()
		{
			SetNameAsAssetFileName();
		}

		/// <summary>
		/// Can be overridden for setting default values without have to constantly copy code from SO to SO
		/// </summary>
		protected virtual void OnValidateUtility()
		{
			
		}
		
		protected virtual void OnEnable()
		{
			EditorApplication.projectChanged += SetNameAsAssetFileName;
		}

		protected virtual void OnDisable()
		{
			EditorApplication.projectChanged -= SetNameAsAssetFileName;
		}
	}
}
