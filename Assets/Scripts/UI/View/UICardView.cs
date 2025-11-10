using DG.Tweening;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 카드 항목.
	/// </summary>
	public class UICardView : UIItemView
	{
		#region INSPECTOR
		//[SerializeField]
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			if (IsDestroyed())
				return;
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