namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 턴 이벤트.
	/// </summary>
	public class TurnEvent : Event
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public TurnEvent() : base()
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

			// 차례 시작.
			context.Next<PhaseEvent>();
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