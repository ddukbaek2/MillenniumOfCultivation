namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 전투 시작 이벤트.
	/// </summary>
	public class BattleStartEvent : Event
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public BattleStartEvent() : base()
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