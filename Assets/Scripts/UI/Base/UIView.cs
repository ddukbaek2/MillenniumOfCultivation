using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 화면.
	/// </summary>
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class UIView : UIBehaviour
	{
		#region INSPECTOR
		[SerializeField] private RectTransform m_RectTransform;
		[SerializeField] private Image m_BackgroundImage;
		#endregion

		/// <summary>
		/// 렉트 트랜스폼 프로퍼티.
		/// </summary>
		public RectTransform RectTransform => m_RectTransform;

		/// <summary>
		/// 백그라운드 이미지 프로퍼티.
		/// </summary>
		public Image BackgroundImage => m_BackgroundImage;

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

			if (m_BackgroundImage == null)
			{
				m_BackgroundImage = TransformHelper.GetOrAddComponent<Image>(transform, "Background");
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
		public TComponent GetOrAddComponent<TComponent>(string transformPath) where TComponent : Component
		{
			var component = TransformHelper.GetOrAddComponent<TComponent>(transform, transformPath);
			return component;
		}
	}
}