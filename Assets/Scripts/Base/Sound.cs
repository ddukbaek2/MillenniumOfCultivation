using MillenniumOfCultivation;
using UnityEngine;
using UnityEngine.Audio;


namespace Crockhead.Unity
{
	/// <summary>
	/// 사운드.
	/// </summary>
	[RequireComponent(typeof(AudioSource))]
	public class Sound : MonoBehaviour
	{
		#region INSPECTOR
		[SerializeField] private AudioSource m_AudioSource;
		#endregion

		/// <summary>
		/// 재생 중인지 여부 프로퍼티.
		/// </summary>
		public bool IsPlaying => m_AudioSource.isPlaying;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected virtual void Awake()
		{
			//base.Awake();

			m_AudioSource = TransformHelper.GetOrAddComponent<AudioSource>(transform);
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected virtual void OnDestroy()
		{
			//base.OnDestroy();
		}

		/// <summary>
		/// 재생.
		/// </summary>
		public void Play(AudioMixerGroup audioMixerGroup, AudioClip audioClip)
		{
			m_AudioSource.outputAudioMixerGroup = audioMixerGroup;
			m_AudioSource.clip = audioClip;
			m_AudioSource.Play();
		}

		/// <summary>
		/// 정지.
		/// </summary>
		public void Stop()
		{
			m_AudioSource.Stop();
			m_AudioSource.clip = null;
		}
	}
}