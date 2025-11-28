using Crockhead.Core;
using MillenniumOfCultivation.Battle;
using MillenniumOfCultivation.UI;
using System.Collections.Generic;
using UnityEngine;
using BattleProcess = MillenniumOfCultivation.Battle.Process;


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
			Debug.Log("[RuntimeInitializer] Present()");
			SharedInstances.Clear();

			// 매니저 생성.
			PlatformManager.Create();
			PerformanceManager.Create();
			SoundManager.Create();
			UIManager.Create();
			MessageManager.Create();

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
			var introController = new IntroController(UIManager.Instance.TopWindow);
			UIManager.Instance.Present(introController);

			//var battle = new BattleController();
			//battle.LoadRootView(); // battle.RootView
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