using Crockhead.Unity;
using System;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 뷰 바인딩 어트리뷰트.
	/// </summary>
	public class UIViewBindingAttribute : AssetPathAttribute
	{
		public Type ViewType { get; }
		public UIViewBindingAttribute(Type viewType, string assetPath, AssetPathType assetPathType) : base(assetPath, assetPathType)
		{
			ViewType = viewType;
		}
	}
}