using Crockhead.Unity;
using Crockhead.Unity.UI;
using System.Collections.Generic;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 전투 UI 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UIBattleView), "Assets/Resources/UI/UIBattleView.prefab", AssetPathType.Resources)]
	public class BattleController : UIController<UIBattleView>
	{
		/// <summary>
		/// 카드 목록.
		/// </summary>
		private List<CardController> m_Cards;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public BattleController(UIWindow window) : base(window)
		{
			m_Cards = new List<CardController>();
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

			// 카드 생성.
			for (var i = 0; i < 10; ++i)
			{
				var card = new CardController(Window);
				m_Cards.Add(card);

				card.LoadView(); // card.RootView
				card.View.transform.SetParent(View.Content, true);
				//card.RootView.RectTransform.localPosition = Vector3.zero;
				//card.RootView.RectTransform.localScale = Vector3.one;
				//card.RootView.RectTransform.localRotation = Quaternion.identity;
				card.SetViewState(CardController.ViewState.Idle);
			}
		}
	}
}