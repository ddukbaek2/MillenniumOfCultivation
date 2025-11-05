using Crockhead.Unity;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// UI 매니저.
	/// </summary>
	public class UIManager : SharedComponent<UIManager>
	{
		#region INSEPCTOR
		/// <summary>
		/// 카메라.
		/// </summary>
		[SerializeField] private Camera m_Camera;

		/// <summary>
		/// 캔버스.
		/// </summary>
		[SerializeField] private Canvas m_Canvas;
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			if (m_Camera == null)
			{
				m_Camera = TransformHelper.GetOrAddComponent<Camera>(transform, "Camera");
			}

			if (m_Canvas == null)
			{
				m_Canvas = TransformHelper.GetOrAddComponent<Canvas>(transform, "Canvas");
			}
			
			BindingUICamera();
		}

		/// <summary>
		/// 카메라 바인딩.
		/// </summary>
		private void BindingUICamera()
		{
			if (m_Camera == null)
				return;

			if (Camera.main != null)
			{
				var mainCameraData = Camera.main.GetUniversalAdditionalCameraData();
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
		/// 뷰 생성.
		/// </summary>
		public UIView CreateView(Type viewType = null, RectTransform parentRectTransform = null)
		{
			if (viewType == null)
				viewType = typeof(UIView);
			if (parentRectTransform == null)
				parentRectTransform = m_Canvas.GetComponent<RectTransform>();

			var obj = new GameObject(viewType.Name);
			var view = (UIView)obj.AddComponent(viewType);
			view.RectTransform.SetParent(parentRectTransform, true);
			return view;
		}

		/// <summary>
		/// 뷰 생성.
		/// </summary>
		public UIView CreateViewFromAsset(string assetPath, Type viewType = null, RectTransform parentRectTransform = null)
		{
			try
			{
				if (viewType == null)
					viewType = typeof(UIView);
				if (parentRectTransform == null)
					parentRectTransform = m_Canvas.GetComponent<RectTransform>();

				using var assetReader = new AssetReader<GameObject>(assetPath, AssetPathType.Resources);
				var operation = assetReader.Read();
				if (operation.IsSucceeded)
				{
					var asset = operation.Result;
					var obj = GameObject.Instantiate<GameObject>(asset);
					obj.name = viewType.Name;
					var view = (UIView)obj.GetOrAddComponent(viewType);
					view.RectTransform.SetParent(parentRectTransform, true);
					return view;
				}
				else
				{
					Debug.LogException(operation.Exception);
					throw operation.Exception;
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 뷰 생성.
		/// </summary>
		public TUIView CreateView<TUIView>(RectTransform parentRectTransform = null) where TUIView : UIView
		{
			var viewType = typeof(TUIView);
			var view = (TUIView)CreateView(viewType, parentRectTransform);
			return view;
		}

		/// <summary>
		/// 뷰 생성.
		/// </summary>
		public TUIView CreateViewFromAsset<TUIView>(string assetPath, RectTransform parentRectTransform = null) where TUIView : UIView
		{
			try
			{
				var viewType = typeof(TUIView);
				var view = (TUIView)CreateViewFromAsset(assetPath, viewType, parentRectTransform);
				return view;
			}
			catch
			{
				throw;
			}
		}
	}
}