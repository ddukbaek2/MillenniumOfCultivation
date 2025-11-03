using Crockhead.Core;
using Crockhead.Unity;
using System.Collections.Generic;
using UnityEngine;


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
		/// 턴 횟수.
		/// </summary>
		private int m_Turn;

		/// <summary>
		/// 고유 식별자 생성기.
		/// </summary>
		private NumberIdentifiers m_NumberIdentifiers;

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
		/// 턴 횟수 프로퍼티.
		/// </summary>
		public int Turn => m_Turn;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Battle() : base()
		{
			m_IsStarted = false;
			m_Controllers = new List<Controller>();
			m_Stack = new Stack<Event>();
			m_Context = null;
			m_Turn = 0;
			m_NumberIdentifiers = new NumberIdentifiers();
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
		/// 이벤트 스택 처리.
		/// </summary>
		private void Process()
		{
			if (m_Stack.Count == 0)
				return;

			var current = m_Stack.Peek();
			if (!current.IsStarted)
			{
				current.Start(m_Context);
				Coroutines.WaitForNextFrame(Process);
			}
			else if (!current.IsProcessed)
			{
				current.Process(m_Context);
				Coroutines.WaitForNextFrame(Process);
			}
			else if (!current.IsCompleted)
			{
				current.Complete(m_Context);
				m_Stack.Pop();
				Coroutines.WaitForNextFrame(Process);
			}
		}

		/// <summary>
		/// 시작.
		/// </summary>
		public void Start(PlayerController player, List<AIController> enemies)
		{
			if (m_IsStarted)
				return;

			Debug.Log("[Battle] Start()");

			m_IsStarted = true;

			// 전투 컨텍스트 생성.
			Disposables.Dispose(m_Context);
			m_Context = new Context(this);

			// 컨트롤러 목록 생성.
			m_Controllers.Clear();
			m_Controllers.Add(player);
			m_Controllers.AddRange(enemies);

			// 전투 시작 이벤트.
			m_Context.Next<BattleEvent>();
			Coroutines.WaitForNextFrame(Process);
		}

		/// <summary>
		/// 강제 종료.
		/// </summary>
		public void Stop()
		{
			if (!m_IsStarted)
				return;

			Debug.Log("[Battle] Stop()");

			m_IsStarted = false;
			Disposables.Dispose(m_Context);
			m_Controllers.Clear();
		}

		/// <summary>
		/// 이벤트 스택 중에 실행될 이벤트 생성.
		/// </summary>
		public void Now<TEvent>() where TEvent : Event, new()
		{
			var @event = new TEvent();
			m_Stack.Push(@event);
			//InternalSetEvent(@event);
			//@event.Complete(this);
			//@event.Complete(this);
		}

		/// <summary>
		/// 이벤트 스택이 끝나고 실행 될 예약 이벤트 생성.
		/// </summary>
		public void Next<TEvent>() where TEvent : Event, new()
		{
			var @event = new TEvent();
			m_Stack.Push(@event);
		}
	}
}