using Crockhead.Unity;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 전투 UI 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UIBattleView), "Assets/Resources/UIKitLite/UIBattleView.prefab", AssetPathType.Resources)]
	public class BattleController : UIController
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public BattleController() : base()
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
		}
	}
}