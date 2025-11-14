using Crockhead.Unity;
using System.Collections.Generic;
using UnityEngine;


namespace MillenniumOfCultivation.Tween
{
	/// <summary>
	/// 트윈 매니저.
	/// </summary>
	public class TweenManager : SharedComponent<TweenManager>
	{
		/// <summary>
		/// 현재 동작 중인 트윈 목록.
		/// </summary>
		private List<Tweener> m_Tweeners;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			m_Tweeners = new List<Tweener>();
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		/// <summary>
		/// 갱신됨.
		/// </summary>
		private void Update()
		{
			var timeDelta = Time.deltaTime;

			var snapshot = m_Tweeners.ToArray();
			foreach (var tweener in snapshot)
			{
				tweener.Update(timeDelta);
				if (tweener.IsPlaying)
					continue;

				m_Tweeners.Remove(tweener);
			}
		}
	}
}