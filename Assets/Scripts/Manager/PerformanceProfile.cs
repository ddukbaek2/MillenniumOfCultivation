using Newtonsoft.Json;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 성능 프로파일.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class PerformanceProfile
	{
		/// <summary>
		/// 디스플레이 수직 동기화 활성화 여부 설정. (기본값: 디스플레이 주사율)
		/// <para>참일 경우 디스플레이 주사율이 적용되며 FrameRate는 무시됨.</para>
		/// </summary>
		[JsonProperty]
		public bool VerticalSynchronization { set; get; }

		/// <summary>
		/// 초당 갱신 주기 설정.
		/// </summary>
		[JsonProperty]
		public int MaxFrameRate { set; get; }

		/// <summary>
		/// 디스플레이 잠자기 활성화 여부 설정.
		/// </summary>
		[JsonProperty]
		public bool DisplaySleep { set; get; }

		/// <summary>
		/// 포커스를 상실 했을 때 백그라운드 상태에서도 계속 동작하기 활성화 여부 설정.
		/// </summary>
		[JsonProperty]
		public bool RunInBackground { set; get; }

		/// <summary>
		/// 렌더링 스케일 설정.
		/// </summary>
		[JsonProperty]
		public float RenderScale { set; get; }

		/// <summary>
		/// 물리 연산 갱신 주기 활성화 여부 설정.
		/// <para>참일 경우 기본값이 적용되며 PhysicsMaxFrameRate는 무시됨.</para>
		/// </summary>
		[JsonProperty]
		public bool PhysicsSynchronization { set; get; }

		/// <summary>
		/// 물리 연산 초당 갱신 주기 설정. (기본값: 0.02초 = 50)
		/// </summary>
		[JsonProperty]
		public int PhysicsMaxFrameRate { set; get; }

		/// <summary>
		/// 지속적인 성능 모드 활성화 여부 설정. (안드로이드 전용)
		/// </summary>
		[JsonProperty]
		public bool SustainedPerformanceMode { set; get; }
	}
}