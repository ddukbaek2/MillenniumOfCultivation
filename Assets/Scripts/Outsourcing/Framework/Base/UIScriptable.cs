using UnityEngine;


namespace Outsourcing
{
	/// <summary>
	/// UI 기반 스크립터블 오브젝트 클래스.
	/// </summary>
	//[CreateAssetMenu(fileName = "UIScriptable", menuName = "Outsourcing/UIScriptable")]
	public abstract class UIScriptable : ScriptableObject
	{
#if UNITY_EDITOR
		/// <summary>
		/// 활성화됨.
		/// </summary>
		private void OnEnable()
		{
			if (Application.isPlaying)
				return;

			OnCreate();
		}

		/// <summary>
		/// 비활성화됨.
		/// </summary>
		private void OnDisable()
		{
			if (Application.isPlaying)
				return;

			OnDispose();
		}
#endif

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected virtual void OnCreate()
		{
		}

		/// <summary>
		/// 복제됨.
		/// </summary>
		protected virtual void OnClone(UIScriptable original)
		{
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected virtual void OnDispose()
		{
		}

		/// <summary>
		/// 복제.
		/// </summary>
		public UIScriptable Clone()
		{
			var scriptable = Object.Instantiate(this);
			scriptable.OnClone(this);
			return scriptable;
		}

		/// <summary>
		/// 생성.
		/// </summary>
		public static TUIScriptable Create<TUIScriptable>() where TUIScriptable : UIScriptable
		{
			var scriptable = ScriptableObject.CreateInstance<TUIScriptable>();
			scriptable.OnCreate();
			return scriptable; 
		}

		/// <summary>
		/// 애셋을 통한 생성.
		/// </summary>
		public static TUIScriptable CreateFromAsset<TUIScriptable>(string assetPath) where TUIScriptable : UIScriptable
		{
			var asset = Resources.Load<TUIScriptable>(assetPath);
			var scriptable = Object.Instantiate<TUIScriptable>(asset);
			scriptable.OnCreate();
			return scriptable;
		}

		/// <summary>
		/// 복제.
		/// </summary>
		public static TUIScriptable Clone<TUIScriptable>(TUIScriptable original) where TUIScriptable : UIScriptable
		{
			if (original == null)
				return null;

			var scriptable = Object.Instantiate<TUIScriptable>(original);
			scriptable.OnClone(original);
			return scriptable;
		}

		/// <summary>
		/// 해제.
		/// </summary>
		public static void Dispose(UIScriptable scriptable)
		{
			if (scriptable == null)
				return;

			scriptable.OnDispose();
			Object.Destroy(scriptable);
		}
	}
}