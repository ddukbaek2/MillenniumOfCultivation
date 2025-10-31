namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 턴 시작 이벤트.
	/// </summary>
	public class TurnStartEvent : Event
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public TurnStartEvent() : base()
		{
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			base.OnDispose(explicitDisposing);
		}
	}
}