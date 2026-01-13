using Crockhead.Unity;
using UnityEngine;
using UnityEngine.Rendering;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 사운드 매니저.
	/// </summary>
	public class SoundManager : SoundManager<SoundManager>
	{
		private bool m_IsCreated;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			m_IsCreated = true;
		}

		///// <summary>
		///// 애플리케이션 일시정지/재개됨.
		///// </summary>
		//protected override void OnApplicationPause(bool pause)
		//{
		//	base.OnApplicationPause(pause);
		//}

		///// <summary>
		///// 애플리케이션 포커스획득/상실됨.
		///// </summary>
		//protected override void OnApplicationFocus(bool focus)
		//{
		//	base.OnApplicationFocus(focus);
		//}

		/// <summary>
		/// 갱신됨.
		/// </summary>
		protected override void Update()
		{
			if (!m_IsCreated)
				return;

			base.Update();
		}

		/// <summary>
		/// 미리 로드.
		/// </summary>
		public void Preload()
		{
		}

		/// <summary>
		/// 재생.
		/// </summary>
		public Sound Play(int soundTableId)
		{
			try
			{
				//var assetPath = "Assets/Resources/Sound/UI/00103.wav";
				//var assetPathType = AssetPathType.Resources;
				//var soundType = SoundType.UI;
				var soundTableRecord = SoundTable.Instance.Find(soundTableId);
				var assetPath = soundTableRecord.AssetPath;
				var assetPathType = AssetPathType.Resources; // soundTableRecord.AssetPathType;
				var soundType = soundTableRecord.Type;

				var sound = Play(assetPath, assetPathType, soundType);
				return sound;
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 재생.
		/// </summary>
		public Sound Play(string name)
		{
			try
			{
				var soundTableRecord = SoundTable.Instance.Find(record => record.Name == name);
				var assetPath = soundTableRecord.AssetPath;
				var assetPathType = AssetPathType.Resources;
				var soundType = soundTableRecord.Type;

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