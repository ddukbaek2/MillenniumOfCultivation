using UnityEngine;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 터미널 뷰.
	/// </summary>
	[AssetPath("Assets/Resources/UI/UITerminalView.prefab", AssetPathType.Resources)]
	public class UITerminalView : UIPanelView
	{
		#region INSPECTOR
		[SerializeField] private UIScrollView m_ScrollView;
		[SerializeField] private UILabelView m_Label;
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			if (m_ScrollView == null)
			{
				m_ScrollView = GetOrAddComponent<UIScrollView>("ScrollView");
			}

			if (m_Label == null)
			{
				m_Label = GetOrAddComponent<UILabelView>("ScrollView/Content/LabelView");
			}
		}

		/// <summary>
		/// 비우기.
		/// </summary>
		public void Clear()
		{
			m_Label.text = string.Empty;
		}

		/// <summary>
		/// 글자 추가.
		/// </summary>
		public void Set(string text)
		{
			m_Label.text = text;
		}

		/// <summary>
		/// 글자 추가.
		/// </summary>
		public void Add(string text)
		{
			m_Label.text += text;
		}
	}
}