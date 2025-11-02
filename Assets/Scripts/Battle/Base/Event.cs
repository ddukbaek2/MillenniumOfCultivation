using Crockhead.Core;


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
		/// 완료 여부.
		/// </summary>
		private bool m_IsCompleted;

		/// <summary>
		/// 시작 여부 프로퍼티.
		/// </summary>
		public bool IsStarted => m_IsStarted;

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
		}

		/// <summary>
		/// 이벤트 처리됨.
		/// </summary>
		protected virtual void OnProcess(Context context)
		{
		}

		/// <summary>
		/// 이벤트 전환됨.
		/// </summary>
		protected virtual void OnTransition(Context context, Event previous, Event next)
		{
		}

		/// <summary>
		/// 이벤트 완료됨.
		/// </summary>
		protected virtual void OnComplete(Context context)
		{
		}

		/// <summary>
		/// 시작.
		/// </summary>
		public void Start(Context context)
		{
			if (m_IsStarted || m_IsCompleted)
				return;

			m_IsStarted = true;
			OnStart(context);
		}

		/// <summary>
		/// 처리.
		/// </summary>
		public void Process(Context context)
		{
			if (m_IsStarted || m_IsCompleted)
				return;

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
			if (!m_IsStarted || m_IsCompleted)
				return;

			m_IsCompleted = true;
			OnComplete(context);
		}
	}
}