using Crockhead.Core;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 플랫폼 매니저.
	/// </summary>
	public class PlatformManager : SharedClass<PlatformManager>
	{
		/// <summary>
		/// 에디터 여부 프로퍼티.
		/// </summary>
		public bool IsEditor
		{
			get
			{
#if UNITY_EDITOR
				return true;
#else
				return false;
#endif
			}
		}

		/// <summary>
		/// 플랫폼 종류 프로퍼티.
		/// </summary>
		public PlatformType Platform
		{
			get
			{
#if STEAMWORKS
				return PlatformType.Steam;
#elif UNITY_ANDROID
				return PlatformType.Android;
#elif UNITY_IOS
				return PlatformType.iOS;
#endif
				return PlatformType.None;
			}
		}

		/// <summary>
		/// 입력 종류 프로퍼티.
		/// </summary>
		public InputType InputType
		{
			get
			{
				return InputType.None;
			}
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose()
		{
			base.OnDispose();
		}
	}
}
