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
			Debug.Log("[RuntimeInitializer] Run()");
			SharedInstances.Clear();
			RunUI();
			RunBattle();
		}

		/// <summary>
		/// UI 생성 및 시작.
		/// </summary>
		private static void RunUI()
		{
			Input.multiTouchEnabled = false;

			// 매니저 생성.
			UIManager.Create();

			// 카드 생성.
			var card = new CardController();
			card.LoadView(); // card.View
			card.SetViewState(CardController.ViewState.Idle);
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