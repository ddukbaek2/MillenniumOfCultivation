using Crockhead.Unity;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 메시지 UI 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UIMessageView), "Assets/Resources/UI/UIMessageView.prefab", AssetPathType.Resources)]
	public class MessageController : UIController<UIMessageView>
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public MessageController(UIWindow window) : base(window)
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
	}
}