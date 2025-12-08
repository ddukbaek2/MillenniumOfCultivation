using Crockhead.Unity;
using Crockhead.Unity.UI;
using System.Collections;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 대화 뷰.
	/// </summary>
	[AssetPath("Assets/Resources/UI/UIDialogView.prefab", AssetPathType.Resources)]
	public class UIDialogView : UIPanelView
	{
		#region INSPECTOR
		[SerializeField] private UILabelView m_NameLabel;
		[SerializeField] private UILabelView m_ContentLabel;
		#endregion

		private Coroutine m_Coroutine;
		private string m_Text;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			BackgroundColor = new Color32(255, 178, 0, 255);

			if (m_NameLabel == null)
			{
				m_NameLabel = GetOrAddComponent<UILabelView>("Name/Label");
			}

			if (m_ContentLabel == null)
			{
				m_ContentLabel = GetOrAddComponent<UILabelView>("Content/Label");
			}
		}

		private void Update()
		{
			
		}

		/// <summary>
		/// 비우기.
		/// </summary>
		public void Clear()
		{
			m_ContentLabel.text = string.Empty;
			CoroutineHelper.StopCoroutine(m_Coroutine);
			m_Coroutine = null;
		}

		/// <summary>
		/// 추가.
		/// </summary>
		public void Add(string text)
		{
			IEnumerator Process()
			{
				var time = 0f;
				while (time < 1f)
				{
					yield return null;
				}

				yield break;
			}

			m_ContentLabel.text += text;

			CoroutineHelper.StartCoroutine(Process());
		}

		/// <summary>
		/// 즉시 완료.
		/// </summary>
		public void Complete()
		{
		}
	}
}