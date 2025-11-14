using System;
using UnityEngine;
using UnityEngine.Audio;


namespace Crockhead.Unity
{
	/// <summary>
	/// 사운드 매니저.
	/// </summary>
	public class SoundManager : MillenniumOfCultivation.SharedComponent<SoundManager>
	{
		#region INSEPCTOR
		[SerializeField] private AudioMixer m_AudioMixer;		
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			if (m_AudioMixer == null)
			{
				m_AudioMixer = Resources.Load<AudioMixer>("Base/AudioMixer"); // AudioMixer.mixer
			}
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
		public AudioClip LoadAudioClip(string assetPath, AssetPathType assetPathType)
		{
			using var assetReader = new AssetReader<AudioClip>(assetPath, assetPathType);
			assetReader.Read();
			var audioClip = assetReader.Result;
			OnAudioClipDidLoad(assetPath, assetPathType, audioClip);
			return audioClip;
		}

		/// <summary>
		/// 오디오 클립 로드됨.
		/// </summary>
		protected virtual void OnAudioClipDidLoad(string assetPath, AssetPathType assetPathType, AudioClip audioClip)
		{
		}

		/// <summary>
		/// 재생.
		/// </summary>
		public Sound Play(string assetPath, AssetPathType assetPathType, SoundType soundType)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(assetPath))
					throw new ArgumentException(nameof(assetPath));

				// 생성.
				var audioClip = LoadAudioClip(assetPath, assetPathType);
				var obj = new GameObject("Sound");
				var sound = obj.AddComponent<Sound>();
				sound.transform.SetParent(transform, true);
				TransformHelper.ResetTransform(sound.transform);

				// 재생.
				var audioMixerGroup = GetAudioMixerGroup(soundType);
				sound.Play(audioMixerGroup, audioClip);
				return sound;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				throw exception;
			}
		}

		/// <summary>
		/// 재생.
		/// </summary>
		public Sound Play(int soundTableId)
		{
			try
			{
				if (soundTableId == 0)
					throw new ArgumentException(nameof(soundTableId));

				var assetPath = $"Assets/Resources/Sound/{soundTableId}.wav";
				var assetPathType = AssetPathType.Resources;
				var soundType = SoundType.Master;
				var sound = Play(assetPath, assetPathType, soundType);
				return sound;
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 소리 크기 설정.
		/// </summary>
		public void SetVolume(SoundType soundType, float volume)
		{
			var parameterName = $"{soundType}_Volume";
			var decibel = SoundManager.VolumeToDecibel(volume);
			m_AudioMixer.SetFloat(parameterName, decibel);
		}

		/// <summary>
		/// 소리 크기 반환.
		/// </summary>
		public float GetVolume(SoundType soundType)
		{
			var parameterName = $"{soundType}_Volume";
			var volume = 0f;
			if (m_AudioMixer.GetFloat(parameterName, out var decibel))
				volume = SoundManager.DecibelToVolume(decibel);

			return volume;
		}

		/// <summary>
		/// 오디오 믹서 그룹 반환.
		/// </summary>
		public AudioMixerGroup GetAudioMixerGroup(SoundType soundType)
		{
			var audioMixerGroupName = soundType.ToString();
			var audioMixerGroups = m_AudioMixer.FindMatchingGroups(audioMixerGroupName);
			if (audioMixerGroups == null || audioMixerGroups.Length == 0)
				return null;

			var audioMixerGroup = audioMixerGroups[0];
			return audioMixerGroup;
		}

		/// <summary>
		/// 대수적인 데시벨을 선형적인 볼륨으로 변경.
		/// <para>데시벨(-80~20) ==> 볼륨(0~10)</para>
		/// <para>볼륨이 1이 초과될 경우 증폭되는 것과 동일하므로 가급적 0~1로 범위 제한 필요.</para>
		/// </summary>
		public static float DecibelToVolume(float decibel)
		{
			// 데시벨이 -80보다 작으면 계산 할 필요 없음.
			if (decibel <= -80f)
				return 0f;

			// 기본은 볼륨의 안정적인 최대치는 0 데시벨, 단, 볼륨이 최대값의 10배 일 경우에도 20 데시벨까지는 적용됨.
			if (decibel >= 20f)
				return 10f; // 볼륨의 10배.

			var volume = Mathf.Pow(10f, decibel / 20f);
			return volume;
		}

		/// <summary>
		/// 선형적인 볼륨을 대수적인 데시벨로 변경.
		/// <para>볼륨(0~10) ==> 데시벨(-80~20)</para>
		/// <para>볼륨이 1이 초과될 경우 증폭되는 것과 동일하므로 가급적 0~1로 범위 제한 필요.</para>
		/// </summary>
		public static float VolumeToDecibel(float volume)
		{
			// 볼륨이 0% 보다 작으면 아래의 데시벨 계산이 infinity가 나오기 때문에 최하 데시벨인 -80으로 고정.
			if (volume <= 0f)
				return -80f;

			// 기본은 볼륨의 안정적인 최대치는 0 데시벨, 단, 볼륨이 최대값의 10배 일 경우에도 20 데시벨까지는 적용됨.
			if (volume >= 10f)
				return 20f;

			var decibel = Mathf.Log10(volume) * 20f;
			return decibel;
		}
	}
}