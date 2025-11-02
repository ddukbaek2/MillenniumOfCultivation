using System.Collections.Generic;


namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 컨트롤러가 제어하는 대상이자 전투의 주도 객체.
	/// </summary>
	public abstract class Character : Identifiable
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public Character(ulong instanceId) : base(instanceId)
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