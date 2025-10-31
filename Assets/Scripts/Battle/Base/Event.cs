using Crockhead.Core;


namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 이벤트.
	/// </summary>
	public abstract class Event : Disposable
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public Event() : base()
		{
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
		protected virtual void OnBegin(Context context)
		{
		}

		/// <summary>
		/// 이벤트 전환됨.
		/// </summary>
		protected virtual void OnTransition(Context context, Event previous, Event next)
		{
		}

		/// <summary>
		/// 이벤트 종료됨.
		/// </summary>
		protected virtual void OnEnd(Context context)
		{
		}

		/// <summary>
		/// 이벤트 시작.
		/// </summary>
		internal void InternalBegin(Context context)
		{
			OnBegin(context);
		}

		/// <summary>
		/// 이벤트 전환.
		/// </summary>
		internal void InternalTransition(Context context, Event previous, Event next)
		{
			OnTransition(context, previous, next);
		}

		/// <summary>
		/// 이벤트 종료.
		/// </summary>
		internal void InternalEnd(Context context)
		{
			OnEnd(context);
		}
	}
}