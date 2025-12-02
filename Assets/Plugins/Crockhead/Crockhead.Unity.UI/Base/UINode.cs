using Crockhead.Core;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// UI 계층의 기본 객체.
	/// <para>UI 계층 객체의 기반으로 사용되지만 실제 UI 시스템에서 사용되진 않음. (UIView, UIWindow 등을 묶기 위한 방식)</para>
	/// </summary>
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public abstract class UINode : UIBehaviour
	{
		#region INSPECTOR
		[SerializeField] private RectTransform m_RectTransform;
		#endregion

		/// <summary>
		/// 월드 좌표계 기준 사각형 위치.
		/// </summary>
		private static readonly Vector3[] s_FourCornersArray = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };

		/// <summary>
		/// 렉트 트랜스폼 프로퍼티.
		/// </summary>
		public RectTransform RectTransform => m_RectTransform;

		/// <summary>
		/// 컴포넌트 타입의 이름 프로퍼티.
		/// </summary>
		public string ComponentName { private set; get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected sealed override void Awake()
		{
			base.Awake();

			if (IsDestroyed())
				return;

			if (!Application.isPlaying)
				return;

			var type = GetType();
			ComponentName = type.Name;
			OnCreate();
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected sealed override void Start()
		{
			base.Start();

			if (IsDestroyed())
				return;

			if (!Application.isPlaying)
				return;

			OnInitialize();
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected sealed override void OnDestroy()
		{
			base.OnDestroy();

			if (IsDestroyed())
				return;

			if (!Application.isPlaying)
				return;

			OnDispose();
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected virtual void OnCreate()
		{
			if (m_RectTransform == null)
			{
				m_RectTransform = GetComponent<RectTransform>();
			}
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected virtual void OnInitialize()
		{
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected virtual void OnDispose()
		{
		}

		/// <summary>
		/// 앵커 설정.
		/// </summary>
		public void SetAnchor(bool stretched)
		{
			if (stretched)
			{
				RectTransform.anchorMin = Vector2.zero;
				RectTransform.anchorMax = Vector2.one;
				RectTransform.anchoredPosition = Vector2.zero;
				RectTransform.sizeDelta = Vector2.zero;
			}
			else
			{
				// 위치와 크기 조절.
				var parentRectTransform = RectTransform.parent as RectTransform;
				if (parentRectTransform == null)
				{
					var worldCenterAndSize = UINode.GetWorldCenterAndSize(RectTransform);

					RectTransform.anchorMin = Vector2.one * 0.5f;
					RectTransform.anchorMax = Vector2.one * 0.5f;
					RectTransform.localPosition = worldCenterAndSize.Center;
					RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, worldCenterAndSize.Size.x);
					RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, worldCenterAndSize.Size.y);
				}
				else
				{
					var localCenterAndSize = UINode.GetLocalCenterAndSize(RectTransform, parentRectTransform);

					RectTransform.anchorMin = Vector2.one * 0.5f;
					RectTransform.anchorMax = Vector2.one * 0.5f;
					RectTransform.localPosition = localCenterAndSize.Center;
					RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, localCenterAndSize.Size.x);
					RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, localCenterAndSize.Size.y);
				}
			}
		}

		/// <summary>
		/// 월드 좌표계 기준 사각형 위치 배열 반환. (배열 재사용 주의)
		/// </summary>
		protected static Vector3[] GetWorldCorners(RectTransform rectTransform)
		{
			if (rectTransform == null)
				return null;

			rectTransform.GetWorldCorners(s_FourCornersArray);
			return s_FourCornersArray;
		}

		/// <summary>
		/// 대상 트랜스폼의 로컬 좌표계 기준 사각형 위치 배열로 변환하여 반환. (배열 재사용 주의)
		/// </summary>
		protected static Vector3[] GetLocalCorners(RectTransform rectTransform, RectTransform parentRectTransform)
		{
			if (rectTransform == null || parentRectTransform == null)
				return null;

			var fourCornersArray = GetWorldCorners(rectTransform);
			for (var i = 0; i < 4; ++i)
			{
				fourCornersArray[i] = parentRectTransform.InverseTransformPoint(fourCornersArray[i]);
			}

			return fourCornersArray;
		}

		/// <summary>
		/// 코너 배열을 사각형 위치 튜플로 변환.
		/// </summary>
		protected static (Vector3 BottomLeft, Vector3 TopLeft, Vector3 TopRight, Vector3 BottomRight) ConvertCornerToRect(Vector3[] fourCornersArray)
		{
			if (fourCornersArray == null)
				return (Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero);

			return (fourCornersArray[0], fourCornersArray[1], fourCornersArray[2], fourCornersArray[3]);
		}

		/// <summary>
		/// 사각형 위치 튜플을 중점과 크기로 변환.
		/// </summary>
		protected static (Vector3 Center, Vector2 Size) ConvertRectToCenterAndSize((Vector3 BottomLeft, Vector3 TopLeft, Vector3 TopRight, Vector3 BottomRight) rect)
		{
			var center = (rect.BottomLeft + rect.TopRight) * 0.5f;
			var width  = Vector3.Distance(rect.BottomLeft, rect.BottomRight); // 좌하-우하.
			var height = Vector3.Distance(rect.BottomLeft, rect.TopLeft); //좌하-좌상.
			var size = new Vector2(width, height);
			return (center, size);
		}

		/// <summary>
		/// 월드 좌표계 기준 사각형 위치 튜플 반환.
		/// </summary>
		public static (Vector3 BottomLeft, Vector3 TopLeft, Vector3 TopRight, Vector3 BottomRight) GetWorldRect(RectTransform rectTransform)
		{
			if (rectTransform == null)
				return (Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero);

			var worldCorners = UINode.GetWorldCorners(rectTransform);
			var worldRect = UINode.ConvertCornerToRect(worldCorners);
			return worldRect;
		}

		/// <summary>
		/// 로컬 좌표계 기준 사각형 위치 튜플 반환.
		/// </summary>
		public static (Vector3 BottomLeft, Vector3 TopLeft, Vector3 TopRight, Vector3 BottomRight) GetLocalRect(RectTransform rectTransform, RectTransform parentRectTransform)
		{
			if (rectTransform == null)
				return (Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero);

			var localCorners = UINode.GetLocalCorners(rectTransform, parentRectTransform);
			var localRect = UINode.ConvertCornerToRect(localCorners);
			return localRect;
		}

		/// <summary>
		/// 월드 좌표계 기준 중점 위치와 크기 반환.
		/// </summary>
		public static (Vector3 Center, Vector2 Size) GetWorldCenterAndSize(RectTransform rectTransform)
		{
			if (rectTransform == null)
				return (Vector3.zero, Vector2.zero);

			var worldRect = UINode.GetWorldRect(rectTransform);
			var worldCenterAndSize = UINode.ConvertRectToCenterAndSize(worldRect);
			return worldCenterAndSize;
		}

		/// <summary>
		/// 로컬 좌표계 기준 중점 위치와 크기 반환.
		/// </summary>
		public static (Vector3 Center, Vector2 Size) GetLocalCenterAndSize(RectTransform rectTransform, RectTransform parentRectTransform)
		{
			if (rectTransform == null || parentRectTransform == null)
				return (Vector3.zero, Vector2.zero);

			var localRect = UINode.GetLocalRect(rectTransform, parentRectTransform);
			var localCenterAndSize = UINode.ConvertRectToCenterAndSize(localRect);
			return localCenterAndSize;
		}

		/// <summary>
		/// 월드 좌표를 스크린 좌표로 변환.
		/// </summary>
		public static Vector2 WorldToScreenPoint(Vector3 worldPoint, Canvas canvas)
		{
			if (canvas == null)
				return Vector3.zero;

			var camera = canvas?.worldCamera ?? null;
			var screenPoint = RectTransformUtility.WorldToScreenPoint(camera, worldPoint);
			//var screenPoint = camera.WorldToScreenPoint(worldPoint);
			return screenPoint;
		}

		/// <summary>
		/// 스크린 좌표를 월드 좌표로 변환.
		/// </summary>
		public static Vector3 ScreenToWorldPoint(Vector2 screenPoint, float depth, Camera camera)
		{
			if (camera == null)
				return Vector3.zero;

			var worldPoint = camera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, depth));
			return worldPoint;
		}

		/// <summary>
		/// 대상 렉트 트랜스폼의 중점이 원점인 로컬 좌표를 스크린 좌표로 변환.
		/// </summary>
		public static Vector2 LocalToScreenPoint(RectTransform rectTransform, Vector2 localPoint, Canvas canvas)
		{
			if (rectTransform == null)
				return Vector2.zero;

			var worldPoint = rectTransform.position;
			var screenPoint = UINode.WorldToScreenPoint(worldPoint, canvas);
			return screenPoint;
		}

		/// <summary>
		/// 스크린 좌표를 대상 렉트 트랜스폼의 중점이 원점인 로컬 좌표로 변환.
		/// </summary>
		public static Vector2 ScreenToLocalPoint(RectTransform rectTransform, Vector2 screenPoint, Canvas canvas)
		{
			if (rectTransform == null)
				return Vector2.zero;

			var camera = canvas?.worldCamera ?? null;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, camera, out var localPoint))
				return Vector2.zero;
			return localPoint;
		}

		/// <summary>
		/// 자식 트랜스폼에 대한 컴포넌트 반환 혹은 생성 후 반환.
		/// </summary>
		public Component GetOrAddComponent(Type componentType, string transformPath)
		{
			//var component = TransformHelper.GetOrAddComponent(transform, transformPath);
			//return component;
			throw new NotImplementedException();
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
		public static UINode Create(Type nodeType, Transform parentTransform)
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
			TransformHelper.ResetTransform(node.transform);
			return node;
		}

		/// <summary>
		/// 애셋을 로드하여 노드 생성.
		/// </summary>
		public static UINode CreateFromAsset(Type nodeType, string assetPath, AssetPathType assetPathType, Transform parentTransform)
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
				
				using var assetLoader = new AssetLoader<GameObject>(assetPath, assetPathType);
				assetLoader.Load();
				var asset = assetLoader.Asset;
				var obj = GameObject.Instantiate<GameObject>(asset);
				obj.name = nodeType.Name;
				var node = (UINode)obj.GetOrAddComponent(nodeType);
				node.transform.SetParent(parentTransform, true);
				TransformHelper.ResetTransform(node.transform);
				return node;
			}
			catch
			{
				Debug.LogError($"[UINode] {assetPath}");
				throw;
			}
		}

		/// <summary>
		/// 애셋을 로드하여 노드 생성. (특성이 설정 된 경우)
		/// </summary>
		public static UINode CreateFromAsset(Type nodeType, Transform parentTransform)
		{
			try
			{
				if (nodeType == null)
					throw new ArgumentNullException(nameof(nodeType));
				if (parentTransform == null)
					throw new ArgumentNullException(nameof(parentTransform));
				if (!Reflections.TryGetAttribute<AssetPathAttribute>(nodeType, out var assetPathAttribute))
					throw new InvalidOperationException(nameof(nodeType));

				var assetPathValue = assetPathAttribute.Value;
				var assetPathType = assetPathAttribute.Type;
				return UINode.CreateFromAsset(nodeType, assetPathValue, assetPathType, parentTransform);
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 노드 생성.
		/// </summary>
		public static TUINode Create<TUINode>(Transform parentTransform) where TUINode : UINode
		{
			try
			{
				var nodeType = typeof(TUINode);
				var node = (TUINode)UINode.Create(nodeType, parentTransform);
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
		public static TUINode CreateFromAsset<TUINode>(string assetPath, AssetPathType assetPathType, Transform parentTransform) where TUINode : UINode
		{
			try
			{
				var nodeType = typeof(TUINode);
				var node = (TUINode)UINode.CreateFromAsset(nodeType, assetPath, assetPathType, parentTransform);
				return node;
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 애셋을 로드하여 노드 생성. (특성이 설정 된 경우)
		/// </summary>
		public static TUINode CreateFromAsset<TUINode>(Transform parentTransform) where TUINode : UINode
		{
			try
			{
				var nodeType = typeof(TUINode);
				var node = (TUINode)UINode.CreateFromAsset(nodeType, parentTransform);
				return node;
			}
			catch
			{
				throw;
			}
		}
	}
}