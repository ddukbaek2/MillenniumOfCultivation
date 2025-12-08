using Crockhead.Table;
using Crockhead.Unity.Table;


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

			var reader = new RecordArrayAssetReader<TRecordable>(this);
			LoadTable(reader);
		}

		/// <summary>
		/// 로드됨.
		/// </summary>
		protected override void OnTableDidLoad()
		{
			base.OnTableDidLoad();
		}
	}
}