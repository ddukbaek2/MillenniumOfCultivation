using Crockhead.Core;
using DG.Tweening;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 트윈 애니메이션.
	/// </summary>
	public class UITweenAnimation : Disposable
	{
		/// <summary>
		/// 대상의 렉트 트랜스폼.
		/// </summary>
		public RectTransform RectTransform { get; }

		/// <summary>
		/// 시퀀스.
		/// </summary>
		public Sequence Sequence { get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		private UITweenAnimation(RectTransform target, Sequence sequence = null) : base()
		{
			RectTransform = target;
			Sequence = sequence;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			Sequence.Kill();
			RectTransform.DOKill();
			//DOTween.Kill(RectTransform);
		}

		/// <summary>
		/// 애니메이션 시작.
		/// </summary>
		public static UITweenAnimation StartFlootAnimation(RectTransform target, float value, float duration)
		{
			value = Mathf.Abs(value);
			duration = duration * 0.5f;

			var moveUpwardTweener = target.DOAnchorPosY(value, duration);
			moveUpwardTweener.SetRelative(true);
			moveUpwardTweener.SetEase(Ease.InOutSine);
			moveUpwardTweener.SetUpdate(UpdateType.Normal, false);
			moveUpwardTweener.SetAutoKill(false);
			moveUpwardTweener.SetRecyclable(false);

			var moveDownwardTweener = target.DOAnchorPosY(-value, duration);
			moveDownwardTweener.SetRelative(true);
			moveDownwardTweener.SetEase(Ease.InOutSine);
			moveDownwardTweener.SetUpdate(UpdateType.Normal, false);
			moveDownwardTweener.SetAutoKill(false);
			moveDownwardTweener.SetRecyclable(false);

			var sequence = DOTween.Sequence();
			sequence.Append(moveUpwardTweener);
			sequence.Append(moveDownwardTweener);
			sequence.SetLoops(-1, LoopType.Restart);
			sequence.SetUpdate(UpdateType.Normal, false);
			sequence.SetAutoKill(false);
			sequence.SetRecyclable(false);

			var animation = new UITweenAnimation(target, sequence);
			return animation;
		}
	}
}