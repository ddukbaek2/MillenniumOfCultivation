using Crockhead.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Crockhead.Unity
{
	/// <summary>
	/// 실행 큐.
	/// </summary>
	public class DispatchQueue : SharedClass<DispatchQueue>
	{
		/// <summary>
		/// 실행 단위.
		/// </summary>
		public readonly struct DispatchItem
		{
			public Func<Task> TaskFactory { get; }
			public TaskCompletionSource<bool> TaskCompletionSource { get; }

			public DispatchItem(Func<Task> task)
			{
				TaskFactory = task;
				TaskCompletionSource = new TaskCompletionSource<bool>();
			}
		}


		private Queue<DispatchItem> m_Queue;
		private bool m_IsProcessing;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public DispatchQueue() : base()
		{
			m_Queue = new Queue<DispatchItem>();
			m_IsProcessing = false;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			base.OnDispose(explicitDisposing);
		}

		/// <summary>
		/// 처리 큐.
		/// </summary>
		private IEnumerator ProcessQueue()
		{
			m_IsProcessing = true;

			while (m_Queue.Count > 0)
			{
				var item = m_Queue.Dequeue();
				var failure = default(Exception);
				var task = default(Task);

				try
				{
					// 작업 생성.
					task = item.TaskFactory();
				}
				catch (Exception exception)
				{
					failure = exception;
				}

				// 오류가 없고 작업이 존재할 경우.
				if (failure == null && task != null)
				{
					while (!task.IsCompleted)
						yield return null;

					// 실패.
					if (task.IsFaulted)
					{
						failure = task.Exception;
					}
					// 취소.
					else if (task.IsCanceled)
					{
						item.TaskCompletionSource.SetCanceled();
						continue;
					}
				}

				// 오류 발생.
				if (failure != null)
				{
					item.TaskCompletionSource.SetException(failure);
				}
				// 완료.
				else
				{
					item.TaskCompletionSource.SetResult(true);
				}
			}

			m_IsProcessing = false;
		}

		/// <summary>
		/// 실행.
		/// </summary>
		public Task RunAsync(Func<Task> task)
		{
			var work = new DispatchItem(task);
			m_Queue.Enqueue(work);

			if (!m_IsProcessing)
			{
				CoroutineHelper.StartCoroutine(ProcessQueue());
			}
			return work.TaskCompletionSource.Task;
		}

		/// <summary>
		/// 실행.
		/// </summary>
		public Task RunAsync(Action action)
		{
			if (action == null)
				throw new ArgumentNullException(nameof(action));

			return RunAsync(() =>
			{
				action.Invoke();
				return Task.CompletedTask;
			});
		}
	}
}