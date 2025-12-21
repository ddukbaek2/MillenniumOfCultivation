using Crockhead.Core;
using System.Collections;


namespace Crockhead.Unity
{
	/// <summary>
	/// 대기.
	/// </summary>
	public class Yield : Disposable, IEnumerator // YieldInstruction, CustomYieldInstruction
	{
		/// <summary>
		/// 열거자의 현재 요소 프로퍼티.
		/// </summary>
		object IEnumerator.Current => null;

		/// <summary>
		/// 대기 여부 프로퍼티. (참이면 계속 대기)
		/// </summary>
		public virtual bool KeepWaiting { get; } = false;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Yield() : base()
		{
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
		}

		/// <summary>
		/// 초기화.
		/// </summary>
		void IEnumerator.Reset()
		{
		}

		/// <summary>
		/// 열거자의 다음 요소로 이동.
		/// </summary>
		bool IEnumerator.MoveNext()
		{
			if (this == null)
				return false;
			if (IsDisposed)
				return false;

			return KeepWaiting;
		}

		public static IEnumerator Create<TYield>() where TYield : Yield, new()
		{
			return new TYield();
		}
	}
}