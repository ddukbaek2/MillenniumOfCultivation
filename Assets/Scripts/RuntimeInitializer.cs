using Crockhead.Core;
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
	}

	/// <summary>
	/// UI 생성 및 시작.
	/// </summary>
	private static void RunUI()
	{
	}
}