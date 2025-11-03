using UnityEngine;


namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 전투 이벤트.
	/// </summary>
	public class BattleEvent : Event
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public BattleEvent() : base()
		{
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			base.OnDispose(explicitDisposing);
		}

		/// <summary>
		/// 이벤트 시작됨.
		/// </summary>
		protected override void OnStart(Context context)
		{
			base.OnStart(context);

			// 턴 시작.
			context.Next<TurnEvent>();
		}

		/// <summary>
		/// 이벤트 처리됨.
		/// </summary>
		protected override void OnProcess(Context context)
		{
			base.OnProcess(context);
		}

		/// <summary>
		/// 이벤트 전환됨.
		/// </summary>
		protected override void OnTransition(Context context, Event previous, Event next)
		{
			base.OnTransition(context, previous, next);
		}

		/// <summary>
		/// 이벤트 완료됨.
		/// </summary>
		protected override void OnComplete(Context context)
		{
			base.OnComplete(context);
		}
	}
}