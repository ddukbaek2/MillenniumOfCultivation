namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 차례 시작 이벤트.
	/// </summary>
	public class PhaseStartEvent : Event
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public PhaseStartEvent() : base()
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