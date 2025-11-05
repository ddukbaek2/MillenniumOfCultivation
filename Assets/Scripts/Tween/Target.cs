using Crockhead.Core;


namespace MillenniumOfCultivation.Tween
{
	/// <summary>
	/// 트윈 적용 대상.
	/// </summary>
	public abstract class Target : Disposable
	{
		/// <summary>
		/// 값.
		/// </summary>
		private Value m_Value;

		/// <summary>
		/// 값 프로퍼티.
		/// </summary>
		public Value Value => m_Value;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Target(ValueType type) : base()
		{
			m_Value = Value.Create(type);
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
		}

		/// <summary>
		/// 값 변화됨.
		/// </summary>
		protected virtual void OnChangedValue(Value value)
		{
			m_Value = value;
		}

		/// <summary>
		/// 값 설정.
		/// </summary>
		public void SetValue(Value value)
		{
			OnChangedValue(value);
		}
	}
}