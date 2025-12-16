namespace Crockhead.Unity.UI
{
	/// <summary>
	/// UI 확장 유틸리티.
	/// </summary>
	public static class UIExtensions
	{
		/// <summary>
		/// 비동기 출력.
		/// </summary>
		public static void PresentAsync(this UIController presentingController, UIController presentedController)
		{
			presentingController.Present(presentedController);
		}
	}
}