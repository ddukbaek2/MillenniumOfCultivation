using Crockhead.Core;
using MillenniumOfCultivation.Battle;
using MillenniumOfCultivation.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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

			Setup();
			RunUI();
			RunBattle();
		}

		/// <summary>
		/// 설정 적용.
		/// </summary>
		private static void Setup()
		{
			// 프레임 설정.
			Application.targetFrameRate = 30;

			// 백그라운드 동작 비활성화.
			Application.runInBackground = false;

			// 화면 꺼짐 설정.
			Screen.sleepTimeout = SleepTimeout.SystemSetting;

			// 물리 업데이트 횟수 감소.
			Time.fixedDeltaTime = (float)Application.targetFrameRate / 1f; // 0.02 ==> 0.033

			// 렌더링 처리용 버퍼 크기 비율 설정.
			//UniversalRenderPipelineAsset universalRenderPipelineAsset;
			//universalRenderPipelineAsset.renderScale = 0.75f; // 1f ==> 0.75f

			// 전체화면 설정.
			//var width  = (int)(Screen.currentResolution.width * 0.75f);
			//var height = (int)(Screen.currentResolution.height * 0.75f);
			//Screen.SetResolution(width, height, true);

//#if UNITY_ANDROID
			UnityEngine.Android.AndroidDevice.SetSustainedPerformanceMode(true);
//#endif
		}

		/// <summary>
		/// UI 생성 및 시작.
		/// </summary>
		private static void RunUI()
		{
			Input.multiTouchEnabled = false;

			// 매니저 생성.
			UIManager.Create();
			SoundManager.Create();
			MessageManager.Create();

			// 시작.
			//var battleController = new BattleController(UIManager.Instance.FrontWindow);
			//UIManager.Instance.Run(battleController);		
			var introController = new IntroController(UIManager.Instance.FrontWindow);
			UIManager.Instance.Run(introController);
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