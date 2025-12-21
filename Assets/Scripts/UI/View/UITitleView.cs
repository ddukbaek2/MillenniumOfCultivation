using Crockhead.Unity;
using Crockhead.Unity.UI;
using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


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

		/// <summary>
		/// 등장 연출.
		/// </summary>
		public async Task PlayApearAnimationAsync(Action completion)
		{
			var content = GetOrAddComponent<RectTransform>("Content");
			content.anchoredPosition = new Vector2(-364f, 0f);
			var tweener = content.DOAnchorPosX(0f, 1f);
			tweener.SetDelay(0.5f);
			tweener.OnComplete(new TweenCallback(completion));
			await tweener.AsyncWaitForCompletion();
		}

		/// <summary>
		/// 등장 연출.
		/// </summary>
		public void PlayApearAnimation(Action completion)
		{
			void Continuation(Task task)
			{
				completion?.Invoke();
			}

			var task = PlayApearAnimationAsync(completion);
			var continuation = task.ContinueWith(Continuation);
		}

		/// <summary>
		/// 시작.
		/// </summary>
		private void OnClickPlay()
		{
			SoundManager.Instance.Play("Sound_UI_Select");
			OnPlayEvent?.Invoke();
		}

		/// <summary>
		/// 설정.
		/// </summary>
		private void OnClickOption()
		{
			SoundManager.Instance.Play("Sound_UI_Select");
			OnOptionEvent?.Invoke();
		}

		/// <summary>
		/// 만든이.
		/// </summary>
		private void OnClickCredit()
		{
			SoundManager.Instance.Play("Sound_UI_Select");
			OnCreditEvent?.Invoke();
		}

		/// <summary>
		/// 나가기.
		/// </summary>
		private void OnClickExit()
		{
			SoundManager.Instance.Play("Sound_UI_Select");
			OnExitEvent?.Invoke();
		}
	}
}