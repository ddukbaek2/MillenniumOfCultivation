using System;
using UnityEngine;


namespace Crockhead.Unity
{
	/// <summary>
	/// 네트워크 통신 기반 컴포넌트.
	/// </summary>
	public abstract class NetworkBehaviour : MonoBehaviour
	{
		/// <summary>
		/// 고유 식별자.
		/// </summary>
		private string m_ObjectId;

		/// <summary>
		/// 고유 식별자 프로퍼티.
		/// </summary>
		public string ObjectId => m_ObjectId;

		/// <summary>
		/// 연결 되었는지 여부 프로퍼티.
		/// </summary>
		public abstract bool IsConnected { get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected virtual void Awake()
		{
			m_ObjectId = Guid.NewGuid().ToString();
			gameObject.name = m_ObjectId;
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			GameObject.DontDestroyOnLoad(gameObject);
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected virtual void OnDestroy()
		{
		}
	}
}