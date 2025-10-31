namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 하수인 소환 이벤트.
	/// </summary>
	public class MinionSpawnEvent : Event
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public MinionSpawnEvent() : base()
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