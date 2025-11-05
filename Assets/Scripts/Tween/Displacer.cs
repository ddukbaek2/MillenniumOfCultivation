using Crockhead.Core;


namespace MillenniumOfCultivation.Tween
{
	/// <summary>
	/// 변위자.
	/// </summary>
	public abstract class Displacer : Disposable
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public Displacer() : base()
		{
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
		}

		/// <summary>
		/// 변위됨.
		/// </summary>
		protected virtual void OnDisplacing(Value value, float progress)
		{
		}

		/// <summary>
		/// 변위.
		/// </summary>
		public void Displace(Value from, Value to, float progress)
		{
			OnDisplacing(to, progress);
		}
	}
}