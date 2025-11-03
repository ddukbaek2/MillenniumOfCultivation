using Crockhead.Core;
using UnityEngine;
using UnityEngine.EventSystems;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// UI 매니저.
	/// </summary>
	[RequireComponent(typeof(RectTransform))]
	public class UIManager : UIBehaviour
	{
		#region INSEPCTOR
		[SerializeField] private Canvas m_Canvas;
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			SharedInstances.Set<UIManager>(this);
			GameObject.DontDestroyOnLoad(gameObject);

			m_Canvas = GetComponent<Canvas>();
		}
	}
}