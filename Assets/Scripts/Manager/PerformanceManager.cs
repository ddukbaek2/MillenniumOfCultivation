using Crockhead.Core;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 성능 매니저.
	/// </summary>
	public class PerformanceManager : SharedClass<PerformanceManager>
	{
		/// <summary>
		/// 성능: 매우 낮음 설정.
		/// </summary>
		public static readonly PerformanceProfile VeryLow = new PerformanceProfile
		{
			VerticalSynchronization = false,
			MaxFrameRate = 30,
			DisplaySleep = true,
			RunInBackground = false,
			RenderScale = 0.5f,
		};


		/// <summary>
		/// 성능: 낮음 설정.
		/// </summary>
		public static readonly PerformanceProfile Low = new PerformanceProfile
		{
			VerticalSynchronization = false,
			MaxFrameRate = 30,
			DisplaySleep = true,
			RunInBackground = false,
			RenderScale = 0.7f,
		};


		/// <summary>
		/// 성능: 보통 설정.
		/// </summary>
		public static readonly PerformanceProfile Normal = new PerformanceProfile
		{
			VerticalSynchronization = false,
			MaxFrameRate = 60,
			DisplaySleep = true,
			RunInBackground = false,
			RenderScale = 1f,
		};


		/// <summary>
		/// 성능: 높음 설정.
		/// </summary>
		public static readonly PerformanceProfile High = new PerformanceProfile
		{
			VerticalSynchronization = true,
			MaxFrameRate = -1,
			DisplaySleep = false,
			RunInBackground = true,
			RenderScale = 1.2f,
		};


		/// <summary>
		/// 성능: 매우 높음 설정.
		/// </summary>
		public static readonly PerformanceProfile VeryHigh = new PerformanceProfile
		{
			VerticalSynchronization = false,
			MaxFrameRate = -1,
			DisplaySleep = false,
			RunInBackground = true,
			RenderScale = 1.5f,
		};


		/// <summary>
		/// 원래의 물리 연산 갱신 주기.
		/// </summary>
		private float m_DefaultFixedDeltaTime;

		/// <summary>
		/// 현재 성능 설정.
		/// </summary>
		private PerformanceProfile m_Profile;

		/// <summary>
		/// 최대 프레임 레이트 프로퍼티.
		/// </summary>
		public int MaxFrameRate
		{
			get
			{
				// 디스플레이 주사율.
				var displayRefreshRate = (int)Screen.currentResolution.refreshRateRatio.value;
				if (m_Profile == null)
					return displayRefreshRate;

				if (m_Profile.VerticalSynchronization)
					return displayRefreshRate;

				return m_Profile.MaxFrameRate;
			}
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			m_Profile = PerformanceManager.High;
			m_DefaultFixedDeltaTime = Time.fixedDeltaTime;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose()
		{
			base.OnDispose();
		}

		/// <summary>
		/// 적용.
		/// </summary>
		public void Apply(PerformanceProfile profile)
		{
			if (profile == null)
				throw new NullReferenceException("PerformanceProfile");

			m_Profile = profile;
			Apply();
		}

		/// <summary>
		/// 적용.
		/// </summary>
		public void Apply()
		{
			if (m_Profile == null)
				throw new NullReferenceException("PerformanceProfile");

			// 최대 프레임 설정.
			if (m_Profile.VerticalSynchronization)
			{
				QualitySettings.vSyncCount = 1;
				Application.targetFrameRate = -1;
			}
			else
			{
				QualitySettings.vSyncCount = 0;
				Application.targetFrameRate = m_Profile.MaxFrameRate;
			}

			// 백그라운드 동작 비활성화.
			if (m_Profile.RunInBackground)
			{
				Application.runInBackground = true;
			}
			else
			{
				Application.runInBackground = false;
			}

			// 화면 꺼짐 설정.
			if (m_Profile.DisplaySleep)
			{
				Screen.sleepTimeout = SleepTimeout.SystemSetting;
			}
			else
			{
				Screen.sleepTimeout = SleepTimeout.NeverSleep;
			}

			// 물리 업데이트 횟수 설정.
			if (m_Profile.PhysicsSynchronization)
			{
				// 기본값: 0.02f 설정.
				Time.fixedDeltaTime = m_DefaultFixedDeltaTime;
			}
			else
			{
				// 최대 프레임수에 따른 값 설정.
				// 60 프레임일 경우 60f;
				Time.fixedDeltaTime = (float)MaxFrameRate / 1f;
			}

			// 렌더링 처리용 버퍼 크기 비율 설정.
			var universalRenderPipelineAsset = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
			if (universalRenderPipelineAsset != null)
			{
				universalRenderPipelineAsset.renderScale = m_Profile.RenderScale;
			}

			// 전체화면 설정.
			//var width  = (int)(Screen.currentResolution.width * 0.75f);
			//var height = (int)(Screen.currentResolution.height * 0.75f);
			//Screen.SetResolution(width, height, true);

#if UNITY_ANDROID
			UnityEngine.Android.AndroidDevice.SetSustainedPerformanceMode(m_Profile.SustainedPerformanceMode);
#endif
		}
	}
}
