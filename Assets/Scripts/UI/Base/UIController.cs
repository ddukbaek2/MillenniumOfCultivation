using Crockhead.Core;
using Crockhead.Unity;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 뷰 컨트롤러.
	/// </summary>
	public class UIController : Disposable
	{
		/// <summary>
		/// 뷰.
		/// </summary>
		private UIView m_View;

		/// <summary>
		/// 뷰 프로퍼티.
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
		/// 뷰 로드 여부 프로퍼티.
		/// </summary>
		public bool ViewIfLoaded => m_View != null;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public UIController() : base()
		{
			m_View = null;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			//base.OnDispose(explicitDisposing);
		}

		/// <summary>
		/// 뷰 로드.
		/// </summary>
		public void LoadView()
		{
			if (ViewIfLoaded)
				return;

			var type = GetType();
			if (Reflections.TryGetAttribute<AssetPathAttribute>(type, out var assetPathAttribute))
			{
				var assetPath = assetPathAttribute.Value;

				// 바인딩일 경우.
				var viewBindingAttribute = assetPathAttribute as UIViewBindingAttribute;
				if (viewBindingAttribute != null)
				{
					// 리소스에 존재하는 애셋을 불러와 지정 뷰 생성.
					// 지정 뷰 클래스가 이미 애셋에 부착되어 있을 경우 해당 뷰 클래스를 사용.
					m_View = UIManager.SharedInstance.CreateViewFromAsset(assetPath, viewBindingAttribute.ViewType);
				}
				else
				{
					// 리소스에 존재하는 애셋을 불러와 기본 뷰 생성.
					// 대상 뷰 클래스가 이미 애셋에 부착되어 있을 경우 해당 뷰 클래스를 사용. 
					m_View = UIManager.SharedInstance.CreateViewFromAsset<UIView>(assetPath);
				}
			}
			else
			{
				// 기본 뷰 생성.
				m_View = UIManager.SharedInstance.CreateView<UIView>();
			}

			OnViewDidLoad();
		}

		/// <summary>
		/// 뷰 로드됨.
		/// </summary>
		protected virtual void OnViewDidLoad()
		{
		}
	}
}