using UnityEngine;
using UnityEngine.EventSystems;


namespace Outsourcing
{
	/// <summary>
	/// UI 기반 컴포넌트 클래스.
	/// </summary>
	public abstract class UIAttachable : UIBehaviour
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		protected sealed override void Awake()
		{
			base.Awake();

			OnCreate();
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected sealed override void OnDestroy()
		{
			OnDispose();

			base.OnDestroy();
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
		/// 생성.
		/// </summary>
		public static TUIAttachable Create<TUIAttachable>() where TUIAttachable : UIAttachable
		{
			var type = typeof(TUIAttachable);
			var obj = new GameObject(type.Name);
			var attachable = obj.AddComponent<TUIAttachable>();
			return attachable;
		}

		/// <summary>
		/// 애셋을 통한 생성.
		/// </summary>
		public static TUIAttachable CreateFromAsset<TUIAttachable>(string assetPath) where TUIAttachable : UIAttachable
		{
			var asset = Resources.Load<GameObject>(assetPath);
			var obj = Object.Instantiate<GameObject>(asset);

			var type = typeof(TUIAttachable);
			obj.name = type.Name;
			var attachable = obj.GetComponent<TUIAttachable>();
			if (attachable == null)
				attachable = obj.AddComponent<TUIAttachable>();

			return attachable;
		}

		/// <summary>
		/// 해제.
		/// </summary>
		public static void Dispose(UIAttachable attachable)
		{
			if (attachable == null)
				return;

			Object.Destroy(attachable);
		}
	}
}