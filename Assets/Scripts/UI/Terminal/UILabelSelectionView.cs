using UnityEngine;
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

			m_StartSelectionIndex = -1;
			m_EndSelectionIndex = -1;

			if (m_LabelView == null)
			{
				m_LabelView = GetComponent<UILabelView>();
			}
		}

		/// <summary>
		/// 선택 영역 설정.
		/// </summary>
		public void SetSelection(int startSelectionIndex, int endSelectionIndex)
		{
			if (startSelectionIndex < 0 || endSelectionIndex < 0)
			{
				m_StartSelectionIndex = -1;
				m_EndSelectionIndex = -1;
			}
			else if (endSelectionIndex < startSelectionIndex)
			{
				m_StartSelectionIndex = endSelectionIndex;
				m_EndSelectionIndex = startSelectionIndex;
			}
			else
			{
				m_StartSelectionIndex = startSelectionIndex;
				m_EndSelectionIndex = endSelectionIndex;
			}

			// 정점 정보 갱신.
			SetVerticesDirty();
		}

		/// <summary>
		/// 정점 정보 갱신됨.
		/// </summary>
		protected override void OnPopulateMesh(VertexHelper vertexHelper)
		{
			// 정점 비우기.
			base.OnPopulateMesh(vertexHelper);

			// 선택 영역 메쉬 생성.
			RebuildSelection(vertexHelper, this, m_LabelView, m_StartSelectionIndex, m_EndSelectionIndex);
		}

		/// <summary>
		/// 선택 영역 표시를 위한 메쉬 생성.
		/// </summary>
		protected static void RebuildSelection(VertexHelper vertexHelper, UIGraphicView graphicView, UILabelView labelView, int startSelectionIndex, int endSelectionIndex)
		{
			// 대상이 되는 레이블 뷰가 없을 경우 중단.
			if (labelView == null)
				return;

			// 선택 구간이 음수일 경우와 시작 종료 위치가 비정상일 경우 선택 기능을 사용하지 않는 상태이므로 중단.
			if (startSelectionIndex < 0 || endSelectionIndex < 0 || endSelectionIndex < startSelectionIndex)
				return;

			// 레이블 뷰에서 출력되는 문자열이 없을 경우 중단.
			var textInfo = labelView.textInfo;
			var characterCount = textInfo.characterCount;
			if (characterCount <= 0)
				return;

			// 선택 구간이 실제 문자열 구간을 벗어나지 않도록 정규화.
			startSelectionIndex = Mathf.Clamp(startSelectionIndex, 0, characterCount - 1);
			endSelectionIndex = Mathf.Clamp(endSelectionIndex, 0, characterCount - 1);

			// 정규화된 선택 구간의 시작 종료 위치가 비정상일 경우 선택 기능 출력에 문제가 생기므로 중단.
			if (endSelectionIndex < startSelectionIndex)
				return;

			var vertices = new Vector3[4];
			foreach (var lineInfo in textInfo.lineInfo)
			{
				//// 현재 라인이 보이지 않는 라인이라면 건너뛰기.
				//if (lineInfo.firstVisibleCharacterIndex < 0 || lineInfo.lastVisibleCharacterIndex < 0)
				//	continue;

				// 현재 라인이 선택 영역이 겹치지 않는 라인이라면 건너뛰기.
				var intersects = startSelectionIndex <= lineInfo.lastCharacterIndex && lineInfo.firstCharacterIndex <= endSelectionIndex;
				if (!intersects)
					continue;

				// 현재 라인의 선택 구간을 실제 보여지는 구간으로 설정.
				//var startLineIndex = Mathf.Max(startSelectionIndex, lineInfo.firstVisibleCharacterIndex);
				//var endLineIndex   = Mathf.Min(endSelectionIndex, lineInfo.lastVisibleCharacterIndex);

				// 현재 라인의 선택 구간을 설정.
				var startLineIndex = Mathf.Max(startSelectionIndex, lineInfo.firstCharacterIndex);
				var endLineIndex   = Mathf.Min(endSelectionIndex, lineInfo.lastCharacterIndex);

				// 현재 라인에서는 실제로 한글자도 보여지지 않는다면 건너뛰기.
				if (startLineIndex > endLineIndex)
					continue;

				// 현재 라인의 선택 구간에 대한 글자 정보를 가져옴.
				var startCharacterInfo = textInfo.characterInfo[startLineIndex];
				var endCharacterInfo = textInfo.characterInfo[endLineIndex];

				// 첫 글자의 좌하 좌표와 마지막 글자의 우상 좌표를 가져와 범위값 생성.
				//var minBound = startCharacterInfo.bottomLeft;
				//var maxBound = endCharacterInfo.topRight;

				// 첫 글자의 좌측 좌표와 마지막 글자의 우측좌표 + 증가폭 그리고 현재 라인의 상하 좌표를 가져와 범위값 생성.
				var minBound = new Vector2(startCharacterInfo.origin, lineInfo.descender);
				var maxBound = new Vector2(endCharacterInfo.origin + endCharacterInfo.xAdvance, lineInfo.ascender);

				// 사각형을 그리기 위한 정점은 LB < LT < RT < RB 로 CW 순서. (0,1,2 < 0,1,3)
				var worldPosition = labelView.RectTransform.TransformPoint(minBound);
				vertices[0] = graphicView.RectTransform.InverseTransformPoint(worldPosition);
				worldPosition = labelView.RectTransform.TransformPoint(new Vector2(minBound.x, maxBound.y));
				vertices[1] = graphicView.RectTransform.InverseTransformPoint(worldPosition);
				worldPosition = labelView.RectTransform.TransformPoint(maxBound);
				vertices[2] = graphicView.RectTransform.InverseTransformPoint(worldPosition);
				worldPosition = labelView.RectTransform.TransformPoint(new Vector2(maxBound.x, minBound.y));
				vertices[3] = graphicView.RectTransform.InverseTransformPoint(worldPosition);

				// 사각형 추가.
				UIGraphicView.AddQuad(vertexHelper, vertices, UIGraphicView.UV, Color.white);
			}
		}
	}
}