using Crockhead.Unity;
using System;
using UnityEngine;
using UnityEngine.UI;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 화면.
	/// </summary>
	public class UIView : UINode
	{
		#region INSPECTOR
		//[SerializeField] private CanvasRenderer m_CanvasRenderer;
		[SerializeField] private Image m_BackgroundImage;
		#endregion

		/// <summary>
		/// 소속 윈도우.
		/// </summary>
		private UIWindow m_Window;

		/// <summary>
		/// 소속 컨트롤러.
		/// </summary>
		private UIController m_Controller;

		/// <summary>
		/// 소속 윈도우 프로퍼티.
		/// </summary>
		public UIWindow Window { internal set => m_Window = value; get => m_Window; }

		/// <summary>
		/// 소속된 컨트롤러 프로퍼티.
		/// </summary>
		public UIController Controller => m_Controller;

		/// <summary>
		/// 백그라운드 이미지 프로퍼티.
		/// </summary>
		public Image BackgroundImage => m_BackgroundImage;

		/// <summary>
		/// 백그라운드 컬러 프로퍼티.
		/// </summary>
		public Color BackgroundImageColor
		{
			set => m_BackgroundImage.color = value;
			get => m_BackgroundImage.color;
		}

		///// <summary>
		///// 표시 여부 프로퍼티.
		///// </summary>
		//public bool IsVisible
		//{
		//	set
		//	{
		//		if (m_CanvasRenderer == null)
		//		{
		//		}
		//		else
		//		{
		//		}
		//	}
		//	get
		//	{
		//		return Canvas.enabled;
		//	}
		//}

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			if (IsDestroyed())
				return;

			//if (m_CanvasRenderer == null)
			//{
			//	m_CanvasRenderer = GetComponent<CanvasRenderer>();
			//}

			if (m_BackgroundImage == null)
			{
				m_BackgroundImage = GetOrAddComponent<Image>("Background");
				m_BackgroundImage.rectTransform.SetAsFirstSibling();
				m_BackgroundImage.color = Color.clear;
			}
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void Start()
		{
			if (Window == null)
				Window = GetComponentInParent<UIWindow>();

			base.Start();
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		/// <summary>
		/// 뷰 생성.
		/// </summary>
		public static UIView CreateView(Type viewType, RectTransform parentRectTransform)
		{
			try
			{
				if (viewType == null)
					viewType = typeof(UIView);
				if (parentRectTransform == null)
					parentRectTransform = UIManager.SharedInstance.FrontWindow.RectTransform;

				var view = (UIView)UINode.CreateNode(viewType, (Transform)parentRectTransform);
				return view;
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 애셋을 로드하여 뷰 생성.
		/// </summary>
		public static UIView CreateViewFromAsset(Type viewType, string assetPath, AssetPathType assetPathType, RectTransform parentRectTransform)
		{
			try
			{
				if (viewType == null)
					viewType = typeof(UIView);
				if (parentRectTransform == null)
					parentRectTransform = UIManager.SharedInstance.FrontWindow.RectTransform;

				var view = (UIView)UINode.CreateNodeFromAsset(viewType, assetPath, assetPathType, (Transform)parentRectTransform);
				return view;
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 뷰 생성.
		/// </summary>
		public static TUIView CreateView<TUIView>(RectTransform parentRectTransform) where TUIView : UIView
		{
			var viewType = typeof(TUIView);
			var view = (TUIView)CreateView(viewType, parentRectTransform);
			return view;
		}

		/// <summary>
		/// 애셋을 로드하여 뷰 생성.
		/// </summary>
		public static TUIView CreateViewFromAsset<TUIView>(string assetPath, AssetPathType assetPathType, RectTransform parentRectTransform) where TUIView : UIView
		{
			try
			{
				var viewType = typeof(TUIView);
				var view = (TUIView)CreateViewFromAsset(viewType, assetPath, assetPathType, parentRectTransform);
				return view;
			}
			catch
			{
				throw;
			}
		}
	}
}