using Crockhead.Unity;
using Crockhead.Unity.UI;
using System;
using System.Collections;
using System.Threading.Tasks;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 화면 최상위 영역 UI 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UITransitionView), "Assets/Resources/UI/UITransitionView.prefab", AssetPathType.Resources)]
	public class TransitionController : UIController<UITransitionView>
	{
		public enum TransitionType
		{
			FadeOut,
			FadeIn,
		}


		/// <summary>
		/// 생성됨.
		/// </summary>
		public TransitionController(UIWindow window) : base(window)
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

			//RootView.RectTransform.anchoredPosition = Vector2.zero;
		}

		/// <summary>
		/// 트랜지션 시작.
		/// </summary>
		public async Task DoTransitionAsync(TransitionType type)
		{
			static IEnumerator Process(UITransitionView view, TransitionType type)
			{
				//while (view.IsTransitioning)
				//	yield return null;
				view.DoTransition(type);
				while (view.IsTransitioning)
					yield return null;
				yield break;
			}

			await TaskHelper.StartForeground(Process(View, type));
		}

		/// <summary>
		/// 트랜지션 시작.
		/// </summary>
		public void DoTransition(TransitionType type, Action completion)
		{
			var task = DoTransitionAsync(type);
			task.ContinueWith((task) => completion?.Invoke());
		}
	}
}