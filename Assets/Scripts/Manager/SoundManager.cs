using Crockhead.Unity;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 사운드 매니저.
	/// </summary>
	public class SoundManager : SoundManager<SoundManager>
	{
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