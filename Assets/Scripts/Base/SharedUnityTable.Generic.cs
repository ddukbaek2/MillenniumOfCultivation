using Crockhead.Table;
using Crockhead.Unity.Table;
using System;
using UnityEngine;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 공유 테이블.
	/// </summary>
	public abstract class SharedUnityTable<TSharedTable, TRecordable> : SharedTable<TSharedTable, TRecordable>
		where TSharedTable : SharedTable<TSharedTable, TRecordable>, new()
		where TRecordable : IRecordable
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			Debug.Log("[SharedUnityTable] OnCreate()");

			try
			{
				var reader = new RecordArrayAssetReader<TRecordable>(this);
				reader.Read();
				LoadTable(reader);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		/// <summary>
		/// 로드됨.
		/// </summary>
		protected override void OnTableDidLoad()
		{
			base.OnTableDidLoad();

			Debug.Log("[SharedUnityTable] OnTableDidLoad()");

			foreach (var record in Collection)
			{
				Debug.Log($"[{GetType().Name}] Record={record.Id}");
			}
		}
	}
}