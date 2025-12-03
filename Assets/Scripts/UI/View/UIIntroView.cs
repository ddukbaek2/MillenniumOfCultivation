using Crockhead.Unity;
using Crockhead.Unity.UI;
using DG.Tweening;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
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
		#endregion

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
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void OnInitialize()
		{
			base.OnInitialize();
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose()
		{
			base.OnDispose();
		}

		/// <summary>
		/// 애니메이션 시작.
		/// </summary>
		public async Task StartAnimation(Action completion)
		{
			static IEnumerator Process(UIIntroView view, Action completion)
			{
				view.m_OverlayImage.color = Color.black;
				var fadeInTween = view.m_OverlayImage.DOColor(Color.clear, 2f);

				view.m_LogoImage.transform.localScale = Vector3.one * 1f;
				var scaleUpTween = view.m_LogoImage.transform.DOScale(Vector3.one * 1.25f, 2f);

				var sequenceTween = DOTween.Sequence();
				sequenceTween.Join(fadeInTween);
				sequenceTween.Join(scaleUpTween);

				yield return sequenceTween.WaitForCompletion();
				completion?.Invoke();
			}

			await TaskHelper.StartForeground(Process(this, completion));
		}
	}
}