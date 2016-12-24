using UnityEngine;
using System.Collections.Generic;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace Excel
{
	/// <summary>
	/// ブックデータ
	/// </summary>
	public class BookData
	{
		/// <summary>ブック名</summary>
		public string bookName;
		/// <summary>シートのリスト</summary>
		public List<SheetData> sheets;
	}

	/// <summary>
	/// シートデータ
	/// </summary>
	public class SheetData
	{
		/// <summary>シート名</summary>
		public string sheetName;
		/// <summary>項目列の総数</summary>
		public int columns;
		/// <summary>行の総数</summary>
		public int rows;
		/// <summary>データ</summary>
		public List<List<string>> data;
	}

	/// <summary>
	/// エクセルリーダー
	/// </summary>
	public class ExcelReader
	{
		/// <summary>
		/// Excelファイルを読み込む
		/// </summary>
		/// <param name="fileName">ファイル名</param>
		/// <returns>ブックデータ</returns>
		public static BookData Load(string fileName)
		{
			var stream = File.OpenRead(fileName);
			var srcBook = new XSSFWorkbook(stream);
			stream.Close();

			var book = new BookData();
			book.bookName = Path.GetFileNameWithoutExtension(fileName);
			book.sheets = new List<SheetData>();

			foreach (ISheet srcSheet in srcBook)
			{
				if (srcSheet.Protect)
				{
					continue;
				}

				var sheet = CreateSheet(srcSheet);

				if (sheet == null)
				{
					continue;
				}

				book.sheets.Add(sheet);
			}

			return book;
		}

		/// <summary>
		/// シートデータを生成
		/// </summary>
		/// <param name="sheet">Excelシート情報</param>
		/// <returns>シートデータ</returns>
		private static SheetData CreateSheet(ISheet sheet)
		{
			var retSheet = new SheetData();

			var rows = sheet.LastRowNum + 1;
			var firstRow = sheet.GetRow(sheet.FirstRowNum);
			var columns = firstRow.LastCellNum;

			retSheet.sheetName = sheet.SheetName;
			retSheet.columns = columns;
			retSheet.rows = rows;
			retSheet.data = new List<List<string>>();

			for (int r = 0; r < rows; r++)
			{
				var rowData = sheet.GetRow(r);

				var rowValue = new List<string>();

				for (int c = 0; c < columns; c++)
				{
					if (rowData == null)
					{
						Debug.Log(sheet.SheetName);
						continue;
					}

					var cellData = rowData.GetCell(c);
					var cellValue = "";

					if (cellData != null)
					{
						if (cellData.CellType == CellType.Numeric)
						{
							cellValue = cellData.NumericCellValue.ToString();
						}
						else
						{
							cellData.SetCellType(CellType.String);
							cellValue = cellData.StringCellValue;
						}
					}
					else
					{
						Debug.Log(sheet.SheetName + ":(" + (char)(c + 'A') + (r + 1).ToString() + ")");
					}

					rowValue.Add(cellValue);
				}

				retSheet.data.Add(rowValue);
			}

			return retSheet;
		}
	}
}
