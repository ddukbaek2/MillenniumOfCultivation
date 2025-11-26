using System.Collections.Generic;


namespace Outsourcing
{
	/// <summary>
	/// 테스트 프레젠터.
	/// </summary>
	public class UITestPresenter : UIPresenter
	{
		private List<UITestItemView> m_ItemViews;

		protected override void OnCreate()
		{
			base.OnCreate();

			m_ItemViews = new List<UITestItemView>();
		}

		/// <summary>
		/// 뷰 생성.
		/// </summary>
		public void BuildAllViews()
		{
		}
	}
}