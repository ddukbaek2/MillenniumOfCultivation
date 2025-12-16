using Crockhead.Unity;
using Crockhead.Unity.UI;
using MillenniumOfCultivation.Battle;
using System.Collections.Generic;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 맵 UI 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UIMapView), "Assets/Resources/UI/UIMapView.prefab", AssetPathType.Resources)]
	public class UIMapController : UIController<UIMapView>
	{
		/// <summary>
		/// 캐릭터 목록.
		/// </summary>
		private List<GameObject> m_Characters;

		/// <summary>
		/// 카드 목록.
		/// </summary>
		private List<UICardController> m_Cards;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public UIMapController() : base()
		{
			m_Cards = new List<UICardController>();
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			base.OnDispose(explicitDisposing);
		}

		/// <summary>
		/// 뷰 로드됨.
		/// </summary>
		protected override void OnViewDidLoad()
		{
			base.OnViewDidLoad();
		}

		/// <summary>
		/// 일시정지 & 메뉴화면.
		/// </summary>
		private void OnMenu()
		{
			Debug.Log("[UIMapController] OnMenu()");

			//Present();
		}
	}
}