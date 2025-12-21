using Crockhead.Unity;
using Crockhead.Unity.UI;
using DG.Tweening;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 인트로 뷰.
	/// </summary>
	[AssetPath("Assets/Resources/UI/UIIntroView.prefab", AssetPathType.Resources)]
	public class UIIntroView : UIPanelView
	{
		#region INSPECTOR
		[SerializeField] private UIImageView m_LogoImage;
		[SerializeField] private UIImageView m_OverlayImage;
		[SerializeField] private UIGraphicView m_TouchArea;
		#endregion

		/// <summary>
		/// 인트로 애니메이션 트윈.
		/// </summary>
		private Sequence m_Sequence;

		/// <summary>
		/// 클릭 이벤트 프로퍼티.
		/// </summary>
		public Action<PointerEventData> OnClickEvent { set; get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			BackgroundColor = new Color32(255, 178, 0, 255);

			if (m_LogoImage == null)
			{
				m_LogoImage = GetOrAddComponent<UIImageView>("Logo");
			}

			if (m_OverlayImage == null)
			{
				m_OverlayImage = GetOrAddComponent<UIImageView>("Overlay");
			}

			m_OverlayImage.color = Color.black;
			m_OverlayImage.gameObject.SetActive(true);

			if (m_TouchArea == null)
			{
				m_TouchArea = GetOrAddComponent<UIGraphicView>("TouchArea");
			}
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void OnInitialize()
		{
			base.OnInitialize();

			if (m_TouchArea != null)
			{
				m_TouchArea.OnClickEvent += OnClickEvent;
			}
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose()
		{
			if (m_TouchArea != null)
			{
				m_TouchArea.OnClickEvent -= OnClickEvent;
			}

			base.OnDispose();
		}

		/// <summary>
		/// 애니메이션 시작.
		/// </summary>
		public async Task StartAnimation(Action completion)
		{
			static IEnumerator Process(UIIntroView view, Sequence sequenceTween, Action completion)
			{
				// 페이드인.
				view.m_OverlayImage.color = Color.black;
				var fadeInTween = view.m_OverlayImage.DOColor(Color.clear, 2f);
				sequenceTween.Join(fadeInTween);

				// 확대.
				view.m_LogoImage.transform.localScale = Vector3.one * 1f;
				var scaleUpTween = view.m_LogoImage.transform.DOScale(Vector3.one * 1.15f, 2f);
				sequenceTween.Join(scaleUpTween);

				yield return sequenceTween.WaitForCompletion();
				completion?.Invoke();
			}

			m_Sequence = DOTween.Sequence();
			await TaskHelper.StartForeground(Process(this, m_Sequence, completion));
			m_Sequence = null;
		}

		/// <summary>
		/// 애니메이션 스킵.
		/// </summary>
		public void SkipAnimation()
		{
			if (m_Sequence == null)
				return;

			m_Sequence.Complete();
		}
	}
}