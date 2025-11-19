using Crockhead.Core;
using System.Collections.Generic;
using System.Linq;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 윈도우 관리.
	/// </summary>
	public class UIWindowManager : Disposable
	{
		/// <summary>
		/// 윈도우 목록.
		/// </summary>
		private List<UIWindow> m_Windows;

		/// <summary>
		/// 윈도우 갯수.
		/// </summary>
		public int Count => m_Windows.Count;

		/// <summary>
		/// 윈도우 목록 프로퍼티.
		/// </summary>
		public IEnumerable<UIWindow> Windows => m_Windows;

		/// <summary>
		/// 메인 윈도우 프로퍼티.
		/// </summary>
		public UIWindow FrontWindow => m_Windows.LastOrDefault();

		/// <summary>
		/// 생성됨.
		/// </summary>
		public UIWindowManager() : base()
		{
			m_Windows = new List<UIWindow>();
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
		}


		/// <summary>
		/// 윈도우 전체 갱신.
		/// </summary>
		public void ForcedUpdateAllWindows()
		{
			static int CanvasComapreBySortingOrder(UIWindow left, UIWindow right)
			{
				var compare = left.SortingOrder.CompareTo(right.SortingOrder);
				return compare;
			}

			if (m_Windows.Count < 2)
				return;

			// 리스트 순서 정렬.
			m_Windows.Sort(CanvasComapreBySortingOrder);

			// 하이어라키 순서 정렬.
			for (var i = 0; i < m_Windows.Count; ++i)
			{
				m_Windows[i].transform.SetSiblingIndex(i);
			}
		}

		/// <summary>
		/// 윈도우 등록.
		/// </summary>
		public bool Register(UIWindow window)
		{
			if (window == null)
				return false;

			if (m_Windows.Contains(window))
				return false;

			var frontWindow = m_Windows.LastOrDefault();
			if (frontWindow != null)
			{
				window.SortingOrder = frontWindow.SortingOrder + 1;
			}
			else
			{
				window.SortingOrder = 1;
			}

			m_Windows.Add(window);

			ForcedUpdateAllWindows();
			return true;
		}

		/// <summary>
		/// 윈도우 등록 해제.
		/// </summary>
		public bool Unregister(UIWindow window)
		{
			if (window == null)
				return false;

			if (!m_Windows.Contains(window))
				return false;

			var removed = m_Windows.Remove(window);
			return removed;
		}

		/// <summary>
		/// 윈도우 등록 여부.
		/// </summary>
		public bool IsRegistered(UIWindow window)
		{
			if (window == null)
				return false;

			var contains = m_Windows.Contains(window);
			return contains;
		}

		/// <summary>
		/// 윈도우 반환.
		/// </summary>
		public UIWindow GetWindow(int index)
		{
			return m_Windows[index];
		}
	}
}