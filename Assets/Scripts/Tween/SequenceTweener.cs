using System.Collections.Generic;
using UnityEngine;


namespace MillenniumOfCultivation.Tween
{
	/// <summary>
	/// 시퀀스 트윈.
	/// </summary>
	public class SequenceTweener : Tweener
	{
		private List<Tweener> m_Tweens;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public SequenceTweener(Target target, Value from, Value to, Displacer displacer) : base(target, from, to, displacer)
		{
			m_Tweens = new List<Tweener>();
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			base.OnDispose(explicitDisposing);
		}

		/// <summary>
		/// 시작됨.
		/// </summary>
		protected override void OnStarted()
		{
			base.OnStarted();
		}

		/// <summary>
		/// 완료됨.
		/// </summary>
		protected override void OnCompleted()
		{
			base.OnCompleted();
		}

		/// <summary>
		/// 비우기.
		/// </summary>
		public void Clear()
		{
			m_Tweens.Clear();
		}

		/// <summary>
		/// 추가.
		/// </summary>
		public void Add(Tweener tweener)
		{
			if (m_Tweens.Contains(tweener))
				return;

			m_Tweens.Add(tweener);
		}
	}
}