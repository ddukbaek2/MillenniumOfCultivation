using Crockhead.Unity;
using System;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 뷰 바인딩 어트리뷰트.
	/// </summary>
	public class UIViewBindingAttribute : AssetPathAttribute
	{
		/// <summary>
		/// 뷰 종류.
		/// </summary>
		public Type ViewType { get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		public UIViewBindingAttribute(Type viewType, string assetPath, AssetPathType assetPathType) : base(assetPath, assetPathType)
		{
			ViewType = viewType;
		}
	}
}