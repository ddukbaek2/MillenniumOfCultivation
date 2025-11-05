using Crockhead.Core;
using System;


namespace MillenniumOfCultivation.Tween
{
	/// <summary>
	/// 변위자.
	/// <para>값을 from==>to로 변환.</para>
	/// </summary>
	public abstract class Displacer : Disposable
	{
		/// <summary>
		/// 변위 종류.
		/// </summary>
		private DisplaceType m_Type;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Displacer(DisplaceType type) : base()
		{
			m_Type = type;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
		}

		/// <summary>
		/// 변위.
		/// </summary>
		public virtual Value Displace(Value from, Value to, float progress)
		{
			//var value = (to / from) * progress;
			var value = Value.Create(ValueType.Float);

			switch (m_Type)
			{
				case DisplaceType.Linear:
					{
						break;
					}
			}

			return value;
		}
	}
}