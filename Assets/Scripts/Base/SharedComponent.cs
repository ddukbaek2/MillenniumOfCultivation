using Crockhead.Core;
using Crockhead.Unity;
using UnityEngine;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 공유 컴포넌트.
	/// </summary>
	public abstract class SharedComponent<TComponent> : MonoBehaviour where TComponent : SharedComponent<TComponent>
	{
		/// <summary>
		/// 생성 되었는지 여부 프로퍼티.
		/// </summary>
		public static bool IsCreated => SharedInstances.IsSet<TComponent>();

		/// <summary>
		/// 공유 컴포넌트 프로퍼티.
		/// </summary>
		public static TComponent SharedInstance => Create();

		/// <summary>
		/// 객체가 파괴 되었는지 여부.
		/// </summary>
		private bool m_IsDestroyed;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected virtual void Awake()
		{
			if (SharedInstances.IsSet<TComponent>())
			{
				GameObject.Destroy(gameObject);
				return;
			}

			GameObject.DontDestroyOnLoad(gameObject);
			SharedInstances.Set<TComponent>((TComponent)this);
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected virtual void OnDestroy()
		{
			if (!SharedInstances.IsSet<TComponent>())
				return;

			var sharedInstance = SharedInstances.Get<TComponent>();
			if (sharedInstance != this)
				return;

			SharedInstances.Unset<TComponent>();
		}

		/// <summary>
		/// 현재 객체가 파괴 되었는지 여부.
		/// </summary>
		public bool IsDestroyed()
		{
			if (this == null)
				return true;

			if (m_IsDestroyed)
				return true;

			return false;
		}


		/// <summary>
		/// 생성.
		/// </summary>
		public static TComponent Create()
		{
			if (SharedInstances.TryGet<TComponent>(out var sharedInstance))
				return sharedInstance;

			sharedInstance = GameObject.FindAnyObjectByType<TComponent>();
			if (sharedInstance != null)
			{
				SharedInstances.Set<TComponent>(sharedInstance);
				return sharedInstance;
			}

			sharedInstance = GameObjects.CreateGameObjectWithComponent<TComponent>();
			SharedInstances.Set<TComponent>(sharedInstance);
			return sharedInstance;
		}
	}
}