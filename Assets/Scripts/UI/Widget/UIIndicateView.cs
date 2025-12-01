using Crockhead.Unity.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 인디케이터.
	/// </summary>
	public class UIIndicateView : UIView
	{
		#region INSPECTOR
		[SerializeField] private UIImageView m_Indicator;
		#endregion

		/// <summary>
		/// 트윈 객체.
		/// </summary>
		private TweenerCore<float, float, FloatOptions> m_Tweener;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			if (m_Indicator == null)
			{
				m_Indicator = GetOrAddComponent<UIImageView>("Indicator");
			}

			m_Tweener = null;
		}

		/// <summary>
		/// 활성화됨.
		/// </summary>
		protected override void OnEnable()
		{
			base.OnEnable();

			Play();
		}

		/// <summary>
		/// 애니메이션 재생.
		/// </summary>
		public void Play()
		{
			if (m_Indicator == null)
				return;

			m_Indicator.fillAmount = 0f;
			m_Tweener = m_Indicator.DOFillAmount(1f, 1f);
			m_Tweener.SetEase(Ease.Linear);
			m_Tweener.SetLoops(-1, LoopType.Restart);
		}
	}
}