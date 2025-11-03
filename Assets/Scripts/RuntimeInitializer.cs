using Crockhead.Core;
using MillenniumOfCultivation.Battle;
using System.Collections.Generic;
using UnityEngine;


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
	/// 전투 시작.
	/// </summary>
	private static void RunBattle()
	{
		var player = new PlayerController(0);
		var enemy = new AIController(0);

		var battle = new Battle();		
		battle.Start(player, new List<AIController>() { enemy });
	}

	/// <summary>
	/// UI 생성 및 시작.
	/// </summary>
	private static void RunUI()
	{
	}
}