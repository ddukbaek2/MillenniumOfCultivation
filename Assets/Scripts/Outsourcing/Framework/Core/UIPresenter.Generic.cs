using UnityEngine;


namespace Outsourcing
{
	/// <summary>
	/// 제너릭 제시자.
	/// </summary>
	public abstract class UIPresenter<TUIView> : UIPresenter where TUIView : UIView
	{
		/// <summary>
		/// 로드된 뷰 프로퍼티. (자동 생성하지 않음)
		/// </summary>
		public new TUIView LoadedRootView => (TUIView)base.LoadedRootView;

		/// <summary>
		/// 뷰 프로퍼티. (없을 경우 동기적 생성)
		/// </summary>
		public new TUIView RootView => (TUIView)base.RootView;
	}
}