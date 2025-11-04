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

			if (m_RectTransform == null)
			{
				m_RectTransform = GetComponent<RectTransform>();
			}

			if (m_BackgroundImage == null)
			{
				var backgroundTransform = transform.Find("Background") as RectTransform;
				if (backgroundTransform != null)
				{
					m_BackgroundImage = backgroundTransform.GetComponent<Image>();
				}
				else
				{
					var obj = new GameObject("Background");
					obj.transform.SetParent(transform, true);
					backgroundTransform = obj.GetComponent<RectTransform>();
					m_BackgroundImage = obj.AddComponent<Image>();
				}
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
	}
}