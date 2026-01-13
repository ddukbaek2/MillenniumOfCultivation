using Crockhead.Unity;
using Crockhead.Unity.UI;
using MillenniumOfCultivation.Battle;
using System.Collections.Generic;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	public class UIViewController<TUIView> : UIController<TUIView> where TUIView : UIView
	{
		/// <summary>
		/// 소유한 뷰.
		/// </summary>
		private TUIView m_View;

		/// <summary>
		/// 소유한 뷰 프로퍼티. (자동생성)
		/// </summary>
		public new TUIView View => m_View;

		/// <summary>
		/// 소유한 뷰 프로퍼티. (뷰 없으면 null)
		/// </summary>
		public new TUIView LoadedView => m_View;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public UIViewController(TUIView view) : base()
		{
			m_View = view;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			m_View = null;

			base.OnDispose(explicitDisposing);
		}
	}

	/// <summary>
	/// 맵 UI 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UIMapView), "Assets/Resources/UI/UIMapView.prefab", AssetPathType.Resources)]
	public class UIMapController : UIController<UIMapView>
	{
		/// <summary>
		/// 카드 목록.
		/// </summary>
		private List<UIMapItemView> m_Items;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public UIMapController() : base()
		{
			m_Items = new List<UIMapItemView>();
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

			DispatchQueue.Foreground.RunAsync(OnPrepare);
		}

		/// <summary>
		/// 등장 완료.
		/// </summary>
		protected override void OnViewDidAppear()
		{
			base.OnViewDidAppear();
		}
		
		/// <summary>
		/// 준비.
		/// </summary>
		private void OnPrepare()
		{
			// 더미 항목 생성.
			var cardCount = 10;
			for (var i = 0; i < cardCount; ++i)
			{
				var item = UIView.CreateFromAttribute<UIMapItemView>();
				m_Items.Add(item);

				item.RectTransform.SetParent(View.RectTransform, true);
				item.RectTransform.anchorMin = Vector2.one * 0.5f;
				item.RectTransform.anchorMax = Vector2.one * 0.5f;
				item.RectTransform.pivot = Vector2.one * 0.5f;
				item.RectTransform.sizeDelta = new Vector2(240f, 400f);
			}
		}

		/// <summary>
		/// 일시정지 & 메뉴화면.
		/// </summary>
		private void OnMenu()
		{
			Debug.Log("[UIMapController] OnMenu()");

			//PresentAsync();
		}
	}
}