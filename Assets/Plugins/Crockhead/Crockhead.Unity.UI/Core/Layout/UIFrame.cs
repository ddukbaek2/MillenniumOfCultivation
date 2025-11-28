using UnityEngine;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 레이아웃의 위치와 크기 데이터.
	/// </summary>
	public class UIFrame
	{
		/// <summary>
		/// 위치.
		/// </summary>
		private Vector2 m_Position;

		/// <summary>
		/// 크기.
		/// </summary>
		private Vector2 m_Size;

		/// <summary>
		/// 위치 프로퍼티.
		/// </summary>
		public Vector2 Position => m_Position;

		/// <summary>
		/// 크기 프로퍼티.
		/// </summary>
		public Vector2 Size => m_Size;

		/// <summary>
		/// 절반 크기 프로퍼티.
		/// </summary>
		public Vector2 Extents => m_Size * 0.5f;

		/// <summary>
		/// 좌측 위치 프로퍼티.
		/// </summary>
		public float Left => m_Position.x - Extents.x;

		/// <summary>
		/// 우측 위치 프로퍼티.
		/// </summary>
		public float Right => m_Position.x + Extents.x;

		/// <summary>
		/// 상측 위치 프로퍼티.
		/// </summary>
		public float Top => m_Position.y + Extents.y;

		/// <summary>
		/// 하측 위치 프로퍼티.
		/// </summary>
		public float Bottom => m_Position.y - Extents.y;

		/// <summary>
		/// 가로 크기 프로퍼티.
		/// </summary>
		public float Width => m_Size.x;

		/// <summary>
		/// 세로 크기 프로퍼티.
		/// </summary>
		public float Height => m_Size.y;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public UIFrame()
		{
			m_Position = Vector2.zero;
			m_Size = Vector2.zero;
		}

		/// <summary>
		/// 점이 사각형 안에 포함되는지 여부.
		/// </summary>
		public bool Overlaps(Vector2 other)
		{
			return false;
		}

		/// <summary>
		/// 사각형과 사각형이 겹치는지 여부.
		/// </summary>
		public bool Overlaps(UIFrame other)
		{
			return false;
		}
	}
}