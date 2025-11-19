using Crockhead.Core;
using System;
using UnityEngine;


namespace Crockhead.Unity
{
	/// <summary>
	/// 기반 컴포넌트.
	/// </summary>
	public abstract class UnityBehaviour : MonoBehaviour
	{
		#region INSPECTOR
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected virtual void Awake()
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] Awake()");
		}

		/// <summary>
		/// 시작됨.
		/// </summary>
		protected virtual void Start()
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] Start()");
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected virtual void OnDestroy()
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] OnDestroy()");
		}

		/// <summary>
		/// 활성화됨.
		/// </summary>
		protected virtual void OnEnable()
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] OnEnable()");
		}

		/// <summary>
		/// 비활성화됨.
		/// </summary>
		protected virtual void OnDisable()
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] OnDisable()");
		}

		/// <summary>
		/// 재시작.
		/// </summary>
		protected virtual void Reset()
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] Reset()");
		}

		protected virtual void OnRectTransformDimensionsChange()
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] OnRectTransformDimensionsChange()");
		}

		protected virtual void OnBeforeTransformParentChanged()
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] OnBeforeTransformParentChanged()");
		}

		protected virtual void OnTransformParentChanged()
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] OnTransformParentChanged()");
		}

		protected virtual void OnCanvasGroupChanged()
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] OnCanvasGroupChanged()");
		}

		protected virtual void OnCanvasHierarchyChanged()
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] OnCanvasHierarchyChanged()");
		}

		protected virtual void OnDidApplyAnimationProperties()
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] OnDidApplyAnimationProperties()");
		}

		/// <summary>
		/// 유효성 체크.
		/// </summary>
		protected virtual void OnValidate()
		{
			var type = GetType();
			Debug.Log($"[{type.Name}] OnValidate()");
		}

		/// <summary>
		/// 객체 파괴 여부.
		/// </summary>
		public virtual bool IsDestroyed()
		{
			return this == null;
		}

		/// <summary>
		/// 활성화 여부.
		/// </summary>
		public virtual bool IsActive()
		{
			return isActiveAndEnabled;
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

		/// <summary>
		/// 생성.
		/// </ummary>
		public static UnityBehaviour Create(Type componentType, Transform parentTransform)
		{
			if (componentType == null)
				throw new ArgumentNullException(nameof(componentType));
			if (parentTransform == null)
				throw new ArgumentNullException(nameof(parentTransform));
			if (!Reflections.IsBaseClass(componentType, typeof(UnityBehaviour)))
				throw new ArgumentException(nameof(componentType));

			var obj = new GameObject(componentType.Name);
			var node = (UnityBehaviour)obj.AddComponent(componentType);
			node.transform.SetParent(parentTransform, true);
			TransformHelper.ResetTransform(node.transform);
			return node;
		}

		/// <summary>
		/// 애셋을 로드하여 생성.
		/// </summary>
		public static UnityBehaviour CreateFromAsset(Type componentType, string assetPath, AssetPathType assetPathType, Transform parentTransform)
		{
			try
			{
				if (componentType == null)
					throw new ArgumentNullException(nameof(componentType));
				if (string.IsNullOrWhiteSpace(assetPath))
					throw new ArgumentException(nameof(assetPath));
				if (parentTransform == null)
					throw new ArgumentNullException(nameof(parentTransform));
				if (!Reflections.IsBaseClass(componentType, typeof(UnityBehaviour)))
					throw new ArgumentException(nameof(componentType));

				using var assetReader = new AssetReader<GameObject>(assetPath, assetPathType);
				var operation = assetReader.Read();
				if (operation.IsSucceeded)
				{
					var asset = operation.Result;
					var obj = GameObject.Instantiate<GameObject>(asset);
					obj.name = componentType.Name;
					var node = (UnityBehaviour)obj.GetOrAddComponent(componentType);
					node.transform.SetParent(parentTransform, true);
					TransformHelper.ResetTransform(node.transform);
					return node;
				}
				else
				{
					Debug.LogException(operation.Exception);
					throw operation.Exception;
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 애셋을 로드하여 노드 생성. (특성이 설정 된 경우)
		/// </summary>
		public static UnityBehaviour CreateFromAsset(Type componentType, Transform parentTransform)
		{
			try
			{
				if (componentType == null)
					throw new ArgumentNullException(nameof(componentType));
				if (parentTransform == null)
					throw new ArgumentNullException(nameof(parentTransform));
				if (!Reflections.TryGetAttribute<AssetPathAttribute>(componentType, out var assetPathAttribute))
					throw new InvalidOperationException(nameof(componentType));

				var assetPathValue = assetPathAttribute.Value;
				var assetPathType = assetPathAttribute.Type;
				return UnityBehaviour.CreateFromAsset(componentType, assetPathValue, assetPathType, parentTransform);
			}
			catch
			{
				throw;
			}
		}
	}
}