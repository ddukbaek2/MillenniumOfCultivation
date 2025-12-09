using Crockhead.Core;
using UnityEngine;


namespace MillenniumOfCultivation
{
	/// <summary>
	/// 맵.
	/// </summary>
	public class Map<T> : Disposable
	{
		/// <summary>
		/// 크기.
		/// </summary>
		public Vector2Int Size { get; }

		/// <summary>
		/// 데이터.
		/// </summary>
		public T[] Data { get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Map(Vector2Int size) : base()
		{
			Size = size;
			Data = new T[size.x * size.y];
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{

		}

		/// <summary>
		/// 빌드됨.
		/// </summary>
		protected virtual void OnBuild(T[] map)
		{

		}

		/// <summary>
		/// 생성.
		/// </summary>
		public void Generate()
		{
			var map = Data;
			OnBuild(map);
		}
	}
}