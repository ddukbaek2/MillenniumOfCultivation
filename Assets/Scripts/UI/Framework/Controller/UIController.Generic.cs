using Crockhead.Core;
using Crockhead.Unity;
using UnityEngine;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 뷰 컨트롤러.
	/// </summary>
	public class UIController<TUIView> : UIController where TUIView : UIView
	{
		/// <summary>
		/// 소유한 뷰 프로퍼티. (자동생성)
		/// </summary>
		public new TUIView View => (TUIView)base.View;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public UIController() : base()
		{
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		public UIController(UIWindow window) : base(window)
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

		/// <summary>
		/// 뷰 나타나기 직전 호출됨.
		/// </summary>
		protected override void OnViewWillApear()
		{
			base.OnViewWillApear();
		}

		/// <summary>
		/// 뷰 나타난 직후 호출됨.
		/// </summary>
		protected override void OnViewDidAppear()
		{
			base.OnViewDidAppear();
		}

		/// <summary>
		/// 뷰 사라지기 직전 호출됨.
		/// </summary>
		protected override void OnViewWillDisapear()
		{
			base.OnViewWillDisapear();
		}

		/// <summary>
		/// 뷰 사라진 직후 호출됨.
		/// </summary>
		protected override void OnViewDidDisapear()
		{
			base.OnViewDidDisapear();
		}
	}
}