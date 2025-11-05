using UnityEngine;


namespace MillenniumOfCultivation.Tween
{
	/// <summary>
	/// 컴포넌트 대상.
	/// </summary>
	public class ComponentTarget<TComponent> : Target where TComponent : Component
	{
		/// <summary>
		/// 적용 대상.
		/// </summary>
		private TComponent m_Target;

		/// <summary>
		/// 적용 대상 프로퍼티.
		/// </summary>
		public TComponent Target => m_Target;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public ComponentTarget(TComponent target, ValueType type) : base(type)
		{
			m_Target = target;
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
		protected override void OnChangedValue(Value value)
		{
		}
	}
}