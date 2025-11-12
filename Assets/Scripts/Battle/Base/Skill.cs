using Crockhead.Core;
using UID = System.UInt64;


namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 스킬.
	/// </summary>
	public abstract class Skill : Identifiable
	{
		public UID ID;

		/// <summary>
		/// 스킬 테이블 식별자.
		/// </summary>
		public int SkillTableId { get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Skill(int skillTableId) : base()
		{
			SkillTableId = skillTableId;
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