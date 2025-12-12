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
		/// 타입 이름 프로퍼티.
		/// </summary>
		public static string TypeName => typeof(TSharedTable).Name;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			Debug.Log($"[{TypeName}] OnCreate()");

			Load();
		}

		/// <summary>
		/// 로드됨.
		/// </summary>
		protected override void OnTableDidLoad()
		{
			base.OnTableDidLoad();

			Debug.Log($"[{TypeName}] OnTableDidLoad()");

			foreach (var record in Collection)
			{
				Debug.Log($"[{TypeName}] Record={record.Id}");
			}
		}

		/// <summary>
		/// 불러오기.
		/// </summary>
		public void Load()
		{
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
	}
}