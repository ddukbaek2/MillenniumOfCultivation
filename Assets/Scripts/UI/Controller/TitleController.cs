using Crockhead.Unity;
using Crockhead.Unity.UI;
using MillenniumOfCultivation.Battle;
using System.Collections.Generic;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 타이틀 UI 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UITitleView), "Assets/Resources/UI/UITitleView.prefab", AssetPathType.Resources)]
	public class TitleController : UIController<UITitleView>
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public TitleController() : base()
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

			View.OnPlayEvent += OnPlay;
			View.OnOptionEvent += OnOption;
			View.OnCreditEvent += OnCredit;
			View.OnExitEvent += OnExit;

			//DispatchQueue.Instance.RunAsync(async () =>
			//{
			//	var battle = new BattleController(Window);
			//	Present(battle);
			//});

			//UIManager.Instance.TransitionController.DoTransition(TransitionController.TransitionType.FadeIn, () =>
			//{
			//	var battle = new BattleController(Window);
			//	var view = battle.View;
			//	UIManager.Instance.TransitionController.DoTransition(TransitionController.TransitionType.FadeOut, null);
			//});
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

			var battle = new BattleController();
			Present(battle);
		}

		/// <summary>
		/// 설정 눌림.
		/// </summary>
		private void OnOption()
		{
			var message = new MessageController();
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