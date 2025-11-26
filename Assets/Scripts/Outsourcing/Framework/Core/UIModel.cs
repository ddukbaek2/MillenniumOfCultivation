using UnityEngine;


namespace Outsourcing
{
	/// <summary>
	/// UI 모델.
	/// <para>화면 내에서 데이터만 존재하는 상태적 항목 단위.</para>
	/// <para>정보와 상태 변경 관련만 기능적으로 외부 호출이 가능하도록 구현한다.</para>
	/// <para>프레젠터가 모델과 뷰를 알고 사용하는 형태로 모델과 뷰는 자신이 가진 정보와 기능 외에는 아무것도 모른다.</para>
	/// </summary>
	//[CreateAssetMenu(fileName = "UIModel", menuName = "Outsourcing/UIModel")]
	public abstract class UIModel : UIScriptable
	{
		#region INSPECTOR
		//[SerializeField] private UIData m_Data;
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