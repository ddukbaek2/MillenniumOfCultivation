using Crockhead.Core;
using System;


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
		public new TUIView View => base.View as TUIView;

		/// <summary>
		/// 소유한 뷰 프로퍼티. (뷰 없으면 null)
		/// </summary>
		public new TUIView LoadedView => base.View as TUIView;

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
		/// 뷰 로드 직전 호출됨.
		/// </summary>
		protected override (Type ViewType, string AssetPath, AssetPathType AssetPathType) OnViewWillLoad(Type viewType)
		{
			// 인자로 넘어오는 기본 뷰를 무시하고, 제네릭으로 설정된 지정 뷰의 타입을 적용.
			viewType = typeof(TUIView);
			var viewConfiguration = base.OnViewWillLoad(viewType);				
			return viewConfiguration;
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