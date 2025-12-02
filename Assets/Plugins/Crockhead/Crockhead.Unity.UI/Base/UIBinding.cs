using Crockhead.Core;
using UnityEngine;



namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 바인더.
	/// </summary>
	public class UIBinding : Disposable
	{
		private MonoBehaviour m_Parent;
		private string m_TransformPath;

		private string TransformPath;
		/// <summary>
		/// 생성됨.
		/// </summary>
		public UIBinding(MonoBehaviour parent) : base()
		{
			m_Parent = parent;
			m_TransformPath = string.Empty;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
		}
	}
}