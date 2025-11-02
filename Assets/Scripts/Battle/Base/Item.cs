namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 아이템.
	/// </summary>
	public abstract class Item : Identifiable
	{
		/// <summary>
		/// 카드 테이블 식별자.
		/// </summary>
		public int CardTableId { set; get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Item(ulong instanceId) : base(instanceId)
		{
			CardTableId = 0;
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