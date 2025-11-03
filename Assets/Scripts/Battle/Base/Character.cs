using System.Collections.Generic;


namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 컨트롤러가 제어하는 대상이자 전투의 주도 객체.
	/// </summary>
	public abstract class Character : Identifiable
	{
		/// <summary>
		/// 스테이터스 프로퍼티.
		/// </summary>
		public Status Status { get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Character() : base()
		{
			Status = new Status();
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