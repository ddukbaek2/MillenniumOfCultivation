using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering.Universal;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// UI 매니저.
	/// </summary>
	//[AssetPath("Assets/Resources/Base/UIManager.prefab", AssetPathType.Resources)]
	public class UIManager<TUIManager> : SharedComponent<TUIManager> where TUIManager : UIManager<TUIManager>
	{
		#region INSEPCTOR
		[SerializeField] private Camera m_Camera;
		[SerializeField] private EventSystem m_EventSystem;
		#endregion

		/// <summary>
		/// 윈도우 목록 관리.
		/// </summary>
		private UIWindows m_Windows;

		/// <summary>
		/// 카메라 프로퍼티.
		/// </summary>
		public Camera Camera => m_Camera;

		/// <summary>
		/// 이벤트 시스템 프로퍼티.
		/// </summary>
		public EventSystem EventSystem => m_EventSystem;

		/// <summary>
		/// 윈도우 관리 프로퍼티.
		/// </summary>
		public UIWindows Windows => m_Windows;

		/// <summary>
		/// 최상위 윈도우 프로퍼티.
		/// </summary>
		public UIWindow TopWindow => m_Windows.TopWindow;

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
			Bind();

			// 윈도우 찾아보고 없으면 생성해서 등록.
			m_Windows = new UIWindows();
			var window = TransformHelper.GetOrAddComponent<UIWindow>(transform, "Window");
			window.SetCamera(m_Camera);
			window.SetResolution(new Vector2Int(1280, 800));
			m_Windows.Register(window);

			// 이벤트 시스템 맨 아래 위치로 이동.
			m_EventSystem.transform.SetAsLastSibling();
		}

		/// <summary>
		/// 카메라 바인딩.
		/// </summary>
		private void Bind()
		{
			Bind(Camera.main);
		}

		/// <summary>
		/// 카메라 바인딩.
		/// </summary>
		private void Bind(Camera camera)
		{
			if (m_Camera == null)
				return;

			Debug.Log("[UIManager] Bind()");

			if (Application.isPlaying)
			{
			}
			else
			{
				camera = null;
				Debug.Log("[UIManager] UIEnvironment Mode.");
			}

			if (camera != null)
			{
				var mainCameraData = camera.GetUniversalAdditionalCameraData();
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
		/// 제출. (최상위 윈도우에 제출)
		/// </summary>
		public void Present(UIController controller)
		{
			Present(controller, TopWindow);
		}

		/// <summary>
		/// 제출. (윈도우 지정 제출)
		/// </summary>
		public void Present(UIController controller, UIWindow window)
		{
			window.Present(controller);
		}
	}
}