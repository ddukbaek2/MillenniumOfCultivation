using System;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 트랜지션 뷰.
	/// </summary>
	public class UITransitionView : UIPanelView
	{
		#region INSPECTOR
		//[SerializeField] private Image m_LogoImage;
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();
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
		/// 트랜지션 효과 시작.
		/// </summary>
		public void StartTransition(TransitionController.TransitionType type, Action completion)
		{

		}
	}
}