using Crockhead.Core;
using Crockhead.Unity.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 입력 매니저.
	/// </summary>
	public class InputManager : SharedClass<InputManager>
	{
		private InputActions m_InputActions;

		public InputType InputType
		{
			get
			{
				return InputType.Gamepad;
			}
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{	
			base.OnCreate();

			m_InputActions = new InputActions();
			m_InputActions.Enable();
			m_InputActions.Player.A.started += OnEvent;
			m_InputActions.Player.A.canceled += OnEvent;
			m_InputActions.Player.A.performed += OnEvent;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose()
		{
			m_InputActions.Dispose();

			base.OnDispose();
		}

		/// <summary>
		/// UI 포커스 셋업.
		/// </summary>
		public void FocusUI(IUIView view)
		{
			EventSystem.current.SetSelectedGameObject(view.RectTransform.gameObject);
		}

		/// <summary>
		/// 콜백.
		/// </summary>
		private void OnEvent(InputAction.CallbackContext context)
		{
			Debug.Log($"[InputManager] OnEvent(): context.action.name={context.action.name}, context.phase={context.phase}");

			switch (context.phase)
			{
				case InputActionPhase.Started:
					{
						break;
					}

				case InputActionPhase.Canceled:
					{
						break;
					}

				case InputActionPhase.Performed:
					{
						break;
					}
			}
		}
	}
}