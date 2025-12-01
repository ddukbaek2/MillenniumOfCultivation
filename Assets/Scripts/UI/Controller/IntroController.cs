using Crockhead.Unity;
using Crockhead.Unity.UI;
using UnityEngine;


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
		public IntroController() : base()
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

			// 비동기 실행.
			DispatchQueue.Instance.RunAsync(async () =>
			{
				await View.StartAnimation(OnFinishAnimation);
			});
		}

		/// <summary>
		/// 애니메이션 완료됨.
		/// </summary>
		private void OnFinishAnimation()
		{
			Debug.Log("[IntroController] OnFinishAnimation()");

			SoundManager.Instance.Play("Assets/Resources/Sound/UI/00106.wav", AssetPathType.Resources, SoundType.UI);
			//UIManager.Instance.TransitionController.DoTransitionAsync(TransitionController.TransitionType.FadeIn);

			var title = new TitleController();
			Present(title);
		}
	}
}