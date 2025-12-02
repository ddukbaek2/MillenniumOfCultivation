using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 화면.
	/// </summary>
	public class UIView : UINode, IUIView, IUIConstraintable
	{
		#region INSPECTOR
		//[SerializeField] private CanvasRenderer m_CanvasRenderer;
		[SerializeField] private UIImageView m_BackgroundImage;
		#endregion

		/// <summary>
		/// 소속 윈도우.
		/// </summary>
		private UIWindow m_Window;

		/// <summary>
		/// 해당 뷰의 소유 컨트롤러.
		/// </summary>
		private UIController m_Controller;

		/// <summary>
		/// 소속 윈도우 프로퍼티.
		/// </summary>
		public UIWindow Window { internal set => m_Window = value; get => m_Window; }

		/// <summary>
		/// 해당 뷰의 소유 컨트롤러 프로퍼티.
		/// </summary>
		public UIController Controller => m_Controller;

		/// <summary>
		/// 백그라운드 이미지 프로퍼티.
		/// </summary>
		public Image BackgroundImage => m_BackgroundImage;

		/// <summary>
		/// 백그라운드 컬러 프로퍼티.
		/// </summary>
		public Color BackgroundColor
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
		protected override void OnCreate()
		{
			base.OnCreate();

			// 하나의 게임 오브젝트에는 동일한 뷰 클래스는 하나만 붙어 있어야 한다.
			CheckIfOnlyAnotherViewExists();


			//if (m_CanvasRenderer == null)
			//{
			//	m_CanvasRenderer = GetComponent<CanvasRenderer>();
			//}

			if (m_BackgroundImage == null)
			{
				m_BackgroundImage = GetOrAddComponent<UIImageView>("Background");
				m_BackgroundImage.RectTransform.anchoredPosition = Vector2.zero;
				m_BackgroundImage.RectTransform.sizeDelta = Vector2.zero;
				m_BackgroundImage.RectTransform.anchorMin = Vector2.zero;
				m_BackgroundImage.RectTransform.anchorMax = Vector2.one;

				m_BackgroundImage.rectTransform.SetAsFirstSibling();
			}

			BackgroundColor = Color.clear;
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void OnInitialize()
		{
			base.OnInitialize();
			
			if (Window == null)
			{
				Window = GetComponentInParent<UIWindow>();
			}
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose()
		{
			base.OnDispose();
		}

		/// <summary>
		/// 컨트롤러 설정.
		/// </summary>
		internal void SetController(UIController controller)
		{
			m_Controller = controller;
		}

		/// <summary>
		/// 버튼 클릭 이벤트 연결.
		/// </summary>
		public void BindButtonClickEvent(string transformPath, UnityAction action)
		{
			var button = GetOrAddComponent<UIButtonView>(transformPath);
			if (button == null)
			{
				Debug.Log($"[UIView] BindButtonClickEvent({transformPath})");
				return;
			}
			button.onClick.AddListener(action);
		}

		/// <summary>
		/// 버튼 클릭 이벤트 제거.
		/// </summary>
		public void UnbindButtonClickEvent(string transformPath, UnityAction action)
		{
			var button = GetOrAddComponent<UIButtonView>(transformPath);
			if (button == null)
			{
				Debug.Log($"[UIView] UnbindButtonClickEvent({transformPath})");
				return;
			}
			button.onClick.RemoveListener(action);
		}

		/// <summary>
		/// 버튼 클릭 이벤트 전체 제거.
		/// </summary>
		public void UnbindAllButtonClickEvents(string transformPath)
		{
			var button = GetOrAddComponent<UIButtonView>(transformPath);
			if (button == null)
			{
				Debug.Log($"[UIView] UnbindAllButtonClickEvents({transformPath})");
				return;
			}
			button.onClick.RemoveAllListeners();
		}

		//public TComponent Find<TComponent>(ref IUIView view, string transformPath) where TComponent : IUIView
		//{
		//	if (view == null)
		//	{
		//		view = GetOrAddComponent("Background");
		//		//view.RectTransform.anchoredPosition = Vector2.zero;
		//		//view.RectTransform.sizeDelta = Vector2.zero;
		//		//view.RectTransform.anchorMin = Vector2.zero;
		//		//view.RectTransform.anchorMax = Vector2.one;

		//		//view.rectTransform.SetAsFirstSibling();
		//	}
		//}

		/// <summary>
		/// 부모 뷰의 렉트 트랜스폼을 반환. (부모뷰가 없다면 윈도우)
		/// </summary>
		public RectTransform GetSuperviewRectTransform()
		{
			var superview = Window.GetComponentInParent<UIView>();
			if (superview == null)
			{
				if (Window == null)
				{
					return null;
				}
				else
				{
					return Window.RectTransform;
				}
			}
			else
			{
				return superview.RectTransform;
			}
		}

		/// <summary>
		/// 현재 게임 오브젝트에 다른 뷰가 붙어있는지 확인.
		/// </summary>
		public bool CheckIfOnlyAnotherViewExists()
		{
			// 하나의 게임 오브젝트에는 동일한 뷰 클래스는 하나만 붙어 있어야 한다.
			var existViews = GetComponents<UIView>();
			foreach (var existView in existViews)
			{
				if (this == existView)
					continue;

				Debug.LogError($"[UIView] Exists Other UIView: {existView}");
				//Debug.LogError($"[UIView] Removal Old UIView: {existView}");
				//GameObject.Destroy(existView);
				return true;
			}

			return false;
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
					//parentRectTransform = UIManager.Instance.TopWindow.RectTransform;
					throw new ArgumentNullException(nameof(parentRectTransform));

				var view = (UIView)UINode.Create(viewType, (Transform)parentRectTransform);
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
					//parentRectTransform = UIManager.Instance.TopWindow.RectTransform;
					throw new ArgumentNullException(nameof(parentRectTransform));

				var view = (UIView)UINode.CreateFromAsset(viewType, assetPath, assetPathType, (Transform)parentRectTransform);
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