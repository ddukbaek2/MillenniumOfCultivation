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
		/// 시작.
		/// </summary>
		[RuntimeInitializeOnLoadMethod]
		private static void Run()
		{
			Debug.Log("[RuntimeInitializer] Run()");

			// 공유 인스턴스 캐시 비우기.
			SharedInstances.Clear();

			// 카메라 생성.
			var assetLoader = new AssetLoader<GameObject>("Assets/Resources/Base/MainCamera.prefab");
			assetLoader.Load();
			var obj = GameObject.Instantiate(assetLoader.Asset);
			GameObject.DontDestroyOnLoad(obj);

			// 초기화.
			Initialize();
		}

		/// <summary>
		/// 시작.
		/// </summary>
		public static void Initialize()
		{
			var activeScene = SceneManager.GetActiveScene();
			if (activeScene.name != "MillenniumOfCultivation")
				return;

			Debug.Log("[RuntimeInitializer] Initialize()");

			// 매니저 생성.
			PlatformManager.Create();
			PerformanceManager.Create();
			SoundManager.Create();
			UIManager.Create();
			MessageManager.Create();
			BattleManager.Create();

			RunUI();
			RunBattle();
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
			SceneManager.LoadScene("MillenniumOfCultivation", LoadSceneMode.Single);
		}

		/// <summary>
		/// UI 생성.
		/// </summary>
		private static void RunUI()
		{
			Input.multiTouchEnabled = false;

			// 시작.
			//var battleController = new BattleController(UIManager.Instance.TopWindow);
			//UIManager.Instance.Present(battleController);		
			var intro = new IntroController();
			UIManager.Instance.Present(intro);
		}

		/// <summary>
		/// 전투 시작.
		/// </summary>
		private static void RunBattle()
		{
			var player = new PlayerController();
			var enemy = new AIController();

			var process = new BattleProcess();
			process.Start(player, new List<AIController>() { enemy });
		}
	}
}