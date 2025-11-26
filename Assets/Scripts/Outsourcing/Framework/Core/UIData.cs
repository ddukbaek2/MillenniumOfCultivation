using UnityEngine;


namespace Outsourcing
{
	/// <summary>
	/// UI 데이터.
	/// <para>미리 애셋으로 저장되어 관리되며, 런타임에는 읽기 전용으로만 사용되는 UI 정보.</para>
	/// <para>모델은 인스턴스 단위로서 모든 정보를 동적으로 가공하기 때문에 이와 무관한 읽기 전용 정보들은 현재 객체로 독립. (like 테이블)</para>
	/// </summary>
	//[CreateAssetMenu(fileName = "UIData", menuName = "Outsourcing/UIData")]
	public abstract class UIData : UIScriptable
	{
		#region INSPECTOR
		[SerializeField] public int Id;
		[SerializeField] public string Name;
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose()
		{
			base.OnDispose();
		}
	}
}