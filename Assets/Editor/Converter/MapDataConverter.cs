using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

public static class MapDataConverter
{
	// 出力コマンド名
	private const string CONVERT_CSV_COMMAND_NAME = "Assets/Data/Convert To Csv File";
	// 出力ファイルパス
	private const string EXPORT_FILEPATH = "Assets/Data/MapData/Resources/";
	/// <summary>読み込みシート名</summary>
	private const string SHEET_NAME = "Map";

	/// <summary>メタデータを格納している行数</summary>
	private const int META_DATA_ROW = 0;
	/// <summary> 列数データを格納している行の列数 </summary>
	private const int COLUMN_INDEX = 0;
	/// <summary> 行数データを格納している行の列数 </summary>
	private const int ROW_INDEX = 1;

	// 選択中のオブジェクトが存在するかどうか
	private static bool isSelectAssets
	{
		get { return Selection.assetGUIDs != null && Selection.assetGUIDs.Length > 0; }
	}

	/// <summary>ストリングテーブルのファイル名</summary>
	private static string _excelFileName = string.Empty;

	/// <summary>Csvのファイル名</summary>
	private static string _csvFileName = string.Empty;

	/// <summary>
	/// シーン名を定数で管理するクラスを作成します
	/// </summary>
	[MenuItem(CONVERT_CSV_COMMAND_NAME, false, 1)]
	public static void ExportCsvFile()
	{
		// ファイルが選択されている時.
		if (!isSelectAssets)
		{
			return;
		}

		try
		{
			// 選択されているパスの取得.
			foreach (var files in Selection.assetGUIDs)
			{
				_excelFileName = AssetDatabase.GUIDToAssetPath(files);
				// 既にあるcsvをタイムスタンプで比較するため名前だけ作っておく
				_csvFileName = EXPORT_FILEPATH + Path.GetFileNameWithoutExtension(_excelFileName) + ".csv";

				ConvXlsx2Csv(_excelFileName, _csvFileName);
			}

			EditorUtility.DisplayDialog("CSV Convert", "作成が正常に完了しました", "OK");
		}
		catch
		{
			EditorUtility.DisplayDialog("CSV Convert", "作成中にエラーが発生しました。", "OK");
		}
	}

	/// <summary>
	/// コンバート
	/// </summary>
	/// <param name="fileName">ファイル名</param>
	private static void ConvXlsx2Csv(string inputFileName, string outputFileName)
	{
		List<string> data = new List<string>();

		// excele読み込み
		using (var fs = new FileStream(inputFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			var workbook = new XSSFWorkbook(fs);
			var sheet = workbook.GetSheet(SHEET_NAME);

			if (sheet == null) throw new Exception(_excelFileName + "のシート" + SHEET_NAME + "が開けませんでした");

			// 行数と列数を取得
			var colunm = sheet.GetRow(META_DATA_ROW).GetCell(COLUMN_INDEX);
			colunm.SetCellType(CellType.String);
			var val = colunm.ToString();
			data.Add(val);
			int columnNum = int.Parse(val);

			var row = sheet.GetRow(META_DATA_ROW).GetCell(ROW_INDEX);
			row.SetCellType(CellType.String);
			val = row.ToString();
			data.Add(val);
			var rowNum = int.Parse(val);

			var rowIndex = META_DATA_ROW + 1;

			while (rowIndex < rowNum + 1)
			{
				for (var i = 0; i < columnNum; ++i)
				{
					var cell = sheet.GetRow(rowIndex).GetCell(i);

					if (cell != null)
					{
						cell.SetCellType(CellType.String);

						val = cell.ToString();

						data.Add(val);
					}
					else
					{
						data.Add("");
					}
				}

				++rowIndex;
			}

			using (var writer = new CsvWriter(outputFileName))
			{
				writer.Write(data, false);
			}
		}
	}
}
