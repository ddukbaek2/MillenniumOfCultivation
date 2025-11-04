using Crockhead.Core;
using Crockhead.Unity;
using UnityEngine;
using UnityEngine.EventSystems;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// UI 매니저.
	/// </summary>
	[RequireComponent(typeof(RectTransform))]
	public class UIManager : UIBehaviour
	{
		/// <summary>
		/// 공유 인스턴스 프로퍼티.
		/// </summary>
		public static UIManager SharedInstance
		{
			get
			{
				if (SharedInstances.TryGet<UIManager>(out var sharedInstance))
					return sharedInstance;

				sharedInstance = GameObject.FindFirstObjectByType<UIManager>();
				if (sharedInstance != null)
					SharedInstances.Set<UIManager>(sharedInstance);
				return sharedInstance;
			}
		}

		/// <summary>
		/// 인스펙터 프로퍼티.
		/// </summary>
		#region INSEPCTOR
		[SerializeField] private Canvas m_Canvas;
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			SharedInstances.Set<UIManager>(this);
			GameObject.DontDestroyOnLoad(gameObject);

			if (m_Canvas == null)
			{
				m_Canvas = transform.Find("Canvas").GetComponent<Canvas>();
			}
		}

		/// <summary>
		/// 뷰 생성.
		/// </summary>
		public TUIView CreateView<TUIView>(string assetPath, UIView parentView = null) where TUIView : UIView
		{
			try
			{
				using var assetReader = new AssetReader<GameObject>(assetPath, AssetPathType.Resources);
				var operation = assetReader.Read();
				if (operation.IsSucceeded)
				{
					var asset = operation.Result;
					var obj = GameObject.Instantiate<GameObject>(asset);
					var view = obj.GetOrAddComponent<TUIView>();

					var parentTransform = default(RectTransform);
					if (parentView != null)
					{
						parentTransform = parentView.RectTransform;
					}
					else
					{
						parentTransform = m_Canvas.GetComponent<RectTransform>();
					}
					
					view.RectTransform.SetParent(parentTransform, true);
					return view;
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
	}
}