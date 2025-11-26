using UnityEngine;


namespace Outsourcing
{
	/// <summary>
	/// 제출자. (화면 단위)
	/// <para>뷰를 화면 단위에서 필요한 만큼 생성하고, 로컬 데이터 및 화면 로직을 제어하며, 객체들 사이의 이벤트를 관리한다.</para>
	/// <para>프레젠터가 모델과 뷰를 알고 사용하는 형태로 모델과 뷰는 자신이 가진 정보와 기능 외에는 아무것도 모른다.</para>
	/// </summary>
	//[CreateAssetMenu(fileName = "UIPresenter", menuName = "Outsourcing/UIPresenter")]
	public abstract class UIPresenter : UIScriptable
	{
		#region INSPECTOR
		[SerializeField] private UIView m_RootView;
		#endregion

		/// <summary>
		/// 뷰가 로드되었는지 여부 프로퍼티.
		/// </summary>
		public bool RootViewIfLoaded => m_RootView != null;

		/// <summary>
		/// 로드된 뷰 프로퍼티. (자동 생성하지 않음)
		/// </summary>
		public UIView LoadedRootView => m_RootView;

		/// <summary>
		/// 뷰 프로퍼티. (없을 경우 동기적 생성)
		/// </summary>
		public UIView RootView
		{
			get
			{
				if (m_RootView != null)
					return m_RootView;

				LoadRootView();
				return m_RootView;
			}
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose()
		{
			base.OnDispose();
		}

		/// <summary>
		/// 뷰 로드.
		/// </summary>
		public void LoadRootView()
		{
			if (m_RootView != null)
				return;

			//m_RootView = UIView.Create<>
			OnRootViewDidLoad();
		}

		/// <summary>
		/// 뷰 로드됨.
		/// </summary>
		protected virtual void OnRootViewDidLoad()
		{
		}

		/// <summary>
		/// 제출됨.
		/// </summary>
		protected virtual void OnPresented()
		{
		}

		/// <summary>
		/// 제출 철회됨.
		/// </summary>
		protected virtual void OnDismissed()
		{
		}

		/// <summary>
		/// 제출. (다음 프레젠터를 제출)
		/// </summary>
		public virtual void Present(UIPresenter presenter)
		{
			OnPresented();
		}

		/// <summary>
		/// 제출 철회. (자신)
		/// </summary>
		public void Dismiss()
		{
			OnDismissed();
		}
	}
}