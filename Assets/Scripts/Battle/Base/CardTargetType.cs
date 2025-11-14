namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 카드 대상 종류.
	/// </summary>
	public enum CardTargetType
	{
		/// <summary>
		/// 없음.
		/// </summary>
		None = 0,

		/// <summary>
		/// 자신.
		/// </summary>
		Self,

		/// <summary>
		/// 아군.
		/// </summary>
		Friendly,

		/// <summary>
		/// 적군.
		/// </summary>
		Enemy,

		/// <summary>
		/// 전체.
		/// </summary>
		All,
	}
}