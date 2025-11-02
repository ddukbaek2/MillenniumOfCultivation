namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 영웅 캐릭터. (전투 진입시 존재하는 주요 객체)
	/// </summary>
	public abstract class HeroCharacter : Character
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public HeroCharacter(ulong instanceId) : base(instanceId)
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