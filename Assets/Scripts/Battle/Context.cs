using Crockhead.Core;


namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 전투 문맥.
	/// </summary>
	public class Context : Disposable
	{
		/// <summary>
		/// 전투 주체.
		/// </summary>
		private Battle m_Battle;

		/// <summary>
		/// 현재 컨트롤러.
		/// </summary>
		private Controller m_Controller;

		/// <summary>
		/// 현재 이벤트.
		/// </summary>
		private Event m_Event;

		/// <summary>
		/// 턴 횟수.
		/// </summary>
		private int m_Turn;

		/// <summary>
		/// 전투 상태.
		/// </summary>
		private Phase m_Phase;

		/// <summary>
		/// 전투 주체 프로퍼티.
		/// </summary>
		public Battle Battle => m_Battle;

		/// <summary>
		/// 턴 횟수 프로퍼티.
		/// </summary>
		public int Turn => m_Turn;

		/// <summary>
		/// 플레이어 페이즈 여부 프로퍼티.
		/// </summary>
		public bool IsPlayerPhase => m_Controller is PlayerController;

		/// <summary>
		/// 현재 컨트롤러 프로퍼티.
		/// </summary>
		public Controller Controller => m_Controller;

		/// <summary>
		/// 현재 이벤트 프로퍼티.
		/// </summary>
		public Event Event => m_Event;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Context(Battle battle) : base()
		{
			m_Battle = battle;
			m_Controller = null;
			m_Event = null;
			m_Turn = 0;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
		}

		/// <summary>
		/// 현재 컨트롤러 설정.
		/// </summary>
		internal void InternalSetController(Controller controller)
		{
			m_Controller = controller;
		}

		/// <summary>
		/// 현재 이벤트 설정.
		/// </summary>
		internal void InternalSetEvent(Event @event)
		{
			m_Event = @event;
		}

		/// <summary>
		/// 이벤트 발급.
		/// </summary>
		internal void InternalRaise<TEvent>() where TEvent : Event, new()
		{
			var @event = new TEvent();
			InternalSetEvent(@event);
			@event.InternalEnd(this);
			@event.InternalEnd(this);
		}
	}
}