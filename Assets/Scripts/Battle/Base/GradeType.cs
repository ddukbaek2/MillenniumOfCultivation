using UnityEngine;


namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 등급 종류.
	/// </summary>
	public enum GradeType
	{
		/// <summary>
		/// 흔함. (일반)
		/// </summary>
		Common = 0,

		/// <summary>
		/// 안흔함. (고급)
		/// </summary>
		Uncommon,

		/// <summary>
		/// 희귀함. (희귀)
		/// </summary>
		Rare,

		/// <summary>
		/// 영웅. (영웅)
		/// </summary>
		Epic,
		/// <summary>
		/// 전설. (전설)
		/// </summary>
		Legendary,

		/// <summary>
		/// 신화. (신화)
		/// </summary>
		Mythic,
	}

	/// <summary>
	/// 등급 종류. (간소화)
	/// </summary>
	public enum GradeShortType
	{
		/// <summary>
		/// 흔함. (일반)
		/// </summary>
		C = GradeType.Common,

		/// <summary>
		/// 안흔함. (고급)
		/// </summary>
		U = GradeType.Uncommon,

		/// <summary>
		/// 희귀함. (희귀)
		/// </summary>
		R = GradeType.Rare,

		/// <summary>
		/// 영웅. (영웅)
		/// </summary>
		E = GradeType.Epic,
		SR = GradeType.Epic,

		/// <summary>
		/// 전설. (전설)
		/// </summary>
		L = GradeType.Legendary,
		SSR = GradeType.Legendary,

		/// <summary>
		/// 신화. (신화)
		/// </summary>
		M = GradeType.Mythic,
		UR = GradeType.Mythic,
	}


	/// <summary>
	/// 등급 유틸리티.
	/// </summary>
	public static class GradeHelper
	{
		/// <summary>
		/// 등급 컬러.
		/// </summary>
		public static Color ToColor(this GradeType type)
		{
			switch (type)
			{
				case GradeType.Common: return Color.white;
				case GradeType.Uncommon: return new Color32(3, 209, 23, 255);//Color.green;
				case GradeType.Rare: return new Color32(0, 165, 216, 255);//Color.cyan;
				case GradeType.Epic: return new Color32(237, 1, 11, 255);// Color.red;
				case GradeType.Legendary: return new Color32(208, 71, 211, 255);// Color.magenta;
				case GradeType.Mythic: return new Color32(235, 222, 10, 255);// Color.yellow;
				default:
					return Color.white;
			}
		}

		/// <summary>
		/// 간소화 등급 컬러.
		/// </summary>
		public static Color ToColor(this GradeShortType type)
		{
			var gradeType = (GradeType)type;
			return GradeHelper.ToColor(gradeType);
		}
	}
}