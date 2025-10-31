using Crockhead.Core;
using System.Collections.Generic;


namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 전투 주체.
	/// </summary>
	public class Battle : Disposable
	{
		/// <summary>
		/// 진행 중 여부.
		/// </summary>
		private bool m_IsStarted;

		/// <summary>
		/// 조작 주체 목록.
		/// </summary>
		private List<Controller> m_Controllers;

		/// <summary>
		/// 이벤트 스택.
		/// </summary>
		private Stack<Event> m_Stack;

		/// <summary>
		/// 컨텍스트.
		/// </summary>
		private Context m_Context;

		/// <summary>
		/// 진행 중 여부 프로퍼티.
		/// </summary>
		public bool IsStarted => m_IsStarted;

		/// <summary>
		/// 조작 주체 목록 프로퍼티.
		/// </summary>
		public IEnumerable<Controller> Controllers => m_Controllers;

		/// <summary>
		/// 컨텍스트 프로퍼티.
		/// </summary>
		public Context Context => m_Context;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Battle() : base()
		{
			m_IsStarted = false;
			m_Controllers = new List<Controller>();
			m_Stack = new Stack<Event>();
			m_Context = null;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			//foreach (var controller in m_Controllers)
			//	Disposables.Dispose(controller);
			//Disposables.Dispose(m_Context);
		}

		/// <summary>
		/// 시작.
		/// </summary>
		public void Start(PlayerController player, List<Controller> enemies)
		{
			if (m_IsStarted)
				return;

			m_IsStarted = true;

			// 전투 컨텍스트 생성.
			Disposables.Dispose(m_Context);
			m_Context = new Context(this);

			// 컨트롤러 목록 생성.
			m_Controllers.Clear();
			m_Controllers.Add(player);
			m_Controllers.AddRange(enemies);

			// 시작 이벤트.
			m_Context.InternalRaise<BattleStartEvent>();
			m_Context.InternalRaise<TurnStartEvent>();
			m_Context.InternalRaise<PhaseStartEvent>();
		}

		/// <summary>
		/// 종료.
		/// </summary>
		public void Stop(bool suspended = true)
		{
			if (!m_IsStarted)
				return;

			if (!suspended)
			{
				// 종료 이벤트.
				m_Context.InternalRaise<BattleEndEvent>();
			}

			m_IsStarted = false;
			Disposables.Dispose(m_Context);
			m_Controllers.Clear();
		}
	}
}