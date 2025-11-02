using System.Collections.Generic;


namespace MillenniumOfCultivation.Battle
{
	/// <summary>
	/// 조작 주체.
	/// </summary>
	public abstract class Controller : Identifiable
	{
		/// <summary>
		/// 현재 뽑지 않은 카드 목록.
		/// </summary>
		private List<Card> m_Deck;

		/// <summary>
		/// 현재 사용 가능한 카드 목록.
		/// </summary>
		private List<Card> m_Background;

		/// <summary>
		/// 사용 중인 카드 목록.
		/// </summary>
		private List<Card> m_Forground;

		/// <summary>
		/// 사용이 끝난 카드 목록.
		/// </summary>
		private List<Card> m_Grave;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Controller(ulong instanceId) : base(instanceId)
		{
			m_Deck = new List<Card>();
			m_Background = new List<Card>();
			m_Forground = new List<Card>();
			m_Grave = new List<Card>();
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			base.OnDispose(explicitDisposing);
		}
	}
}