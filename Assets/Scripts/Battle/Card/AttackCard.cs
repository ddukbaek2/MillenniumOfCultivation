namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 공격 카드.
	/// </summary>
	public class AttackCard : Card
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public AttackCard(int instanceId) : base(instanceId)
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