using Crockhead.Core;
using Crockhead.Unity;
using MillenniumOfCultivation.UI;
using System;
using UnityEngine;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 뷰 컨트롤러.
	/// </summary>
	public class UIController : Disposable
	{
		/// <summary>
		/// 소속 윈도우.
		/// </summary>
		private UIWindow m_Window;

		/// <summary>
		/// 소유한 뷰.
		/// </summary>
		private UIView m_View;

		/// <summary>
		/// 소속 윈도우 프로퍼티.
		/// </summary>
		public UIWindow Window { internal set => m_Window = value; get => m_Window; }

		/// <summary>
		/// 소유한 뷰 프로퍼티. (자동생성)
		/// </summary>
		public UIView View
		{
			get
			{
				LoadView();
				return m_View;
			}
		}

		/// <summary>
		/// 소유한 뷰 프로퍼티. (뷰 없으면 null)
		/// </summary>
		public UIView LoadedView => m_View;

		/// <summary>
		/// 뷰 로드 여부 프로퍼티.
		/// </summary>
		public bool ViewIfLoaded => m_View != null;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public UIController() : base()
		{
			m_Window = null;
			m_View = null;
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		public UIController(UIWindow window) : this()
		{
			m_Window = window;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			m_Window = null;

			if (m_View != null)
			{
				GameObject.Destroy(m_View);
				m_View = null;
			}

			Debug.Log("[UIController] OnDispose()");

			//base.OnDispose(explicitDisposing);
		}

		/// <summary>
		/// 뷰 로드.
		/// </summary>
		public void LoadView()
		{
			try
			{
				if (ViewIfLoaded)
					return;

				// 현재 윈도우가 없을 경우.
				if (m_Window == null)
					m_Window = UIManager.Instance.FrontWindow;

				// 뷰 로드 직전 정보를 수집하고, 정보에 따른 뷰를 생성.
				var viewConfiguration = OnViewWillLoad(typeof(UIView));
				var viewType = viewConfiguration.ViewType;
				var assetPath = viewConfiguration.AssetPath;
				var assetPathType = viewConfiguration.AssetPathType;

				// 경로가 없다면 생성.
				if (string.IsNullOrWhiteSpace(assetPath))
				{
					m_View = UIView.CreateView(viewType, m_Window.RectTransform);
				}
				// 경로가 있다면 로드.
				else
				{
					m_View = UIView.CreateViewFromAsset(viewType, assetPath, assetPathType, m_Window.RectTransform);
				}

				OnViewDidLoad();
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				throw;
			}
		}

		///// <summary>
		///// 뷰 로드. (비동기)
		///// </summary>
		//public async TaskFactory LoadViewAsync()
		//{
		//	if (ViewIfLoaded)
		//		await TaskFactory.CompletedTask;

		//	//if (m_Window == null)
		//	//	m_Window = UIManager.Instance.FrontWindow;

		//	//var controllerType = GetType();
		//	//var viewType = typeof(UIView);
		//	//if (Reflections.TryGetAttribute<AssetPathAttribute>(controllerType, out var assetPathAttribute))
		//	//{
		//	//	var assetPath = assetPathAttribute.Value;

		//	//	// 바인딩일 경우.
		//	//	// 리소스에 존재하는 애셋을 불러와 기본 뷰 생성.
		//	//	// 대상 뷰 클래스가 이미 애셋에 부착되어 있을 경우 해당 뷰 클래스를 사용. 
		//	//	var viewBindingAttribute = assetPathAttribute as UIViewBindingAttribute;
		//	//	if (viewBindingAttribute != null)
		//	//	{
		//	//		viewType = viewBindingAttribute.ViewType;
		//	//	}

		//	//	m_View = UIView.CreateViewFromAsset(viewType, assetPath, AssetPathType.Resources, m_Window.RectTransform);
		//	//}
		//	//else
		//	//{
		//	//	// 기본 뷰 생성.
		//	//	m_View = UIView.CreateView(viewType, m_Window.RectTransform);
		//	//}

		//	OnViewDidLoad();
		//}

		/// <summary>
		/// 뷰 로드 직전 호출됨.
		/// <para>이를 상속 받아서 뷰 설정을 각 상속 뷰 별로 커스텀하면 특성 없이 각 뷰 마다 연결될 애셋을 개별 지정 가능.</para>
		/// </summary>
		protected virtual (Type ViewType, string AssetPath, AssetPathType AssetPathType) OnViewWillLoad(Type viewType)
		{
			if (viewType == null)
				throw new ArgumentNullException(nameof(viewType));

			var controllerType = GetType();
			var assetPathValue = string.Empty;
			var assetPathType = AssetPathType.Resources;

			// 컨트롤러에 부착된 애셋 경로 특성 사용.
			// AssetPathAttribute 혹은 ViewBindingAttribute 가 부착 되어있다는 전제. (강제사항은 아님)
			if (Reflections.TryGetAttribute<AssetPathAttribute>(controllerType, out var assetPathAttribute))
			{
				assetPathValue = assetPathAttribute.Value;
				assetPathType = assetPathAttribute.Type;

				// 뷰 바인딩 특성 사용.
				var viewBindingAttribute = assetPathAttribute as UIViewBindingAttribute;
				if (viewBindingAttribute != null)
				{
					// 지정 뷰 설정.
					viewType = viewBindingAttribute.ViewType;
				}
			}
			// 뷰에 부착된 애셋 경로 특성 사용.
			// 뷰에서 ViewBindingAttribute를 사용하는 것은 모순.
			else if (Reflections.TryGetAttribute<AssetPathAttribute>(viewType, out assetPathAttribute))
			{
				assetPathValue = assetPathAttribute.Value;
				assetPathType = assetPathAttribute.Type;
			}

			return (viewType, assetPathValue, assetPathType);
		}

		/// <summary>
		/// 뷰 로드됨.
		/// </summary>
		protected virtual void OnViewDidLoad()
		{
		}

		/// <summary>
		/// 뷰 나타나기 직전 호출됨.
		/// </summary>
		protected virtual void OnViewWillApear()
		{
		}

		/// <summary>
		/// 뷰 나타난 직후 호출됨.
		/// </summary>
		protected virtual void OnViewDidAppear()
		{
		}

		/// <summary>
		/// 뷰 사라지기 직전 호출됨.
		/// </summary>
		protected virtual void OnViewWillDisapear()
		{
		}

		/// <summary>
		/// 뷰 사라진 직후 호출됨.
		/// </summary>
		protected virtual void OnViewDidDisapear()
		{
		}

		/// <summary>
		/// 뷰 반환.
		/// </summary>
		public TUIView GetView<TUIView>() where TUIView : UIView
		{
			return (TUIView)View;
		}
	}
}