using Crockhead.Unity;
using Crockhead.Unity.UI;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 인트로 UI 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UIIntroView), "Assets/Resources/UI/UIIntroView.prefab", AssetPathType.Resources)]
	public class UIIntroController : UIController<UIIntroView>
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public UIIntroController() : base()
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

			View.OnClickEvent += (pointerEventData) => OnSkipAnimation();

			// 비동기 실행.
			DispatchQueue.Foreground.RunAsync(async () =>
			{
				await View.StartAnimation(OnFinishAnimation);
			});
		}

		/// <summary>
		/// 애니메이션 스킵됨.
		/// </summary>
		private void OnSkipAnimation()
		{
			Debug.Log("[UIIntroController] OnSkipAnimation()");

			View.SkipAnimation();
		}

		/// <summary>
		/// 애니메이션 완료됨.
		/// </summary>
		private void OnFinishAnimation()
		{
			Debug.Log("[UIIntroController] OnFinishAnimation()");

			SoundManager.Instance.Play("Assets/Resources/Sound/UI/00106.wav", AssetPathType.Resources, SoundType.UI);
			//UIManager.Instance.UITransitionController.DoTransitionAsync(UITransitionController.TransitionType.FadeIn);

			var title = new UITitleController();
			Present(title);
		}
	}
}