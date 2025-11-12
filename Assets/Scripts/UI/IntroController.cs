using Crockhead.Unity;
using System.Numerics;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 인트로 UI 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UIIntroView), "Assets/Resources/UI/UIIntroView.prefab", AssetPathType.Resources)]
	public class IntroController : UIController<UIIntroView>
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public IntroController(UIWindow window) : base(window)
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

			//View.RectTransform.anchoredPosition = Vector2.zero;
#pragma warning disable CS4014 // 이 호출을 대기하지 않으므로 호출이 완료되기 전에 현재 메서드가 계속 실행됩니다.
			View.StartAnimation(OnFinishAnimation);
#pragma warning restore CS4014 // 이 호출을 대기하지 않으므로 호출이 완료되기 전에 현재 메서드가 계속 실행됩니다.
		}

		/// <summary>
		/// 애니메이션 완료됨.
		/// </summary>
		private void OnFinishAnimation()
		{
			//UIManager.SharedInstance.TransitionController.StartTransition(TransitionController.TransitionType.FadeIn);
			var battleController = new BattleController(Window);
			battleController.LoadView();
		}
	}
}