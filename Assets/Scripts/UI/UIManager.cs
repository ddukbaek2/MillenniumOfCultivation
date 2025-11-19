using Crockhead.Core;
using Crockhead.Unity;
using Crockhead.Unity.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering.Universal;
using UIController = Crockhead.Unity.UI.UIController;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// UI 매니저.
	/// </summary>
	[AssetPath("Assets/Resources/Base/UIManager.prefab", AssetPathType.Resources)]
	public class UIManager : SharedComponent<UIManager>
	{
		#region INSEPCTOR
		[SerializeField] private Camera m_Camera;
		[SerializeField] private EventSystem m_EventSystem;
		#endregion

		/// <summary>
		/// 윈도우 목록 관리.
		/// </summary>
		private UIWindowManager m_WindowManager;

		/// <summary>
		/// 트랜지션 윈도우. (별도 관리)
		/// </summary>
		private UIWindow m_TransitionWindow;

		/// <summary>
		/// 시작 컨트롤러.
		/// </summary>
		private UIController m_StartController;

		/// <summary>
		/// 트랜지션 컨트롤러.
		/// </summary>
		private TransitionController m_TransitionController;

		/// <summary>
		/// 윈도우 관리 프로퍼티.
		/// </summary>
		public UIWindowManager WindowManager => m_WindowManager;

		/// <summary>
		/// 메인 윈도우 프로퍼티.
		/// </summary>
		public UIWindow FrontWindow => m_WindowManager.FrontWindow;

		/// <summary>
		/// 트랜지션 컨트롤러 프로퍼티.
		/// </summary>
		public TransitionController TransitionController;

		/// <summary>
		/// 시작 컨트롤러 프로퍼티.
		/// </summary>
		public UIController StartController => m_StartController;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			if (IsDestroyed())
				return;

			// 카메라 설정.
			if (m_Camera == null)
			{
				m_Camera = TransformHelper.GetOrAddComponent<Camera>(transform, "UICamera");
			}

			// 이벤트 시스템 설정.
			if (m_EventSystem == null)
			{
				m_EventSystem = TransformHelper.GetOrAddComponent<EventSystem>(transform, "EventSystem");
				m_EventSystem.AddComponent<InputSystemUIInputModule>();
			}

			// 카메라 바인딩.
			BindingUICamera();

			// 윈도우 찾아보고 없으면 생성해서 등록.
			m_WindowManager = new UIWindowManager();
			var window = TransformHelper.GetOrAddComponent<UIWindow>(transform, "Window");
			window.SetCamera(m_Camera);
			window.SetResolution(new Vector2Int(1280, 800));
			m_WindowManager.Register(window);

			// 트랜지션용 오버레이 윈도우. (등록하지 않음)
			m_TransitionWindow = TransformHelper.GetOrAddComponent<UIWindow>(transform, "TransitionWindow");
			m_TransitionWindow.SetCamera(m_Camera);
			m_TransitionWindow.SortingOrder = 1000;
			m_TransitionWindow.SetResolution(new Vector2Int(1280, 800));
			m_TransitionController = new TransitionController(m_TransitionWindow);
			m_TransitionController.LoadView();

			m_EventSystem.transform.SetAsLastSibling();
		}

		/// <summary>
		/// 카메라 바인딩.
		/// </summary>
		private void BindingUICamera()
		{
			if (m_Camera == null)
				return;

			Debug.Log("[UIManager] BindingUICamera()");

			var mainCamera = Camera.main;
			if (Application.isPlaying)
			{
			}
			else
			{
				mainCamera = null;
				Debug.Log("[UIManager] UIEnvironment Mode.");
			}

			if (mainCamera != null)
			{
				var mainCameraData = mainCamera.GetUniversalAdditionalCameraData();
				mainCameraData.renderType = CameraRenderType.Base;

				var uiCameraData = m_Camera.GetUniversalAdditionalCameraData();
				uiCameraData.renderType = CameraRenderType.Overlay;
				if (!mainCameraData.cameraStack.Contains(m_Camera))
					mainCameraData.cameraStack.Add(m_Camera);
			}
			else
			{
				var uiCameraData = m_Camera.GetUniversalAdditionalCameraData();
				uiCameraData.renderType = CameraRenderType.Base;
			}
		}

		/// <summary>
		/// 시작.
		/// </summary>
		public void Run(UIController controller)
		{
			if (m_StartController != null)
			{
				Disposables.Dispose(m_StartController);
			}

			m_StartController = controller;

			if (m_StartController.Window == null)
				m_StartController.Window = FrontWindow;
			m_StartController.LoadView();

			//var battle = new BattleController();
			//battle.LoadView(); // battle.View
		}
	}
}