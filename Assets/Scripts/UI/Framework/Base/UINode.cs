using Crockhead.Core;
using Crockhead.Unity;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// UI 계층의 기본 객체.
	/// <para>UI 계층 객체의 기반으로 사용되지만 실제 UI 시스템에서 사용되진 않음. (UIView, UIWindow 등을 묶기 위한 방식)</para>
	/// </summary>
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class UINode : UIBehaviour
	{
		#region INSPECTOR
		[SerializeField] private RectTransform m_RectTransform;
		#endregion

		/// <summary>
		/// 렉트 트랜스폼 프로퍼티.
		/// </summary>
		public RectTransform RectTransform => m_RectTransform;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			// 하나의 게임 오브젝트에는 뷰 클래스는 하나만 붙어 있어야 한다.
			var existViews = GetComponents<UIView>();
			foreach (var existView in existViews)
			{
				if (this == existView)
					continue;

				Debug.LogError($"[UIView] Removal Old UIView: {existView}");
				GameObject.Destroy(existView);
			}

			if (m_RectTransform == null)
			{
				m_RectTransform = GetComponent<RectTransform>();
			}
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void Start()
		{
			base.Start();
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected override void OnDestroy()
		{
			base.OnDestroy();
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
		/// 노드 생성.
		/// </summary>
		public static UINode CreateNode(Type nodeType, Transform parentTransform)
		{
			if (nodeType == null)
				throw new ArgumentNullException(nameof(nodeType));
			if (parentTransform == null)
				throw new ArgumentNullException(nameof(parentTransform));
			if (!Reflections.IsBaseClass(nodeType, typeof(UINode)))
				throw new ArgumentException(nameof(nodeType));

			var obj = new GameObject(nodeType.Name);
			var node = (UINode)obj.AddComponent(nodeType);
			node.transform.SetParent(parentTransform, true);
			node.RectTransform.localPosition = Vector3.zero;
			node.RectTransform.localScale = Vector3.one;
			node.RectTransform.localRotation = Quaternion.identity;
			return node;
		}

		/// <summary>
		/// 애셋을 로드하여 노드 생성.
		/// </summary>
		public static UINode CreateNodeFromAsset(Type nodeType, string assetPath, AssetPathType assetPathType, Transform parentTransform)
		{
			try
			{
				if (nodeType == null)
					throw new ArgumentNullException(nameof(nodeType));
				if (string.IsNullOrWhiteSpace(assetPath))
					throw new ArgumentException(nameof(assetPath));
				if (parentTransform == null)
					throw new ArgumentNullException(nameof(parentTransform));
				if (!Reflections.IsBaseClass(nodeType, typeof(UINode)))
					throw new ArgumentException(nameof(nodeType));
				
				using var assetReader = new AssetReader<GameObject>(assetPath, assetPathType);
				var operation = assetReader.Read();
				if (operation.IsSucceeded)
				{
					var asset = operation.Result;
					var obj = GameObject.Instantiate<GameObject>(asset);
					obj.name = nodeType.Name;
					var node = (UINode)obj.GetOrAddComponent(nodeType);
					node.RectTransform.SetParent(parentTransform, true);
					node.RectTransform.localPosition = Vector3.zero;
					node.RectTransform.localScale = Vector3.one;
					node.RectTransform.localRotation = Quaternion.identity;
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
		/// 노드 생성.
		/// </summary>
		public static TUINode CreateNode<TUINode>(Transform parentTransform) where TUINode : UINode
		{
			try
			{
				var nodeType = typeof(TUINode);
				var node = (TUINode)UINode.CreateNode(nodeType, parentTransform);
				return node;
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 애셋을 로드하여 노드 생성.
		/// </summary>
		public static TUINode CreateNodeFromAsset<TUINode>(string assetPath, AssetPathType assetPathType, Transform parentTransform) where TUINode : UINode
		{
			try
			{
				var nodeType = typeof(TUINode);
				var node = (TUINode)UINode.CreateNodeFromAsset(nodeType, assetPath, assetPathType, parentTransform);
				return node;
			}
			catch
			{
				throw;
			}
		}
	}
}