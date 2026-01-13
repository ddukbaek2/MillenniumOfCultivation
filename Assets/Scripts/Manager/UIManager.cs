using Crockhead.Unity;
using Crockhead.Unity.UI;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// UI 매니저.
	/// </summary>
	[AssetPath("Assets/Resources/Base/UIManager.prefab", AssetPathType.Resources)]
	public class UIManager : UIApplication<UIManager>
	{
		/// <summary>
		/// 기본 윈도우.
		/// </summary>
		private UIWindow m_Window;

		/// <summary>
		/// 트랜지션 윈도우.
		/// </summary>
		private UIWindow m_TransitionWindow;

		/// <summary>
		/// 트랜지션 컨트롤러.
		/// </summary>
		private UITransitionController m_TransitionController;

		/// <summary>
		/// 트랜지션 컨트롤러 프로퍼티.
		/// </summary>
		public UITransitionController TransitionController;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			// 멀티터치 잠금.
			Input.multiTouchEnabled = false;
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void OnInitialize()
		{
			base.OnInitialize();

			// 기본 윈도우 받아오기.
			m_Window = Top;

			// 트랜지션용 오버레이 윈도우. (등록하지 않음)
			m_TransitionWindow = TransformHelper.GetOrAddComponent<UIWindow>(transform, "TransitionWindow");
			m_TransitionWindow.SetCamera(Camera);
			m_TransitionWindow.SortingOrder = 1000;
			m_TransitionWindow.SetResolution(new Vector2Int(1728, 1080));
			m_TransitionController = new UITransitionController();
			//m_TransitionController.Window = m_TransitionWindow;
			//var view = m_TransitionController.View;
			m_TransitionWindow.PresentAsync(m_TransitionController);
			m_TransitionWindow.GraphicRaycaster.enabled = false;
		}

		/// <summary>
		/// 명령 실행.
		/// </summary>
		public void ExecuteCommand(string command)
		{
		}

		/// <summary>
		/// 갱신됨.
		/// </summary>
		private void Update()
		{
			if (m_Window == null)
				return;

			var lastController = m_Window.PresentationCoordinator.Last;
			if (lastController is not UIBattleController)
				return;

			OnUpdateBattle();
		}

		/// <summary>
		/// 임시 전투 업데이트.
		/// </summary>
		private void OnUpdateBattle()
		{
			Debug.Log("[UIManager] OnUpdateBattle()");
		}
	}
}