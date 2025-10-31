namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 카드 사용 이벤트.
	/// </summary>
	public class CardPlayEvent : Event
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public CardPlayEvent() : base()
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