namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 하수인 캐릭터. (필드에 스폰되거나 디스폰되어도 전투 종료와는 무관한 소비형 캐릭터 객체)
	/// </summary>
	public abstract class MinionCharacter : Character
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public MinionCharacter(ulong instanceId) : base(instanceId)
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