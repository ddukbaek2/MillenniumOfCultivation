using Crockhead.Unity;
using Crockhead.Unity.UI;
using MillenniumOfCultivation.Battle;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 타이틀 UI 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UITitleView), "Assets/Resources/UI/UITitleView.prefab", AssetPathType.Resources)]
	public class UITitleController : UIController<UITitleView>
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public UITitleController() : base()
		{
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

			// 이벤트 바인딩.
			View.OnPlayEvent += OnPlay;
			View.OnOptionEvent += OnOption;
			View.OnCreditEvent += OnCredit;
			View.OnExitEvent += OnExit;

			// 애니메이션 시작.
			View.PlayApearAnimation(OnCompleteAnimation);

			//TaskHelper.StartForeground();

			//DispatchQueue.Instance.RunAsync(async () =>
			//{
			//	var battle = new UIBattleController(Window);
			//	Present(battle);
			//});

			//UIManager.Instance.UITransitionController.DoTransition(UITransitionController.TransitionType.FadeIn, () =>
			//{
			//	var battle = new UIBattleController(Window);
			//	var view = battle.View;
			//	UIManager.Instance.UITransitionController.DoTransition(UITransitionController.TransitionType.FadeOut, null);
			//});
		}

		/// <summary>
		/// 애니메이션 완료.
		/// </summary>
		private void OnCompleteAnimation()
		{
		}

		/// <summary>
		/// 시작 눌림.
		/// </summary>
		private void OnPlay()
		{
			var player = new PlayerController();
			var enemy = new AIController();
			var process = new BattleProcess();
			process.Start(player, new List<AIController>() { enemy });

			var battle = new UIBattleController();
			Present(battle);
		}

		/// <summary>
		/// 설정 눌림.
		/// </summary>
		private void OnOption()
		{
			var message = new UIMessageController();
			Present(message);
		}

		/// <summary>
		/// 만든이 눌림.
		/// </summary>
		private void OnCredit()
		{
		}

		/// <summary>
		/// 나가기 눌림.
		/// </summary>
		private void OnExit()
		{
			Application.Quit();
		}
	}
}