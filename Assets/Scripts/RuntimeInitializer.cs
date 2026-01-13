using Crockhead.Core;
using Crockhead.Unity;
using MillenniumOfCultivation.Battle;
using MillenniumOfCultivation.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BattleProcess = MillenniumOfCultivation.Battle.BattleProcess;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 게임 진입점.
	/// </summary>
	public static class RuntimeInitializer
	{
		/// <summary>
		/// 메인 씬 이름.
		/// </summary>
		public static string MainRuntimeSceneName = "MillenniumOfCultivation";

		/// <summary>
		/// 시작.
		/// </summary>
		[RuntimeInitializeOnLoadMethod]
		private static void Run()
		{
			var activeScene = SceneManager.GetActiveScene();
			if (activeScene.name != RuntimeInitializer.MainRuntimeSceneName)
				return;

			Debug.Log("[RuntimeInitializer] Run()");

			// 프레임워크 초기화.
			// 공유 인스턴스 캐시 비우기.
			SharedInstances.Clear();
			var unityRuntime = UnityRuntime.Create();
			GameObject.DontDestroyOnLoad(unityRuntime.gameObject);

			// 초기화.
			Initialize();
		}

		/// <summary>
		/// 시작.
		/// </summary>
		public static void Initialize()
		{
			Debug.Log("[RuntimeInitializer] Initialize()");

			// 매니저 생성.
			PlatformManager.Create();
			PerformanceManager.Create();
			CameraManager.Create();
			SoundManager.Create();
			InputManager.Create();
			UIManager.Create();
			MessageManager.Create();
			BattleManager.Create();

			// 다음 프레임에 실행.
			DispatchQueue.Foreground.RunNextFrameAsync(RunUI);
		}

		/// <summary>
		/// 종료.
		/// </summary>
		public static void Shutdown()
		{
			Debug.Log("[RuntimeInitializer] Shutdown()");

			Disposables.Dispose(PlatformManager.Instance);
			Disposables.Dispose(PerformanceManager.Instance);
			Disposables.Dispose(MessageManager.Instance);
			Disposables.Dispose(BattleManager.Instance);
			SoundManager.Dispose();
			UIManager.Dispose();
			SceneManager.LoadScene(RuntimeInitializer.MainRuntimeSceneName, LoadSceneMode.Single);
		}

		/// <summary>
		/// UI 실행.
		/// </summary>
		private static void RunUI()
		{
			var intro = new UIIntroController();
			_ = UIManager.Instance.PresentAsync(intro);
		}
	}
}