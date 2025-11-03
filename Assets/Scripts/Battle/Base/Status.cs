using System.Collections.Generic;


namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 스테이터스.
	/// </summary>
	public class Status : Identifiable
	{
		/// <summary>
		/// 스탯 프로퍼티.
		/// </summary>
		public SortedDictionary<string, float> Stat { get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Status() : base()
		{
			Stat = new SortedDictionary<string, float>();
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