using Crockhead.Table;
using Crockhead.Unity.Table;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 사운드 테이블.
	/// </summary>
	public class SoundTable : SharedTable<SoundTable, SoundTableRecord>
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			var reader = new RecordArrayAssetReader<SoundTableRecord>(this);
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