using Crockhead.Unity;
using UnityEngine;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 사운드 매니저.
	/// </summary>
	public class SoundManager : SoundManager<SoundManager>
	{
		/// <summary>
		/// 애플리케이션 일시정지/재개됨.
		/// </summary>
		protected override void OnApplicationPause(bool pause)
		{
			base.OnApplicationPause(pause);
		}

		/// <summary>
		/// 애플리케이션 포커스획득/상실됨.
		/// </summary>
		protected override void OnApplicationFocus(bool focus)
		{
			base.OnApplicationFocus(focus);
		}

		/// <summary>
		/// 재생.
		/// </summary>
		public Sound Play(int soundTableId)
		{
			try
			{
				var assetPath = "Assets/Resources/Sound/UI/00103.wav";
				var assetPathType = AssetPathType.Resources;
				var soundType = SoundType.UI;
				//using var assetReader = new AssetReader<AudioClip>(assetPath, assetPathType);
				//assetReader.Read();
				//var audioClip = assetReader.Result;

				var sound = Play(assetPath, assetPathType, soundType);
				return sound;
			}
			catch
			{
				throw;
			}
		}
	}
}