namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 능력치 종류.
	/// </summary>
	public enum StatType
	{
		/// <summary>
		/// 없음.
		/// </summary>
		None = 0,

		/// <summary>
		/// 힘.
		/// </summary>
		Strength,
		STR = Strength,

		/// <summary>
		/// 민.
		/// </summary>
		Agility,
		AGI = Agility,

		/// <summary>
		/// 지.
		/// </summary>
		Intelligence,
		INT = Intelligence,

		/// <summary>
		/// 운.
		/// </summary>
		Luck,
		LCK = Luck,

		/// <summary>
		/// 솜씨.
		/// </summary>
		Dexterity,
		DEX = Dexterity,

		/// <summary>
		/// 체력.
		/// </summary>
		Health,
		HP = Health,

		/// <summary>
		/// 생명력.
		/// </summary>
		Vitality,
		VP = Vitality,

		/// <summary>
		/// 기력.
		/// </summary>
		Stemina,
		SP = Stemina,

		/// <summary>
		/// 영력.
		/// </summary>
		Mana,
		MP = Mana,

		/// <summary>
		/// 공격력.
		/// </summary>
		Offensive,
		ATK = Offensive,

		/// <summary>
		/// 수비력.
		/// </summary>
		Defensive,
		DEF = Defensive,

		/// <summary>
		/// 행동력.
		/// </summary>
		Active,
		AP = Active,

		/// <summary>
		/// 치명타 배수.
		/// </summary>
		Critical,

		/// <summary>
		/// 치명타 확률.
		/// </summary>
		CriticalRate,

		/// <summary>
		/// 회피 확률.
		/// </summary>
		EvasionRate,
	}
}