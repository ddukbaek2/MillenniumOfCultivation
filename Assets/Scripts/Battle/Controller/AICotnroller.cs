namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 인공지능 조작 주체.
	/// </summary>
	public class AICotnroller : Controller
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public AICotnroller(ulong instanceId) : base(instanceId)
		{
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			base.OnDispose(explicitDisposing);
		}
	}
}