using Crockhead.Unity;
using Crockhead.Unity.UI;
using TMPro;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 메시지 항목 뷰.
	/// </summary>
	[AssetPath("Assets/Resources/UI/UIMessageItemView.prefab", AssetPathType.Resources)]
	public class UIMessageItemView : UIItemView
	{
		#region INSPECTOR
		//[SerializeField] private Image m_OverlayImage;
		//[SerializeField] private RawImage m_LogoImage;
		[SerializeField] private TextMeshProUGUI m_Label;
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			BackgroundColor = Color.black;
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void OnInitialize()
		{
			base.OnInitialize();

			var parentRectTransform = RectTransform.parent as RectTransform;
			RectTransform.sizeDelta = new Vector2(parentRectTransform.rect.width, 24f);
		}

		/// <summary>
		/// 메시지 설정.
		/// </summary>
		public void SetMessage(Message message)
		{
			//var timestampFormat = "yyyy-MM-dd HH:mm:ss";
			var timestampFormat = "HH:mm";
			var timestamp = message.DateTime.ToString(timestampFormat);
			m_Label.text = $"{message.ClientId} ({timestamp}): {message.Text}";
		}
	}
}