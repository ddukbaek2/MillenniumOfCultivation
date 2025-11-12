namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 카드.
	/// </summary>
	public abstract class Card : Identifiable
	{
		/// <summary>
		/// 카드 테이블 식별자.
		/// </summary>
		public int CardTableId { get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Card(int cardTableId) : base()
		{
			CardTableId = cardTableId;
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