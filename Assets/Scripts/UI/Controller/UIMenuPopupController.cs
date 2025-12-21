using Crockhead.Unity;
using Crockhead.Unity.UI;
using DG.Tweening;
using MillenniumOfCultivation.Battle;
using System.Collections.Generic;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 메뉴 팝업 UI 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UIMenuPopupView), "Assets/Resources/UI/UIMenuPopupView.prefab", AssetPathType.Resources)]
	public class UIMenuPopupController : UIController<UIMenuPopupView>
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public UIMenuPopupController() : base()
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

			View.BindButtonClickEvent("Content/Option", OnOption);
			View.BindButtonClickEvent("Content/Exit", OnExit);
			View.BindButtonClickEvent("Content/Resume", OnResume);

			View.gameObject.SetActive(true);
			View.RectTransform.localScale = Vector3.zero;
			View.RectTransform.DOScale(1f, 0.5f);
		}

		/// <summary>
		/// 설정 눌림.
		/// </summary>
		private void OnOption()
		{
		}

		/// <summary>
		/// 나가기 눌림.
		/// </summary>
		private void OnExit()
		{
		}

		/// <summary>
		/// 계속하기 눌림.
		/// </summary>
		private void OnResume()
		{
			Dismiss();
		}
	}
}