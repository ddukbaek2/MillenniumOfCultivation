using System;
using System.Collections.Generic;


namespace Outsourcing
{
	/// <summary>
	/// UI 모델 관리 처리자.
	/// </summary>
	public class UIModelKeeper : Singleton<UIModelKeeper>
	{
		/// <summary>
		/// 모델 목록.
		/// </summary>
		private Dictionary<Type, UIModel> m_Models;

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
		/// 모델 반환.
		/// </summary>
		public TUIModel GetModel<TUIModel>(bool created = false) where TUIModel : UIModel
		{
			var type = typeof(TUIModel);
			if (m_Models.TryGetValue(type, out var model))
				return (TUIModel)model;

			if (!created)
				return null;

			model = UIScriptable.Create<TUIModel>();
			m_Models.Add(type, model);
			return (TUIModel)model;
		}
	}
}