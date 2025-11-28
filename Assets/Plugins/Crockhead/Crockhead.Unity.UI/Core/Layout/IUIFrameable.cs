namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 레이아웃에 포함 될 수 있는 인터페이스.
	/// </summary>
	public interface IUIFrameable
	{
		/// <summary>
		/// 프레임 프로퍼티.
		/// </summary>
		UIFrame Frame { get; }

		/// <summary>
		/// 프레임 갱신.
		/// </summary>
		void FrameUpdate();

		/// <summary>
		/// 레이아웃 갱신.
		/// </summary>
		void LayoutUpdate();
	}
}