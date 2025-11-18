using Crockhead.Unity.UI;
using TMPro;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 메시지 항목 뷰.
	/// </summary>
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
		protected override void Awake()
		{
			base.Awake();

			if (IsDestroyed())
				return;

			BackgroundColor = Color.black;
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void Start()
		{
			base.Start();
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		/// <summary>
		/// 메시지 설정.
		/// </summary>
		public void SetMessage(Message message)
		{
			var timestamp = message.DateTime.ToString("yyyy-MM-dd HH:mm:ss");
			m_Label.text = $"[{timestamp}] {message.Text}";
		}
	}
}