namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 전투 종료 이벤트.
	/// </summary>
	public class BattleEndEvent : Event
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public BattleEndEvent() : base()
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