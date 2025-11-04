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

				var viewBindingAttribute = assetPathAttribute as UIViewBindingAttribute;
				if (viewBindingAttribute != null)
				{
					m_View = UIManager.SharedInstance.CreateViewFromAsset<UIView>(assetPath);
				}
				else
				{
					m_View = UIManager.SharedInstance.CreateViewFromAsset<UIView>(assetPath);
				}
			}
			else
			{

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