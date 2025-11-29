using Crockhead.Unity;
using Crockhead.Unity.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 트랜지션 뷰.
	/// </summary>
	[AssetPath("Assets/Resources/UI/UITransitionView.prefab", AssetPathType.Resources)]
	public class UITransitionView : UIPanelView
	{
		#region INSPECTOR
		//[SerializeField] private Image m_LogoImage;
		#endregion

		/// <summary>
		/// 트랜지션 진행 중 여부.
		/// </summary>
		private bool m_IsTransitioning;

		/// <summary>
		/// 트랜지션 진행 중 여부 프로퍼티.
		/// </summary>
		public bool IsTransitioning => m_IsTransitioning;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			BackgroundColor = Color.clear;
			m_IsTransitioning = false;
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
		/// 시작됨.
		/// </summary>
		protected virtual void OnTransitionStarted()
		{
		}

		/// <summary>
		/// 완료됨.
		/// </summary>
		protected virtual void OnTransitionCompleted()
		{
		}

		/// <summary>
		/// 트랜지션 효과 시작.
		/// </summary>
		public void DoTransition(TransitionController.TransitionType type)
		{
			if (m_IsTransitioning)
				return;

			m_IsTransitioning = true;
			OnTransitionStarted();

			switch (type)
			{
				case TransitionController.TransitionType.FadeOut:
					{
						var target = GetOrAddComponent<Image>("FadeOut");
						target.color = Color.clear;
						var tween = target.DOColor(Color.black, 1f);
						tween.OnComplete(OnTransitionCompleted);
						break;
					}

				case TransitionController.TransitionType.FadeIn:
					{
						var target = GetOrAddComponent<Image>("FadeIn");
						target.color = Color.black;
						var tween = target.DOColor(Color.clear, 1f);
						tween.OnComplete(OnTransitionCompleted);
						break;
					}
			}
		}
	}
}