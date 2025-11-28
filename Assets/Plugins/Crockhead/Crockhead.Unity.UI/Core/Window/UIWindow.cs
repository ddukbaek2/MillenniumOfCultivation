using Crockhead.Core;
using UnityEngine;
using UnityEngine.UI;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 화면 영역 단위 구분 객체.
	/// </summary>
	public class UIWindow : UINode, IUIView
	{
		#region INSPECTOR
		[SerializeField] private Canvas m_Canvas;
		[SerializeField] private CanvasScaler m_CanvasScaler;
		[SerializeField] private GraphicRaycaster m_GraphicRaycaster;
		#endregion

		/// <summary>
		/// 윈도우 목록.
		/// </summary>
		private UIWindows m_Windows;

		/// <summary>
		/// 현재 윈도우가 제출한 컨트롤러.
		/// </summary>
		private UIController m_PresentingController;

		/// <summary>
		/// 윈도우 목록 프로퍼티.
		/// </summary>
		public UIWindows Windows { internal set => m_Windows = value; get => m_Windows; }

		/// <summary>
		/// 캔버스 프로퍼티.
		/// </summary>
		public Canvas Canvas => m_Canvas;

		/// <summary>
		/// 캔버스 스케일러 프로퍼티.
		/// </summary>
		public CanvasScaler CanvasScaler => m_CanvasScaler;

		/// <summary>
		/// 그래픽 레이캐스터 프로퍼티.
		/// </summary>
		public GraphicRaycaster GraphicRaycaster => m_GraphicRaycaster;

		/// <summary>
		/// 캔버스 정렬 순서 프로퍼티.
		/// </summary>
		public int SortingOrder
		{
			set
			{
				if (Canvas == null)
					return;

				if (Canvas.sortingOrder == value)
					return;

				Canvas.sortingOrder = value;

				// 등록되지 않은 윈도우는 Windows가 null이다.
				if (Windows != null)
				{
					Windows.ForcedUpdateAllWindows();
				}
			}
			get
			{
				if (Canvas == null)
					return -1;

				return Canvas.sortingOrder;
			}
		}

		/// <summary>
		/// 표시 여부 프로퍼티.
		/// </summary>
		public bool IsVisible
		{
			set
			{
				Canvas.enabled = value;
			}
			get
			{
				return Canvas.enabled;
			}
		}

		/// <summary>
		/// 현재 윈도우가 제출한 컨트롤러 프로퍼티. (Next)
		/// </summary>
		public UIController PresentingController
		{
			set
			{
				m_PresentingController = value;
				m_PresentingController.Window = this;
			}
			get
			{
				return m_PresentingController;
			}
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			if (IsDestroyed())
				return;

			if (!Application.isPlaying)
				return;

			if (m_Canvas == null)
			{
				m_Canvas = GetOrAddComponent<Canvas>();
			}

			if (m_CanvasScaler == null)
			{
				m_CanvasScaler = GetOrAddComponent<CanvasScaler>();
			}

			if (m_GraphicRaycaster == null)
			{
				m_GraphicRaycaster = GetOrAddComponent<GraphicRaycaster>();
			}

			m_Canvas.renderMode = RenderMode.ScreenSpaceOverlay; // RenderMode.ScreenSpaceCamera
			m_Canvas.planeDistance = 100;
			m_Canvas.pixelPerfect = true;
			m_Canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.None;
			m_Canvas.vertexColorAlwaysGammaSpace = true;
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

		/// <summary>
		/// 카메라 설정.
		/// </summary>
		public void SetCamera(Camera camera)
		{
			if (camera == null)
			{
				m_Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
				m_Canvas.worldCamera = null;
			}
			else
			{
				m_Canvas.renderMode = RenderMode.ScreenSpaceCamera;
				m_Canvas.worldCamera = camera;
			}
		}

		/// <summary>
		/// 해상도 설정.
		/// </summary>
		public void SetResolution(int width, int height, bool fixedWidth = true)
		{
			var referenceResolution = new Vector2Int(width, height);
			SetResolution(referenceResolution, fixedWidth);
		}

		/// <summary>
		/// 해상도 설정.
		/// </summary>
		public void SetResolution(Vector2Int referenceResolution, bool fixedWidth = true)
		{
			m_CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			m_CanvasScaler.referenceResolution = referenceResolution;
			m_CanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
			m_CanvasScaler.matchWidthOrHeight = fixedWidth ? 0f : 1f;
			m_CanvasScaler.scaleFactor = 1f;
			m_CanvasScaler.referencePixelsPerUnit = 100;
		}

		///// <summary>
		///// 뷰 추가.
		///// </summary>
		//public void AddSubview<TUIView>() where TUIView : UIView
		//{

		//}

		/// <summary>
		/// 제출. (현재 윈도우가 제출)
		/// </summary>
		public void Present(UIController controller)
		{
			// 기존 해제.
			if (PresentingController != null)
			{
				Disposables.Dispose(PresentingController);
			}

			PresentingController = controller;
			PresentingController.LoadView();
		}
	}
}