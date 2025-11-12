using MillenniumOfCultivation;
using UnityEngine;


namespace Crockhead.Unity
{
	/// <summary>
	/// 사운드.
	/// </summary>
	[RequireComponent(typeof(AudioSource))]
	public class Sound : CrockheadBehaviour
	{
		#region INSPECTOR
		[SerializeField] private AudioSource m_AudioSource;
		#endregion

		public bool IsPlaying;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			m_AudioSource = TransformHelper.GetOrAddComponent<AudioSource>(transform);
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

		public void Stop()
		{
		}
	}
}