using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 메시지 뷰.
	/// </summary>
	public class UIMessageView : UIPanelView
	{
		#region INSPECTOR
		//[SerializeField] private Image m_OverlayImage;
		//[SerializeField] private RawImage m_LogoImage;
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			if (IsDestroyed())
				return;

			BackgroundColor = new Color32(255, 178, 0, 255);
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
		/// 모든 메시지 제거.
		/// </summary>
		public void RemoveAllMessages()
		{
		}

		/// <summary>
		/// 메시지 추가.
		/// </summary>
		public void AddMessage()
		{
		}
	}
}