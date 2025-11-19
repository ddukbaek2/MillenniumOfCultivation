using UnityEngine;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 뷰가 될 수 있는 객체.
	/// </summary>
	public interface IUIView
	{
		/// <summary>
		/// 렉트 트랜스폼 프로퍼티.
		/// </summary>
		RectTransform RectTransform { get; }
	}
}