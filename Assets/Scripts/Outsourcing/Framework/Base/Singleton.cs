using UnityEngine;


namespace Outsourcing
{
	/// <summary>
	/// 공유되는 단일 컴포넌트 인스턴스.
	/// </summary>
	public abstract class Singleton<TComponent> : MonoBehaviour where TComponent : Singleton<TComponent>
	{
		/// <summary>
		/// 인스턴스.
		/// </summary>
		private static TComponent s_Instance;

		/// <summary>
		/// 인스턴스 프로퍼티.
		/// </summary>
		public static TComponent Instance => Create();

		/// <summary>
		/// 생성됨.
		/// </summary>
		private void Awake()
		{
			if (!IsValid())
			{
				GameObject.Destroy(gameObject);
				return;
			}

			s_Instance = this as TComponent;
			Object.DontDestroyOnLoad(s_Instance.gameObject);
			OnCreate();
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		private void OnDestroy()
		{
			if (!IsValid())
				return;

			OnDispose();
			s_Instance = null;
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected virtual void OnCreate()
		{
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected virtual void OnDispose()
		{
		}

		/// <summary>
		/// 싱글톤 인스턴스 여부 반환.
		/// </summary>
		public bool IsValid()
		{
			var singleton = Singleton<TComponent>.IsValid(this);
			return singleton;
		}

		/// <summary>
		/// 생성.
		/// </summary>
		public static TComponent Create()
		{
			// 변수로 존재 할 경우.
			if (s_Instance != null)
				return s_Instance;

			// 씬에 존재 할 경우.
			s_Instance = Object.FindAnyObjectByType<TComponent>();
			if (s_Instance != null)
				return s_Instance;

			// 새롭게 생성.
			var type = typeof(TComponent);
			var obj = new GameObject(type.Name);
			s_Instance = obj.AddComponent<TComponent>();
			return s_Instance;
		}

		/// <summary>
		/// 해제.
		/// </summary>
		public static void Dispose()
		{
			if (s_Instance == null)
				return;

			Object.Destroy(s_Instance.gameObject);
		}

		/// <summary>
		/// 싱글톤 인스턴스 여부 반환.
		/// </summary>
		public static bool IsValid(Singleton<TComponent> target)
		{
			if (target == null)
				return false;

			// 싱글톤 인스턴스가 존재하지만 현재 객체가 아닐 경우.
			if (s_Instance != null && target != s_Instance)
				return false;

			return true;
		}
	}
}