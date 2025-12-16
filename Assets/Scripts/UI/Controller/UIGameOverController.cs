using Crockhead.Unity;
using Crockhead.Unity.UI;
using MillenniumOfCultivation.Battle;
using System.Collections.Generic;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 게임오버 화면 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UITitleView), "Assets/Resources/UI/UIGameOverView.prefab", AssetPathType.Resources)]
	public class UIGameOverController : UIController<UITitleView>
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public UIGameOverController() : base()
		{
		}

		/// <summary>
		/// 뷰 로드됨.
		/// </summary>
		protected override void OnViewDidLoad()
		{
			base.OnViewDidLoad();
		}

		/// <summary>
		/// 재시작 눌림.
		/// </summary>
		private void OnRestart()
		{
			//Present()
		}

		/// <summary>
		/// 타이틀 화면 가기 눌림.
		/// </summary>
		private void OnTitle()
		{
			var message = new UIMessageController();
			Present(message);
		}
	}
}