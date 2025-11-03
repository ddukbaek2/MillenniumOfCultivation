using Crockhead.Core;
using UnityEngine;


namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 이벤트.
	/// </summary>
	public abstract class Event : Disposable
	{
		/// <summary>
		/// 시작 여부.
		/// </summary>
		private bool m_IsStarted;

		/// <summary>
		/// 처리 중 여부.
		/// </summary>
		private bool m_IsProcessed;

		/// <summary>
		/// 완료 여부.
		/// </summary>
		private bool m_IsCompleted;

		/// <summary>
		/// 시작 여부 프로퍼티.
		/// </summary>
		public bool IsStarted => m_IsStarted;

		/// <summary>
		/// 처리 중 여부 프로퍼티.
		/// </summary>
		public bool IsProcessed => m_IsProcessed;

		/// <summary>
		/// 완료 여부 프로퍼티.
		/// </summary>
		public bool IsCompleted => m_IsCompleted;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Event() : base()
		{
			m_IsStarted = false;
			m_IsProcessed = false;
			m_IsCompleted = false;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			//base.OnDispose(explicitDisposing);
		}

		/// <summary>
		/// 이벤트 시작됨.
		/// </summary>
		protected virtual void OnStart(Context context)
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] OnStart()");
		}

		/// <summary>
		/// 이벤트 처리됨.
		/// </summary>
		protected virtual void OnProcess(Context context)
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] OnProcess()");
		}

		/// <summary>
		/// 이벤트 전환됨.
		/// </summary>
		protected virtual void OnTransition(Context context, Event previous, Event next)
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] OnTransition()");
		}

		/// <summary>
		/// 이벤트 완료됨.
		/// </summary>
		protected virtual void OnComplete(Context context)
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] OnComplete()");
		}

		/// <summary>
		/// 시작.
		/// </summary>
		public void Start(Context context)
		{
			if (m_IsStarted)
				return;

			m_IsStarted = true;
			OnStart(context);
		}

		/// <summary>
		/// 처리.
		/// </summary>
		public void Process(Context context)
		{
			if (!m_IsStarted || m_IsProcessed)
				return;

			m_IsProcessed = true;
			OnProcess(context);
		}

		/// <summary>
		/// 전환.
		/// </summary>
		public void Transition(Context context, Event previous, Event next)
		{
			if (previous == next || next == null)
				return;

			OnTransition(context, previous, next);
		}

		/// <summary>
		/// 완료.
		/// </summary>
		public void Complete(Context context)
		{
			if (!m_IsStarted || !m_IsProcessed || m_IsCompleted)
				return;

			m_IsCompleted = true;
			OnComplete(context);
		}
	}
}