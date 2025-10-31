namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 턴 종료 이벤트.
	/// </summary>
	public class TurnEndEvent : Event
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public TurnEndEvent() : base()
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