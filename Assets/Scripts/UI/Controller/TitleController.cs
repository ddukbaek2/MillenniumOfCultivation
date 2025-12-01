using Crockhead.Unity;
using Crockhead.Unity.UI;


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
		public TitleController(UIWindow window) : base(window)
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
	}
}