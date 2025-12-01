using Crockhead.Core;
using Crockhead.Unity;
using MillenniumOfCultivation.Battle;
using System.Collections;
using UnityEngine;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 전투 매니저.
	/// </summary>
	public class BattleManager : SharedClass<BattleManager>
	{
		/// <summary>
		/// 전투 처리기.
		/// </summary>
		private BattleProcess m_Process;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public BattleManager() : base()
		{
			m_Process = new BattleProcess();
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			base.OnDispose(explicitDisposing);
		}

		/// <summary>
		/// 처리.
		/// </summary>
		IEnumerator Process()
		{
			while (true)
			{
				yield return null;
			}
		}

		/// <summary>
		/// 시작.
		/// </summary>
		public void Start()
		{
			var task = TaskHelper.StartForeground(Process());
			//CoroutineHelper.WaitForTaskCompletion(task);
		}
	}
}