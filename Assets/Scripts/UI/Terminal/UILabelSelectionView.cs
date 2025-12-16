using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 레이블 선택 영역 표시 뷰.
	/// </summary>
	[RequireComponent(typeof(CanvasRenderer))]
	public class UILabelSelectionView : UIGraphicView
	{
		#region INSPECTOR
		[SerializeField] private UILabelView m_LabelView;
		#endregion

		/// <summary>
		/// 시작 선택 인덱스.
		/// </summary>
		private int m_StartSelectionIndex; 

		/// <summary>
		/// 종료 선택 인덱스.
		/// </summary>
		private int m_EndSelectionIndex;

		/// <summary>
		/// 레이블 뷰 프로퍼티.
		/// </summary>
		public UILabelView LabelView
		{
			set
			{
				m_LabelView = value;

				// 정점 정보 갱신.
				SetVerticesDirty();
			}
			get
			{
				return m_LabelView;
			}
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			m_StartSelectionIndex = -1; // inclusive
			m_EndSelectionIndex = -1; // exclusive
		}

		/// <summary>
		/// 선택 영역 설정.
		/// </summary>
		public void SetSelection(int startSelectionIndex, int endSelectionIndex)
		{
			m_StartSelectionIndex = startSelectionIndex;
			m_EndSelectionIndex = endSelectionIndex;

			// 정점 정보 갱신.
			SetVerticesDirty();
		}

		/// <summary>
		/// 정점 정보 갱신됨.
		/// </summary>
		protected override void OnPopulateMesh(VertexHelper vertexHelper)
		{
			base.OnPopulateMesh(vertexHelper);

			if (m_LabelView == null)
				return;
			if (m_StartSelectionIndex < 0 || m_EndSelectionIndex < 0 || m_EndSelectionIndex < m_StartSelectionIndex)
				return;

			var textInfo = m_LabelView.textInfo;
			var characterCount = textInfo.characterCount;
			if (characterCount <= 0)
				return;

			var startIndex = Mathf.Clamp(m_StartSelectionIndex, 0, characterCount);
			var endIndex = Mathf.Clamp(m_EndSelectionIndex, 0, characterCount);
			if (endIndex <= startIndex)
				return;

			var index = startIndex;
			while (index < endIndex)
			{
				if (index >= characterCount)
					break;

				var line = textInfo.characterInfo[index].lineNumber;
				var left = float.PositiveInfinity;
				var right = float.NegativeInfinity;
				var top = float.NegativeInfinity;
				var bottom = float.PositiveInfinity;

				var current = index;
				while (current < endIndex && current < characterCount && textInfo.characterInfo[current].lineNumber == line)
				{
					var characterInfo = textInfo.characterInfo[current];

					switch (characterInfo.character)
					{
						case '\n':
						case '\r':
							{
								break;
							}

						default:
							{
								left = Mathf.Min(left, characterInfo.bottomLeft.x);
								right = Mathf.Max(right, characterInfo.bottomRight.x);
								top = Mathf.Max(top, characterInfo.topLeft.y);
								bottom = Mathf.Min(bottom, characterInfo.bottomLeft.y);
								break;
							}
					}

					++current;
				}

				if (!float.IsInfinity(left) && right > left && top > bottom)
				{
					UILabelSelectionView.AddQuad(vertexHelper, RectTransform, m_LabelView.RectTransform, left, bottom, right, top, color);
				}

				index = current;
			}
		}

		/// <summary>
		/// 사각형 추가.
		/// </summary>
		public static void AddQuad(VertexHelper vertexHelper,
			RectTransform currentRectTransform, RectTransform targetRectTransform, 
			float xMin, float yMin, float xMax, float yMax,
			Color color)
		{
			// 레이블 뷰의 문자열 오프셋을 레이블 뷰의 로컬 좌표계로 변경.
			var w0 = targetRectTransform.TransformPoint(new Vector3(xMin, yMin, 0f));
			var w1 = targetRectTransform.TransformPoint(new Vector3(xMin, yMax, 0f));
			var w2 = targetRectTransform.TransformPoint(new Vector3(xMax, yMax, 0f));
			var w3 = targetRectTransform.TransformPoint(new Vector3(xMax, yMin, 0f));

			// 레이블 뷰의 로컬 좌표계를 현재 뷰의 오프셋으로 변경.
			var p0 = currentRectTransform.InverseTransformPoint(w0);
			var p1 = currentRectTransform.InverseTransformPoint(w1);
			var p2 = currentRectTransform.InverseTransformPoint(w2);
			var p3 = currentRectTransform.InverseTransformPoint(w3);

			// 정점 추가.
			var vertexStartIndex = vertexHelper.currentVertCount;
			var vertex = UIVertex.simpleVert;
			vertex.color = color;
			vertex.position = p0;
			vertexHelper.AddVert(vertex);
			vertex.position = p1;
			vertexHelper.AddVert(vertex);
			vertex.position = p2;
			vertexHelper.AddVert(vertex);
			vertex.position = p3;
			vertexHelper.AddVert(vertex);

			// 삼각형 인덱스 추가.
			vertexHelper.AddTriangle(vertexStartIndex + 0, vertexStartIndex + 1, vertexStartIndex + 2);
			vertexHelper.AddTriangle(vertexStartIndex + 0, vertexStartIndex + 2, vertexStartIndex + 3);
		}
	}
}