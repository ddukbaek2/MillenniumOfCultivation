namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 사용자 조작 주체.
	/// </summary>
	public class PlayerController : Controller
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public PlayerController(ulong instanceId) : base(instanceId)
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