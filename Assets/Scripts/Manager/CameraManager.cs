using Crockhead.Unity;
using UnityEngine;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 카메라 매니저.
	/// </summary>
	public class CameraManager : SharedComponent<CameraManager>
	{
		#region INSPECTOR
		[SerializeField] private Camera m_Camera;
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			if (m_Camera == null)
			{
				var assetLoader = new AssetLoader<GameObject>("Assets/Resources/Base/MainCamera.prefab");
				assetLoader.Load();
				var obj = GameObject.Instantiate(assetLoader.Asset);
				obj.transform.SetParent(transform);
				TransformHelper.ResetTransform(obj.transform);
				m_Camera = obj.GetOrAddComponent<Camera>();
			}
		}
	}
}