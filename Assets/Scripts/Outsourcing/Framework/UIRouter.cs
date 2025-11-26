namespace Outsourcing
{
	/// <summary>
	/// UI 경로 처리기.
	/// </summary>
	public class UIRouter : Singleton<UIRouter>
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
		/// UI 열기.
		/// </summary>
		public void Open(UIPresenter presenter)
		{

		}

		/// <summary>
		/// UI 닫기.
		/// </summary>
		public void Close(UIPresenter presenter)
		{

		}
	}
}