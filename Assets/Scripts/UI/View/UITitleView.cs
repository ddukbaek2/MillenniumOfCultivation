using Crockhead.Unity;
using Crockhead.Unity.UI;
using System;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 타이틀 뷰.
	/// </summary>
	[AssetPath("Assets/Resources/UI/UITitleView.prefab", AssetPathType.Resources)]
	public class UITitleView : UIPanelView
	{
		#region INSPECTOR
		[SerializeField] private UILabelView m_VersionLabel;
		#endregion

		/// <summary>
		/// 플레이 이벤트 프로퍼티.
		/// </summary>
		public Action OnPlayEvent { set; get; }

		/// <summary>
		/// 설정 이벤트 프로퍼티.
		/// </summary>
		public Action OnOptionEvent { set; get; }

		/// <summary>
		/// 크레디트 이벤트 프로퍼티.
		/// </summary>
		public Action OnCreditEvent { set; get; }

		/// <summary>
		/// 나가기 이벤트 프로퍼티.
		/// </summary>
		public Action OnExitEvent { set; get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			BackgroundColor = new Color32(255, 178, 0, 255);

			BindButtonClickEvent("Content/Play", OnClickPlay);
			BindButtonClickEvent("Content/Option", OnClickOption);
			BindButtonClickEvent("Content/Credit", OnClickCredit);
			BindButtonClickEvent("Content/Exit", OnClickExit);
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void OnInitialize()
		{
			base.OnInitialize();

			SetAnchor(true);
		}

		private void OnClickPlay()
		{
			OnPlayEvent?.Invoke();
		}

		private void OnClickOption()
		{
			OnOptionEvent?.Invoke();
		}

		private void OnClickCredit()
		{
			OnCreditEvent?.Invoke();
		}

		private void OnClickExit()
		{
			OnExitEvent?.Invoke();
		}

		///// <summary>
		///// 애니메이션 시작.
		///// </summary>
		//public async Task StartAnimation(Action completion)
		//{
		//	static IEnumerator BattleProcess(Image overlayImage, Action completion)
		//	{
		//		overlayImage.color = Color.black;
		//		var tween = overlayImage.DOColor(Color.clear, 2f);
		//		yield return tween.WaitForCompletion();
		//		completion?.Invoke();
		//	}

		//	await TaskHelper.StartForeground(BattleProcess(m_OverlayImage, completion));
		//}
	}
}