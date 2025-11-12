namespace Crockhead.Unity
{
	public class SoundManager : MillenniumOfCultivation.SharedComponent<SoundManager>
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		/// <summary>
		/// 오디오 클립 로드.
		/// </summary>
		public virtual void LoadAudioClip()
		{
		}

		/// <summary>
		/// 오디오 클립 로드됨.
		/// </summary>
		protected virtual void OnAudioClipDidLoad()
		{
		}

		/// <summary>
		/// 재생.
		/// </summary>
		public void Play()
		{
		}
	}
}