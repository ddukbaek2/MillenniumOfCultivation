using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 화면 영역 단위 구분 객체.
	/// </summary>
	public class UIWindow : UINode
	{
		#region INSPECTOR
		[SerializeField] private Canvas m_Canvas;
		[SerializeField] private CanvasScaler m_CanvasScaler;
		[SerializeField] private GraphicRaycaster m_GraphicRaycaster;
		#endregion

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
				UIManager.SharedInstance.WindowManagement.ForcedUpdateAllWindows();
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
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			if (IsDestroyed())
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
		public void SetResolution(Vector2Int referenceResolution, bool fixedWidth = true)
		{
			m_CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			m_CanvasScaler.referenceResolution = referenceResolution;
			m_CanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
			m_CanvasScaler.matchWidthOrHeight = fixedWidth ? 0f : 1f;
			m_CanvasScaler.scaleFactor = 1f;
			m_CanvasScaler.referencePixelsPerUnit = 100;
		}

		/// <summary>
		/// 뷰 추가.
		/// </summary>
		public void AddSubview<TUIView>() where TUIView : UIView
		{

		}
	}
}