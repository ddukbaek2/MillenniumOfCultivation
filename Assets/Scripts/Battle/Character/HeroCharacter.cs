namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 영웅 캐릭터.
	/// </summary>
	public abstract class HeroCharacter : Character
	{
		/// <summary>
		/// 영웅 테이블 식별자 프로퍼티.
		/// </summary>
		public int HeroTableId { set; get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		public HeroCharacter(int heroTableId) : base()
		{
			HeroTableId = heroTableId;
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