namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 차례 종료 이벤트.
	/// </summary>
	public class PhaseEndEvent : Event
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public PhaseEndEvent() : base()
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