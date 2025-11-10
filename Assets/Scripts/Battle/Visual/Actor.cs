using Crockhead.Core;
using UnityEngine;


namespace MillenniumOfCultivation.Battle
{
	public class ActorState : Disposable
	{
		public ActorState()
		{
		}

		protected override void OnDispose(bool explicitDisposing)
		{
		}
	}


	/// <summary>
	/// 화면 상에 출력되는 개체.
	/// </summary>
	public class Actor : MonoBehaviour
	{
		public enum AnimationState
		{
			None = 0,
			Spawn,
			Idle,
			Move,
			Attack,
			Damaged,
			Dead,
			Despawn,
		}

		/// <summary>
		/// 액터 스프라이트.
		/// </summary>
		private Sprite m_ActorSprite;

		/// <summary>
		/// 애니메이션 상태.
		/// </summary>
		private AnimationState m_AnimationState;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected virtual void Awake()
		{
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected virtual void Start()
		{
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected virtual void OnDestroy()
		{
		}

		/// <summary>
		/// 액터 스프라이트 설정.
		/// </summary>
		public void SetActorSprite(Sprite sprite)
		{
			m_ActorSprite = sprite;

			SetAnimationState(m_AnimationState, true);
		}

		/// <summary>
		/// 상태 설정.
		/// </summary>
		public void SetAnimationState(AnimationState state, bool forced = false)
		{
			if (state == AnimationState.None)
				return;

			if (m_AnimationState == state && !forced)
				return;

			m_AnimationState = state;

			switch (state)
			{
				case AnimationState.Spawn:
					{
						break;
					}

				case AnimationState.Idle:
					{
						break;
					}

				case AnimationState.Move:
					{
						break;
					}

				case AnimationState.Attack:
					{
						break;
					}

				case AnimationState.Damaged:
					{
						break;
					}

				case AnimationState.Dead:
					{
						break;
					}

				case AnimationState.Despawn:
					{
						break;
					}
			}
		}
	}
}