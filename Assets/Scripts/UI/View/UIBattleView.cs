using Crockhead.Unity;
using Crockhead.Unity.UI;
using Unity.VisualScripting;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 전투 화면.
	/// </summary>
	[AssetPath("Assets/Resources/UI/UIBattleView.prefab", AssetPathType.Resources)]
	public class UIBattleView : UIPanelView
	{
		#region INSPECTOR
		[SerializeField] private RectTransform m_Content;
		//[SerializeField] private UIButtonView m_ResetButton;
		//[SerializeField] private UIButtonView m_MenuButton;
		#endregion

		/// <summary>
		/// 컨텐트 프로퍼티.
		/// </summary>
		public RectTransform Content => m_Content;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			if (m_Content == null)
			{
				m_Content = GetOrAddComponent<RectTransform>("Content");
			}

			BackgroundColor = Color.black;
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void OnInitialize()
		{
			base.OnInitialize();
		}
	}
}