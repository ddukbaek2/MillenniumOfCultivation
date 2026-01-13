using UnityEngine;


namespace MillenniumOfCultivation
{
	//[CreateAssetMenu()]
	public class PlayerData : ScriptableObject
	{
		#region INSEPCTOR
		[SerializeField] private int Id;
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		public PlayerData() : base()
		{
		}
	}
}