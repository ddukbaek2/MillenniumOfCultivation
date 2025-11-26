namespace Outsourcing
{
	/// <summary>
	/// UI 생성 처리기.
	/// </summary>
	public class UICreator : Singleton<UICreator>
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose()
		{
			base.OnDispose();
		}

		/// <summary>
		/// 모델 생성.
		/// </summary>
		public static TUIModel CreateModel<TUIModel>() where TUIModel : UIModel
		{
			var model = UIScriptable.Create<TUIModel>();
			return model;
		}

		/// <summary>
		/// 뷰 생성.
		/// </summary>
		public static TUIView CreateView<TUIView>() where TUIView : UIView
		{
			var view = UIView.Create<TUIView>();
			return view;
		}

		/// <summary>
		/// 애셋을 통한 뷰 생성.
		/// </summary>
		public static TUIView CreateViewFromAsset<TUIView>(string assetPath) where TUIView : UIView
		{
			var view = UIView.CreateFromAsset<TUIView>(assetPath);
			return view;
		}

		/// <summary>
		/// 프레젠터 생성.
		/// </summary>
		public static TUIPresenter CreatePresenter<TUIPresenter>() where TUIPresenter : UIPresenter
		{
			var presenter = UIScriptable.Create<TUIPresenter>();
			return presenter;
		}
	}
}