using Crockhead.Unity;
using Crockhead.Unity.UI;
using MillenniumOfCultivation.UI;
using UnityEngine;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 카드 UI 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UICardView), "Assets/Resources/UI/UICardView.prefab", AssetPathType.Resources)]
	public class CardController : UIController<UICardView>
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
			/// 카드 뽑기. (카드패에서 손패로 넘어오는 상태. = Spawn)
			/// </summary>
			Draw,

			/// <summary>
			/// 대기. (손패에서 대기)
			/// </summary>
			Idle,

			/// <summary>
			/// 카드 선택. (손패에서 포커스 되며, 사용하거나 취소하는 수밖에 없음)
			/// </summary>
			Select,

			/// <summary>
			/// 사용.
			/// </summary>
			Use,

			/// <summary>
			/// 대상 지정 사용.
			/// </summary>
			UseToTarget,

			/// <summary>
			/// 사용 취소.
			/// </summary>
			Cancel,
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
		public CardController(UIWindow window) : base(window)
		{
			m_State = ViewState.None;
			m_Animation = null;

			//// 카드 뷰 생성.
			//var cardView = UIManager.Instance.CreateFromAsset<UIView>("Assets/Resources/UI/UICardView.prefab");
			//cardView.RectTransform.anchoredPosition = Vector2.zero;

			//// 애니메이션 생성.
			//var cardAnimation = UITweenAnimation.StartFlootCardAnimation(cardView.RectTransform, 32f, 3f);
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			base.OnDispose(explicitDisposing);
		}

		/// <summary>
		/// 뷰 로드됨.
		/// </summary>
		protected override void OnViewDidLoad()
		{
			base.OnViewDidLoad();

			View.RectTransform.anchoredPosition = Vector2.zero;
			View.OnBeginDraggedEvent += OnBegin;
			View.OnEndDraggedEvent += OnEnd;
			//View.SetData();
		}

		protected virtual void OnBegin(UIDraggableItemView view)
		{
		}

		protected virtual void OnEnd(UIDraggableItemView view)
		{
		}

		/// <summary>
		/// 뷰 상태 설정.
		/// </summary>
		public void SetViewState(ViewState state, bool forced = false)
		{
			if (state == ViewState.None)
				return;

			if (m_State == state && !forced)
				return;

			m_State = state;

			switch (state)
			{
				case ViewState.Idle:
					{
						//m_Animation = UITweenAnimation.StartFlootCardAnimation(View.RectTransform, 32f, 2f);
						break;
					}

				case ViewState.Select:
					{
						break;
					}
			}
		}
	}
}