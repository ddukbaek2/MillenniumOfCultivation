using UnityEngine;
using UnityEngine.UI;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 이미지 뷰.
	/// </summary>
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasRenderer))]
	public sealed class UIImageView : Image, IUIWidget
	{
		/// <summary>
		/// 렉트 트랜스폼 프로퍼티.
		/// </summary>
		public RectTransform RectTransform => rectTransform;
	}
}