namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 하수인 소환 해제 이벤트. (사망)
	/// </summary>
	public class MinionDespawnEvent : Event
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public MinionDespawnEvent() : base()
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