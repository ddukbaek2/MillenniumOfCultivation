using Crockhead.Unity;
using Crockhead.Unity.UI;
using System.Collections;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 대화 화면 컨트롤러.
	/// </summary>
	[UIViewBinding(typeof(UIDialogView), "Assets/Resources/UI/UIDialogView.prefab", AssetPathType.Resources)]
	public class UIDialogController : UIController<UIDialogView>
	{
		/// <summary>
		/// 뷰 로드됨.
		/// </summary>
		protected override void OnViewDidLoad()
		{
			base.OnViewDidLoad();
		}

		/// <summary>
		/// 처리.
		/// </summary>
		private IEnumerator ProcessSmoothWrite(int dialogTableId)
		{
			//DialogTable
			var time = 0f;
			while (time < 1f)
			{
				yield return null;
			}

			yield break;
		}

		/// <summary>
		/// 비우기.
		/// </summary>
		private void Clear()
		{
			View.Clear();
		}

		/// <summary>
		/// 텍스트 추가.
		/// </summary>
		private void Add(string text)
		{
			View.Add(text);
		}

		/// <summary>
		/// 다이얼로그 실행.
		/// </summary>
		public void StartDialog(int dialogTableId)
		{
			CoroutineHelper.StartCoroutine(ProcessSmoothWrite(dialogTableId));
		}

		/// <summary>
		/// 즉시 완료.
		/// </summary>
		public void Complete()
		{
		}

		/// <summary>
		/// 연결된 다음 다이얼로그 실행.
		/// </summary>
		public void Next()
		{
		}
	}
}