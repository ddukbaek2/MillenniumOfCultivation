using Crockhead.Unity;
using Crockhead.Unity.UI;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 대화 뷰.
	/// </summary>
	[AssetPath("Assets/Resources/UI/UIDialogView.prefab", AssetPathType.Resources)]
	public class UIDialogView : UIPanelView
	{
		#region INSPECTOR
		[SerializeField] private UILabelView m_NameLabel;
		[SerializeField] private UILabelView m_ContentLabel;
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			BackgroundColor = new Color32(255, 178, 0, 255);

			if (m_NameLabel == null)
			{
				m_NameLabel = GetOrAddComponent<UILabelView>("Name/Label");
			}

			if (m_ContentLabel == null)
			{
				m_ContentLabel = GetOrAddComponent<UILabelView>("Content/Label");
			}
		}
	}
}