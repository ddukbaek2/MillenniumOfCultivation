using Crockhead.Core;


namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 아이템.
	/// </summary>
	public abstract class Item : Identifiable
	{
		/// <summary>
		/// 아이템 테이블 식별자.
		/// </summary>
		public int ItemTableId { get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Item(int itemTableId) : base()
		{
			ItemTableId = itemTableId;
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