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
			static IEnumerator Process(Image overlayImage, Action completion)
			{
				overlayImage.color = Color.black;
				var tween = overlayImage.DOColor(Color.clear, 2f);
				yield return tween.WaitForCompletion();
				completion?.Invoke();
			}

			await TaskHelper.StartForeground(Process(m_OverlayImage, completion));
		}
	}
}