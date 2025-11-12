using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 동적 항목.
	/// </summary>
	public class UIItemView : UIView
	{
		#region INSPECTOR
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			if (IsDestroyed())
				return;

			BackgroundColor = Color.clear;
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void Start()
		{
			base.Start();

			//// 크기 설정.
			//RectTransform.anchoredPosition3D = Vector3.zero;
			//RectTransform.sizeDelta = Vector2.zero;
			//RectTransform.anchorMin = Vector2.zero;
			//RectTransform.anchorMax = Vector2.one;
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		/// <summary>
		/// 큐에 들어감.
		/// </summary>
		protected virtual void OnEnqueued()
		{
		}

		/// <summary>
		/// 큐에서 나옴.
		/// </summary>
		protected virtual void OnDequeued()
		{
		}
	}
}