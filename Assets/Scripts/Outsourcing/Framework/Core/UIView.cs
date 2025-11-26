namespace Outsourcing
{
	/// <summary>
	/// UI 뷰.
	/// <para>화면 내에서 시각적으로 구별되는 항목 단위. (패널, 팝업, 그룹, 동적 아이템 등)</para>
	/// <para>실제 화면에 출력되는 내용이나 사용자 조작만 기능적으로 외부 호출이 가능하도록 구현한다.</para>
	/// <para>프레젠터가 모델과 뷰를 알고 사용하는 형태로 모델과 뷰는 자신이 가진 정보와 기능 외에는 아무것도 모른다.</para>
	/// </summary>
	public abstract class UIView : UIAttachable
	{
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

		/// <summary>
		/// 갱신됨.
		/// </summary>
		protected virtual void OnRefreshed()
		{
		}

		/// <summary>
		/// 갱신.
		/// </summary>
		public void Refresh()
		{
			OnRefreshed();
		}
	}
}