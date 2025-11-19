using Crockhead.Unity;
using Crockhead.Unity.UI;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 인벤토리 뷰.
	/// </summary>
	[AssetPath("Assets/Resources/UI/UIInventoryView.prefab", AssetPathType.Resources)]
	public class UIInventoryView : UIPopupView
	{
		#region INSPECTOR
		//[SerializeField] private Image m_LogoImage;
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			if (IsDestroyed())
				return;

		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void Start()
		{
			base.Start();
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected override void OnDestroy()
		{
			base.OnDestroy();
		}
	}
}