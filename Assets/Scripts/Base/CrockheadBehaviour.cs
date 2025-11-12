using UnityEngine;


namespace Crockhead.Unity
{
	/// <summary>
	/// 비헤이비어.
	/// </summary>
	public abstract class CrockheadBehaviour : MonoBehaviour
	{
		#region INSPECTOR
		//[SerializeField] private AudioSource m_AudioSource;
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected virtual void Awake()
		{
		}

		/// <summary>
		/// 활성화됨.
		/// </summary>
		protected virtual void OnEnable()
		{
		}

		/// <summary>
		/// 시작됨.
		/// </summary>
		protected virtual void Start()
		{
		}

		/// <summary>
		/// 비활성화됨.
		/// </summary>
		protected virtual void OnDisable()
		{
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected virtual void OnDestroy()
		{
		}

		/// <summary>
		/// 활성화 여부.
		/// </summary>
		public virtual bool IsActive()
		{
			return isActiveAndEnabled;
		}

#if UNITY_EDITOR
		/// <summary>
		/// 유효성 체크.
		/// </summary>
		protected virtual void OnValidate()
		{
		}

		/// <summary>
		/// 재시작.
		/// </summary>
		protected virtual void Reset()
		{
		}
#endif
		//protected virtual void OnRectTransformDimensionsChange()
		//{
		//}

		protected virtual void OnBeforeTransformParentChanged()
		{
		}

		protected virtual void OnTransformParentChanged()
		{
		}

		protected virtual void OnDidApplyAnimationProperties()
		{
		}

		//protected virtual void OnCanvasGroupChanged()
		//{
		//}

		//protected virtual void OnCanvasHierarchyChanged()
		//{
		//}

		/// <summary>
		/// 객체 파괴 여부.
		/// </summary>
		public bool IsDestroyed()
		{
			return this == null;
		}

		/// <summary>
		/// 자식 트랜스폼에 대한 컴포넌트 반환 혹은 생성 후 반환.
		/// </summary>
		public TComponent GetOrAddComponent<TComponent>() where TComponent : Component
		{
			var component = GetOrAddComponent<TComponent>(string.Empty);
			return component;
		}

		/// <summary>
		/// 자식 트랜스폼에 대한 컴포넌트 반환 혹은 생성 후 반환.
		/// </summary>
		public TComponent GetOrAddComponent<TComponent>(string transformPath) where TComponent : Component
		{
			var component = TransformHelper.GetOrAddComponent<TComponent>(transform, transformPath);
			return component;
		}
	}
}