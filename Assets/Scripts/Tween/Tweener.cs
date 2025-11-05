using Crockhead.Core;
using UnityEngine;


namespace MillenniumOfCultivation.Tween
{
	/// <summary>
	/// 트윈.
	/// </summary>
	public abstract class Tweener : Disposable
	{
		/// <summary>
		/// 재생 중 여부.
		/// </summary>
		private bool m_IsPlaying;

		/// <summary>
		/// 지속 시간.
		/// </summary>
		private float m_Duration;

		/// <summary>
		/// 반복 횟수.
		/// </summary>
		private int m_Repeat;

		/// <summary>
		/// 누적 시간.
		/// </summary>
		private float m_ElapsedTime;

		/// <summary>
		/// 트윈 대상.
		/// </summary>
		private Target m_Target;

		/// <summary>
		/// 적용 주체.
		/// </summary>
		private Displacer m_Displacer;

		/// <summary>
		/// 시작 값.
		/// </summary>
		private Value m_FromValue;

		/// <summary>
		/// 종료 값.
		/// </summary>
		private Value m_ToValue;

		/// <summary>
		/// 재생 중 여부 프로퍼티.
		/// </summary>
		public bool IsPlaying => m_IsPlaying;

		/// <summary>
		/// 적용 주체 프로퍼티.
		/// </summary>
		public Displacer Displacer => m_Displacer;

		/// <summary>
		/// 1회 진행율 프로퍼티.
		/// </summary>
		public float Progress => Mathf.Clamp01(m_ElapsedTime / m_Duration);

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Tweener(Target target, Displacer displacer, Value fromValue, Value toValue) : base()
		{
			m_IsPlaying = false;
			m_Duration = 0f;
			m_Repeat = 0;
			m_ElapsedTime = 0f;

			m_Target = target;
			m_FromValue = fromValue;
			m_ToValue = toValue;
			m_Displacer = displacer;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
		}

		/// <summary>
		/// 시작됨.
		/// </summary>
		protected virtual void OnStarted()
		{
		}

		/// <summary>
		/// 갱신됨.
		/// </summary>
		protected virtual void OnUpdate(float timeDelta)
		{
			m_ElapsedTime += timeDelta;

			var value = m_Displacer.Displace(m_FromValue, m_ToValue, Progress);
			m_Target.SetCurrentValue(value);

			if (m_ElapsedTime >= m_Duration)
			{
				if (m_Repeat > 0)
				{
					--m_Repeat;

					if (m_Repeat > 0)
					{
						m_ElapsedTime -= m_Duration;
					}
					else
					{
						Stop(true);
					}
				}
			}
		}

		/// <summary>
		/// 완료됨.
		/// </summary>
		protected virtual void OnCompleted()
		{
		}

		/// <summary>
		/// 시작.
		/// </summary>
		public void Start(bool applyFromValue)
		{
			if (m_IsPlaying)
				return;

			m_IsPlaying = true;
			m_ElapsedTime = 0f;

			if (applyFromValue)
			{
				m_Target.SetCurrentValue(m_FromValue);
			}
			else
			{
				m_FromValue = m_Target.CurrentValue;
			}

			OnStarted();
		}

		/// <summary>
		/// 재시작.
		/// </summary>
		public void Restart(bool applyFromValue)
		{
			if (m_IsPlaying)
			{
				Stop(false);
				Start(applyFromValue);
			}
			else
			{
				Start(applyFromValue);
			}
		}

		/// <summary>
		/// 업데이트.
		/// </summary>
		public void Update(float timeDelta)
		{
			if (!m_IsPlaying)
				return;

			OnUpdate(timeDelta);
		}

		/// <summary>
		/// 정지.
		/// </summary>
		public void Stop(bool applyToValue)
		{
			if (!m_IsPlaying)
				return;

			m_IsPlaying = false;

			if (applyToValue)
			{
				m_Target.SetCurrentValue(m_ToValue);
			}

			OnCompleted();
		}
	}
}