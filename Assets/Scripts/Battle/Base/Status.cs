using Crockhead.Core;
using System;
using System.Collections.Generic;


namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 스테이터스.
	/// </summary>
	public class Status : Identifiable, ICloneable
	{
		/// <summary>
		/// 스탯 목록 프로퍼티.
		/// </summary>
		public SortedDictionary<StatType, float> Stats { get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Status() : base()
		{
			Stats = new SortedDictionary<StatType, float>();

			// 값 등록.
			var statTypes = Reflections.GetEnumValues<StatType>();
			foreach (var statType in statTypes)
			{
				if (Stats.ContainsKey(statType))
					continue;

				Stats.Add(statType, 0f);
			}
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Status(IEnumerable<KeyValuePair<StatType, float>> enumerable) : this()
		{
			foreach (var pair in enumerable)
			{
				Stats[pair.Key] = pair.Value;
			}
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			base.OnDispose(explicitDisposing);
		}

		/// <summary>
		/// 복제.
		/// </summary>
		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// 복제.
		/// </summary>
		public Status Clone()
		{
			return new Status(Stats);
		}
	}
}