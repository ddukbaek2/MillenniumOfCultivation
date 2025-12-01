using Crockhead.Core;
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
		public static void Run()
		{
			var activeScene = SceneManager.GetActiveScene();
			if (activeScene.name != "MillenniumOfCultivation")
				return;

			Debug.Log("[RuntimeInitializer] Present()");
			SharedInstances.Clear();

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
		/// UI 생성 및 시작.
		/// </summary>
		private static void RunUI()
		{
			Input.multiTouchEnabled = false;

			// 시작.
			//var battleController = new BattleController(UIManager.Instance.TopWindow);
			//UIManager.Instance.Present(battleController);		
			var intro = new IntroController(UIManager.Instance.TopWindow);
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