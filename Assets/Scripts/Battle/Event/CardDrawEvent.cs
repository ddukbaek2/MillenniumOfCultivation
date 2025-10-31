namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 카드 뽑기 이벤트.
	/// </summary>
	public class CardDrawEvent : Event
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public CardDrawEvent() : base()
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