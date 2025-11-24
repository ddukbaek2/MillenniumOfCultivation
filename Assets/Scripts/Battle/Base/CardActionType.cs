namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 카드 행동 종류.
	/// </summary>
	public enum CardActionType
	{
		/// <summary>
		/// 없음.
		/// </summary>
		None = 0,

		/// <summary>
		/// 공격 카드.
		/// </summary>
		Attack,

		/// <summary>
		/// 스킬 카드.
		/// </summary>
		Skill,

		/// <summary>
		/// 파워 카드.
		/// </summary>
		Power,

		/// <summary>
		/// 상태 이상 카드.
		/// </summary>
		Abnormal,
	}
}