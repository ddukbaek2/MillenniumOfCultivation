using Crockhead.Core;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 식별 될 수 있는 객체.
	/// </summary>
	public class Identifiable : Disposable
	{
		private static NumberIdentifiers s_NumberIdentifiers = new NumberIdentifiers();

		/// <summary>
		/// 인스턴스 고유 식별자.
		/// </summary>
		public ulong InstanceId { get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Identifiable() : base()
		{
			InstanceId = s_NumberIdentifiers.Generate();
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Identifiable(ulong instanceId) : base()
		{
			InstanceId = instanceId;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			s_NumberIdentifiers.Release(InstanceId);
		}
	}
}