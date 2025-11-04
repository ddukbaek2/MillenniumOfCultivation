using Crockhead.Unity;
using MillenniumOfCultivation.UI;
using UnityEngine;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 카드 컨트롤러.
	/// </summary>
	[UIViewBindingAttribute(typeof(UICardView), "Assets/Resources/UI/UICardView.prefab", AssetPathType.Resources)]
	public class CardController : UIController
	{
		/// <summary>
		/// 카드 뷰 상태.
		/// </summary>
		public enum ViewState
		{
			/// <summary>
			/// 상태 없음.
			/// </summary>
			None = 0,

			/// <summary>
			/// 대기.
			/// </summary>
			Idle,
		}

		/// <summary>
		/// 카드 뷰 상태.
		/// </summary>
		private ViewState m_State;

		/// <summary>
		/// 현재 카드 뷰 애니메이션.
		/// </summary>
		private UITweenAnimation m_Animation;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public CardController() : base()
		{
			m_State = ViewState.None;
			m_Animation = null;

			//// 카드 뷰 생성.
			//var cardView = UIManager.SharedInstance.CreateViewFromAsset<UIView>("Assets/Resources/UI/UICardView.prefab");
			//cardView.RectTransform.anchoredPosition = Vector2.zero;

			//// 애니메이션 생성.
			//var cardAnimation = UITweenAnimation.StartFlootAnimation(cardView.RectTransform, 32f, 3f);
		}

		/// <summary>
		/// 뷰 로드됨.
		/// </summary>
		protected override void OnViewDidLoad()
		{
			base.OnViewDidLoad();

			View.RectTransform.anchoredPosition = Vector2.zero;

			// 애니메이션 생성.
			var cardAnimation = UITweenAnimation.StartFlootAnimation(View.RectTransform, 32f, 3f);
		}

		/// <summary>
		/// 뷰 상태 변경.
		/// </summary>
		public void SetViewState(ViewState state, bool forced = true)
		{
			if (m_State == state && !forced)
				return;

			m_State = state;

			switch (state)
			{
				case ViewState.Idle:
					{
						UITweenAnimation.StartFlootAnimation(View.RectTransform, 32f, 2f);
						break;
					}
			}
		}
	}
}