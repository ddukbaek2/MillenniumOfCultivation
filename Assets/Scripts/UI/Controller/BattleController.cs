using Crockhead.Unity;
using Crockhead.Unity.UI;
using MillenniumOfCultivation.Battle;
using System.Collections.Generic;
using UnityEngine;


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
		public BattleController() : base()
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

			View.BindButtonClickEvent("Left/Reset", OnReset);
			View.BindButtonClickEvent("Left/Menu", OnMenu);

			DispatchQueue.Foreground.RunAsync(OnPrepare);
		}

		/// <summary>
		/// 재시작.
		/// </summary>
		private void OnReset()
		{
			Debug.Log("[BattleController] OnReset()");
			//DispatchQueue.Instance.RunAsync(static () => {

			//});
			RuntimeInitializer.Shutdown();
			RuntimeInitializer.Initialize();
		}

		/// <summary>
		/// 재시작.
		/// </summary>
		private void OnMenu()
		{
			Debug.Log("[BattleController] OnMenu()");

			//Present();
		}

		/// <summary>
		/// 준비.
		/// </summary>
		private void OnPrepare()
		{
			var player = new PlayerController();
			var enemy = new AIController();
			var process = new BattleProcess();
			process.Start(player, new List<AIController>() { enemy });

			// 더미 카드 생성.
			var cardCount = 5;
			var spacing = 8f;
			var cardSize = new Vector2(260f, 400f);
			var totalWidth = cardCount * cardSize.x + (cardCount - 1) * spacing;
			var startX = -totalWidth * 0.5f + cardSize.x * 0.5f;
			for (var i = 0; i < cardCount; ++i)
			{
				var card = new CardController(Window);
				m_Cards.Add(card);

				var x = startX + i * (cardSize.x + spacing);
				card.View.transform.SetParent(View.Content, true);
				card.View.RectTransform.anchoredPosition = new Vector2(x, 0f);
				card.SetViewState(CardController.ViewState.Idle);
			}

			OnStart();
		}

		/// <summary>
		/// 시작.
		/// </summary>
		private void OnStart()
		{
		}
	}
}