using Crockhead.Unity;
using System.Numerics;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 인트로 UI 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UIIntroView), "Assets/Resources/UI/UIIntroView.prefab", AssetPathType.Resources)]
	public class IntroController : UIController
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
		}

		
	}
}