using System.Threading.Tasks;


namespace Crockhead.Unity
{
	/// <summary>
	/// 대기.
	/// </summary>
	public class YieldTask : Yield
	{
		/// <summary>
		/// 태스크.
		/// </summary>
		private Task m_Task;

		/// <summary>
		/// 대기 여부 프로퍼티. (참이면 계속 대기)
		/// </summary>
		public override bool KeepWaiting => m_Task != null && !m_Task.IsCompleted;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public YieldTask(Task task) : base()
		{
			m_Task = task;
		}
	}
}