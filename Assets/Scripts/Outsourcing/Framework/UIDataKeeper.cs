namespace Outsourcing
{
	/// <summary>
	/// UI 데이터 관리 처리자.
	/// </summary>
	public class UIDataKeeper : Singleton<UIDataKeeper>
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
		/// 데이터 생성.
		/// </summary>
		public static TUIData CreateData<TUIData>() where TUIData : UIData
		{
			var data = UIScriptable.Create<TUIData>();
			return data;
		}
	}
}