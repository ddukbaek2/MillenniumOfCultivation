namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 차례 이벤트.
	/// </summary>
	public class PhaseEvent : Event
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public PhaseEvent() : base()
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

			// 플레이어 차례.
			if (context.IsPlayerPhase)
			{
			}
			// 적 차례.
			else
			{
			}
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