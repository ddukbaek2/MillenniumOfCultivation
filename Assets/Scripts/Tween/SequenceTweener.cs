using System.Collections.Generic;
using UnityEngine;


namespace MillenniumOfCultivation.Tween
{
	/// <summary>
	/// 시퀀스 트윈.
	/// </summary>
	public class SequenceTweener : Tweener
	{
		/// <summary>
		/// 트윈 목록.
		/// </summary>
		private List<Tweener> m_Tweens;

		/// <summary>
		/// 현재 위치.
		/// </summary>
		private int m_Position;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public SequenceTweener(Target target, Displacer displacer, Value fromValue, Value toValue) : base(target, displacer, fromValue, toValue)
		{
			m_Tweens = new List<Tweener>();
			m_Position = -1;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			m_Tweens.Clear();

			base.OnDispose(explicitDisposing);
		}

		/// <summary>
		/// 시작됨.
		/// </summary>
		protected override void OnStarted()
		{
			base.OnStarted();

			m_Position = 0;
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