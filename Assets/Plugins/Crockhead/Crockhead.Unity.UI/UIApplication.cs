//using System.Threading.Tasks;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.InputSystem.UI;
//using UnityEngine.Rendering.Universal;


//namespace Crockhead.Unity.UI
//{
//	/// <summary>
//	/// UI 매니저.
//	/// </summary>
//	//[AssetPath("Assets/Resources/Base/UIApplication.prefab", AssetPathType.Resources)]
//	public class UIApplication<TUIManager> : SharedComponent<TUIManager> where TUIManager : UIApplication<TUIManager>
//	{
//		#region INSEPCTOR
//		[SerializeField] private Camera m_Camera;
//		[SerializeField] private EventSystem m_EventSystem;
//		#endregion

//		/// <summary>
//		/// 윈도우 조정자.
//		/// </summary>
//		private UIWindowCoordinator m_WindowCoordinator;

//		/// <summary>
//		/// 카메라 프로퍼티.
//		/// </summary>
//		public Camera Camera => m_Camera;

//		/// <summary>
//		/// 이벤트 시스템 프로퍼티.
//		/// </summary>
//		public EventSystem EventSystem => m_EventSystem;

//		/// <summary>
//		/// 윈도우 조정자 프로퍼티.
//		/// </summary>
//		public UIWindowCoordinator WindowCoordinator => m_WindowCoordinator;

//		/// <summary>
//		/// 레이어 맨 위의 윈도우 프로퍼티.
//		/// </summary>
//		public UIWindow Top => m_WindowCoordinator.Top;

//		/// <summary>
//		/// 생성됨.
//		/// </summary>
//		protected override void OnCreate()
//		{
//			base.OnCreate();

//			// 카메라 설정.
//			if (m_Camera == null)
//			{
//				m_Camera = TransformHelper.GetOrAddComponent<Camera>(transform, "UICamera");
//			}

//			// 이벤트 시스템 설정.
//			if (m_EventSystem == null)
//			{
//				m_EventSystem = TransformHelper.GetOrAddComponent<EventSystem>(transform, "EventSystem");
//				TransformHelper.GetOrAddComponent<InputSystemUIInputModule>(m_EventSystem.transform);
//			}

//			// 카메라 바인딩.
//			BindCamera();

//			// 조정자 생성.
//			m_WindowCoordinator = new UIWindowCoordinator();
//		}

//		/// <summary>
//		/// 초기화됨.
//		/// </summary>
//		protected override void OnInitialize()
//		{
//			base.OnInitialize();

//			var window = TransformHelper.GetOrAddComponent<UIWindow>(transform, "Window");
//			m_WindowCoordinator.Register(window);
//			window.SetCamera(m_Camera);
//			window.SetResolution(new Vector2Int(1728, 1080)); //new Vector2Int(1280, 800));

//			// 이벤트 시스템 맨 아래 위치로 이동.
//			m_EventSystem.transform.SetAsLastSibling();
//		}

//		//protected override void OnCreate()
//		//{
//		//}

//		/// <summary>
//		/// 카메라 바인딩.
//		/// </summary>
//		private void BindCamera()
//		{
//			BindCamera(Camera.main);
//		}

//		/// <summary>
//		/// 카메라 바인딩.
//		/// </summary>
//		private void BindCamera(Camera camera)
//		{
//			if (m_Camera == null)
//				return;

//			//Debug.Log("[UIApplication] BindCamera()");

//			//if (Application.isPlaying)
//			//{
//			//}
//			//else
//			//{
//			//	camera = null;
//			//	Debug.Log("[UIApplication] UIEnvironment Mode.");
//			//}

//			if (camera != null)
//			{
//				var mainCameraData = camera.GetUniversalAdditionalCameraData();
//				mainCameraData.renderType = CameraRenderType.Base;

//				var uiCameraData = m_Camera.GetUniversalAdditionalCameraData();
//				uiCameraData.renderType = CameraRenderType.Overlay;
//				if (!mainCameraData.cameraStack.Contains(m_Camera))
//					mainCameraData.cameraStack.Add(m_Camera);
//			}
//			else
//			{
//				var uiCameraData = m_Camera.GetUniversalAdditionalCameraData();
//				uiCameraData.renderType = CameraRenderType.Base;
//			}
//		}

//		/// <summary>
//		/// 제출. (최상위 윈도우에 제출)
//		/// </summary>
//		public void Present(UIController controller)
//		{
//			Present(controller, Top);
//		}

//		/// <summary>
//		/// 제출. (윈도우 지정 제출)
//		/// </summary>
//		public void Present(UIController controller, UIWindow window)
//		{
//			window.Present(controller);
//		}

//		/// <summary>
//		/// 제출. (최상위 윈도우에 제출)
//		/// </summary>
//		public async Task PresentAsync(UIController controller)
//		{
//			await PresentAsync(controller, Top);
//		}

//		/// <summary>
//		/// 제출. (윈도우 지정 제출)
//		/// </summary>
//		public async Task PresentAsync(UIController controller, UIWindow window)
//		{
//			await window.PresentAsync(controller);
//		}
//	}
//}