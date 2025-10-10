using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Crockhead.Table;
using Crockhead.Unity.Editor;
//using Crockhead.Unity;
//using Crockhead.Unity.Editor;
//using Crockhead.Unity.Table.Editor;


/// <summary>
/// 테이블 도구.
/// </summary>
public static class TableEditor
{
	/// <summary>
	/// 파일 경로가 유효한지 확인.
	/// </summary>
	private static bool CheckValidateFilePath(string filePath)
	{
		// 파일 경로 확인.
		if (string.IsNullOrWhiteSpace(filePath))
			return false;

		// 파일 존재 여부 확인.
		if (!File.Exists(filePath))
			return false;

		// 파일 경로 분해.
		var path = Path.GetDirectoryName(filePath);
		var name = Path.GetFileNameWithoutExtension(filePath);
		var extension = Path.GetExtension(filePath);

		// 파일 이름 접두어로 임시파일 접두어 존재 여부 확인.
		if (name.StartsWith("~$"))
			return false;

		return true;
	}

	/// <summary>
	/// 파일 경로 수정.
	/// </summary>
	private static string ReplaceFilePath(string filePath)
	{
		filePath = filePath.Replace("\\", "/");
		//filePath = Replace(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar);
		return filePath;
	}

	/// <summary>
	/// 엑셀 파일 목록 처리.
	/// </summary>
	private static void GenerateAllExcelFiles(string title, bool writeCS, bool writeJSON)
	{
		EditorUtility.ClearProgressBar();

		using var writter = new DataTableWritter(null);
		var excelFilePaths = Directory.GetFiles(Projects.ExcelDirectory, "*.xlsx", SearchOption.TopDirectoryOnly);
		excelFilePaths = excelFilePaths.Where(CheckValidateFilePath).Select(ReplaceFilePath).ToArray();

		var fileCount = excelFilePaths.Length;
		for (var fileIndex = 0; fileIndex < fileCount; ++fileIndex)
		{
			var fileDisplayIndex = fileIndex + 1;
			var excelFilePath = excelFilePaths[fileIndex];

			var content = $"{excelFilePath} ({fileDisplayIndex} / {fileCount})";
			var progress = Mathf.Clamp01((float)fileIndex / (float)fileCount);
			Debug.Log($"[TableEditor] GenerateAllExcelFiles(): {content}");
			var cancel = EditorUtility.DisplayCancelableProgressBar(title, content, progress);
			if (cancel)
			{
				Debug.Log($"[TableEditor] Cancel Generate.");
				EditorUtility.ClearProgressBar();
				return;
			}

			var rawTables = writter.CreateRawTablesFromXLSXFile(excelFilePath);
			foreach (var rawTable in rawTables)
			{
				Debug.Log($"[TableEditor] EXCEL File: \"{excelFilePath}\"");
				var dataTable = writter.CreateDataTableFromRawTable(rawTable);
				//tableConverter.UpdateTableManifestFromDataTable(dataTable);
				var tableName = dataTable.Name;

				if (writeCS)
				{
					var csFilePath = Path.Combine(Projects.ScriptsDirectory, "Table", "Record", $"{tableName}Record.cs");
					csFilePath = ReplaceFilePath(csFilePath);
					Debug.Log($"[TableEditor] CS File: \"{csFilePath}\"");
					writter.CreateCSToFile(csFilePath, dataTable);
				}

				if (writeJSON)
				{
					var jsonFilePath = Path.Combine(Projects.ResourcesDirectory, "Table", $"{tableName}.json");
					jsonFilePath = ReplaceFilePath(jsonFilePath);
					Debug.Log($"[TableEditor] JSON File: \"{jsonFilePath}\"");
					writter.CreateJSONToFile(jsonFilePath, dataTable);
				}
			}
		}

		EditorUtility.ClearProgressBar();
		AssetDatabase.Refresh();
	}

	/// <summary>
	/// 전체 테이블 코드 파일 생성.
	/// </summary>
	[MenuItem("Project/Table/Generate CS All")]
	public static void GenerateCSFileAll()
	{
		var title = "CSharp Script File Generate";
		TableEditor.GenerateAllExcelFiles(title, true, false);
	}

	/// <summary>
	/// 전체 테이블 데이터 파일 생성.
	/// </summary>
	[MenuItem("Project/Table/Generate JSON All")]
	public static void GenerateJSONFileAll()
	{
		var title = "JSON Text File Generate";
		TableEditor.GenerateAllExcelFiles(title, false, true);
	}
}