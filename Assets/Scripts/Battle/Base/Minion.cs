namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 하수인.
	/// </summary>
	public abstract class Minion : Identifiable
	{
		/// <summary>
		/// 미니언 테이블 식별자.
		/// </summary>
		public int MinionTableId { set; get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Minion(ulong instanceId) : base(instanceId)
		{
			MinionTableId = 0;
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