using Crockhead.Unity;
using Crockhead.Unity.UI;


namespace MillenniumOfCultivation.UI
{
	//public class ItemInfo
	//{
	//	public int ItemTableId;
	//}


	/// <summary>
	/// 토스트 아이템 뷰.
	/// </summary>
	[AssetPath("Assets/Resources/UI/UIToastItemView.prefab", AssetPathType.Resources)]
	public class UIToastItemView : UIItemView
	{
		#region INSPECTOR
		//[SerializeField] private Image m_LabelView;
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();
		}

		/// <summary>
		/// 아이템 설정.
		/// </summary>
		public void SetItemInfo(int itemId)
		{

		}

		public void SetItemInfo(string name)
		{
		}
	}
}