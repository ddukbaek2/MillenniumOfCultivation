using Crockhead.Unity;
using Crockhead.Unity.UI;
using MillenniumOfCultivation.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 전투 UI 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UIBattleView), "Assets/Resources/UI/UIBattleView.prefab", AssetPathType.Resources)]
	public class UIBattleController : UIController<UIBattleView>
	{
		/// <summary>
		/// 캐릭터 목록.
		/// </summary>
		private List<GameObject> m_Characters;

		/// <summary>
		/// 카드 목록.
		/// </summary>
		private List<UICardController> m_Cards;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public UIBattleController() : base()
		{
			m_Cards = new List<UICardController>();
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
			View.BindButtonClickEvent("Right/Menu", OnMenu);

			DispatchQueue.Foreground.RunAsync(OnPrepare);
		}

		/// <summary>
		/// 뷰 등장 시작됨.
		/// </summary>
		protected override void OnViewWillAppear(bool animated)
		{
			base.OnViewWillAppear(animated);

			//Debug.Log("")
		}

		/// <summary>
		/// 뷰 등장 완료됨.
		/// </summary>
		protected override void OnViewDidAppear()
		{
			base.OnViewDidAppear();


			// 전투 로그.
			// 스킬.
			// 어빌리티.
			// 아이템.
		}

		/// <summary>
		/// 뷰 퇴장 시작됨.
		/// </summary>
		protected override void OnViewWillDisappear(bool animated)
		{
			base.OnViewWillDisappear(animated);
		}

		/// <summary>
		/// 뷰 퇴장 완료됨.
		/// </summary>
		protected override void OnViewDidDisappear()
		{
			base.OnViewDidDisappear();
		}

		/// <summary>
		/// 현재 컨트롤러의 메인 뷰가 최상위 화면에 노출됨. (메인뷰의 서브뷰를 타깃으로한 컨트롤러는 해당 이벤트 전달 안됨)
		/// </summary>
		protected virtual void OnViewEnterForeground(UIView previous, UIView next, bool backward)
		{
		}

		protected virtual void OnBecameInvisible()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		private IEnumerator Process()
		{
			yield break;
		}

		/// <summary>
		/// 재시작.
		/// </summary>
		private void OnReset()
		{
			Debug.Log("[UIBattleController] OnReset()");
			DispatchQueue.Foreground.RunNextFrameAsync(RuntimeInitializer.Shutdown);
			DispatchQueue.Foreground.RunAsync(RuntimeInitializer.Initialize, 2);
		}

		/// <summary>
		/// 일시정지 & 메뉴화면.
		/// </summary>
		private void OnMenu()
		{
			Debug.Log("[UIBattleController] OnMenu()");

			var menuPopup = new UIMenuPopupController();
			//this.PresentingController
			PresentAsync(menuPopup);
		}

		/// <summary>
		/// 도망. (탈출)
		/// </summary>
		private void OnEscape()
		{
			// 전투 패배.
			// 
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
			var cardCount = 10;
			for (var i = 0; i < cardCount; ++i)
			{
				var card = new UICardController(Window);
				m_Cards.Add(card);
				card.View.transform.SetParent(View.Content, true);
				card.View.RectTransform.anchorMin = Vector2.one * 0.5f;
				card.View.RectTransform.anchorMax = Vector2.one * 0.5f;
				card.View.RectTransform.pivot = Vector2.one * 0.5f;
				card.View.RectTransform.sizeDelta = new Vector2(240f, 400f);
				card.SetViewState(UICardController.ViewState.Idle);
			}

			UpdateAllCardPositions();
			OnStart();
		}

		/// <summary>
		/// 시작.
		/// </summary>
		private void OnStart()
		{
			CoroutineHelper.StartCoroutine(Process());
		}

		/// <summary>
		/// 카드 위치 갱신.
		/// </summary>
		private void UpdateAllCardPositions()
		{
			var cardCount = m_Cards.Count;
			var spacing = 8f;
			var cardSize = new Vector2(260f, 400f);
			var totalWidth = cardCount * cardSize.x + (cardCount - 1) * spacing;
			var startX = -totalWidth * 0.5f + cardSize.x * 0.5f;
			for (var i = 0; i < m_Cards.Count; ++i)
			{
				var card = m_Cards[i];
				var x = startX + i * (cardSize.x + spacing);
				card.View.RectTransform.anchoredPosition = new Vector2(x, 0f);
			}
		}
	}
}