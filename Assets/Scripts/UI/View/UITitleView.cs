using Crockhead.Unity;
using Crockhead.Unity.UI;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 타이틀 뷰.
	/// </summary>
	[AssetPath("Assets/Resources/UI/UITitleView.prefab", AssetPathType.Resources)]
	public class UITitleView : UIPanelView
	{
		#region INSPECTOR
		//[SerializeField] private Image m_OverlayImage;
		//[SerializeField] private RawImage m_LogoImage;
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			if (IsDestroyed())
				return;

			if (!Application.isPlaying)
				return;

			BackgroundColor = new Color32(255, 178, 0, 255);
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

		///// <summary>
		///// 애니메이션 시작.
		///// </summary>
		//public async Task StartAnimation(Action completion)
		//{
		//	static IEnumerator BattleProcess(Image overlayImage, Action completion)
		//	{
		//		overlayImage.color = Color.black;
		//		var tween = overlayImage.DOColor(Color.clear, 2f);
		//		yield return tween.WaitForCompletion();
		//		completion?.Invoke();
		//	}

		//	await TaskHelper.StartForeground(BattleProcess(m_OverlayImage, completion));
		//}
	}
}