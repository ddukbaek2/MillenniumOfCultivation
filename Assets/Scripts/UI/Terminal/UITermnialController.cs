using Crockhead.Unity;
using Crockhead.Unity.UI;
using System.Text;
using UnityEngine;


namespace UnityTerminal
{
	/// <summary>
	/// 터미널 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UITerminalView), "Assets/Resources/UI/UITerminalView.prefab", AssetPathType.Resources)]
	public class UITerminalController : UIController<UITerminalView>
	{
		/// <summary>
		/// 문자열 처리기.
		/// </summary>
		private StringBuilder m_StringBuilder;

		/// <summary>
		/// 위치.
		/// </summary>
		private int m_Position;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public UITerminalController() : base()
		{
			m_StringBuilder = new StringBuilder();
			m_Position = 0;
			UnityRuntime.Instance.Scheduler.Add(UpdateTerminal);
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			UnityRuntime.Instance.Scheduler.Remove(UpdateTerminal);
			base.OnDispose(explicitDisposing);
		}

		/// <summary>
		/// 뷰 로드됨.
		/// </summary>
		protected override void OnViewDidLoad()
		{
			base.OnViewDidLoad();	
			View.Clear();
		}

		/// <summary>
		/// 갱신.
		/// </summary>
		private void UpdateTerminal()
		{
			if (!ViewIfLoaded)
				return;

			foreach (var inputChar in Input.inputString)
			{
				switch (inputChar)
				{
					case '\b':
						{
							Backspace();
							break;
						}
					case '\n':
					case '\r':
						{
							Submit();
							break;
						}

					default:
						{
							Insert(inputChar);
							break;
						}
				}
			}
		}

		void UpdateSelectionOverlay()
		{
		}

		void UpdateCaretPosition()
		{
		}

		void ResetCaretBlinkVisible()
		{

		}

		private void Insert(char ch)
		{
			View.Add(ch.ToString());
			++m_Position;
		}

		private void Left()
		{
		
		}

		private void Right()
		{
		}

		private void Backspace()
		{
		}

		private void Submit()
		{
		}
	}
}