using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public static class MakeMasterData
{
	private static readonly Encoding ENCODING = Encoding.UTF8;
	private const string QUOTE = @"""";
	private const string TAB = "\t";

	public static bool MakeMaster(string fileName, string dir)
	{
		var book = CreateBook(fileName);

		if (!ExportCSharp(book.sheets[0], book.bookName, dir))
		{
			return false;
		}

		return true;
	}

	/// <summary>
	/// ブックデータを生成
	/// </summary>
	/// <param name="fileName">ファイル名</param>
	/// <returns>ブックデータ</returns>
	public static BookData CreateBook(string fileName)
	{
		var srcBook = Excel.ExcelReader.Load(fileName);
		return new BookData(srcBook);
	}

	/// <summary>
	/// .cs ファイルを出力
	/// </summary>
	/// <param name="sheet">シートデータ</param>
	/// <param name="dir">ディレクトリ</param>
	/// <returns>成功したか</returns>
	private static bool ExportCSharp(SheetData sheet, string fileName, string dir)
	{
		var filePath = dir + fileName + ".cs";

		using (var writer = new StreamWriter(filePath, false, ENCODING))
		{
			// 名前空間定義
			writer.WriteLine("using System;");
			writer.WriteLine("using System.Collections.Generic;");
			writer.WriteLine("");

			writer.WriteLine("namespace GameData");
			writer.WriteLine("{");

			{
				writer.WriteLine(TAB + "[Serializable]");
				writer.WriteLine(TAB + "public class " + fileName + "List");
				writer.WriteLine(TAB + "{");

				writer.WriteLine(TAB + TAB + "// " + "List of " + fileName);
				writer.WriteLine(TAB + TAB + "public List<" + fileName + "> " + "data_list;");

				writer.WriteLine(TAB + "}");
				writer.WriteLine("");

				writer.WriteLine(TAB + "[Serializable]");
				writer.WriteLine(TAB + "public class " + fileName);
				writer.WriteLine(TAB + "{");

				// メンバ変数
				for (int cl = 0; cl < sheet.columns; cl++)
				{
					var comment = sheet.comments[cl];
					var key = sheet.keys[cl];
					var type = sheet.types[cl];
					var keyType = "";
					switch (type)
					{
					case KeyType.Int:
						keyType = "int";
						break;

					case KeyType.String:
						keyType = "string";
						break;

					case KeyType.Float:
						keyType = "float";
						break;
					}

					writer.WriteLine(TAB + TAB + "// " + comment);
					writer.WriteLine(TAB + TAB + "public " + keyType + " " + key + ";");
				}

				writer.WriteLine(TAB + "}");
			}

			// End of namespace GameData
			writer.WriteLine("}");

			writer.Close();
		}

		return true;
	}

	/// <summary>
	/// キーの種類
	/// </summary>
	public enum KeyType
	{
		None,
		Int,
		String,
		Float,
	}

	/// <summary>
	/// ブックデータ
	/// </summary>
	public class BookData
	{
		/// <summary>ブックの名前</summary>
		public string bookName;
		/// <summary>シートのリスト</summary>
		public List<SheetData> sheets;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="srcBook">元のブックデータ</param>
		public BookData(Excel.BookData srcBook)
		{
			bookName = srcBook.bookName;
			sheets = new List<SheetData>();

			for (int i = 0; i < srcBook.sheets.Count; i++)
			{
				sheets.Add(new SheetData(srcBook.sheets[i]));
			}
		}
	}

	/// <summary>
	/// シートデータ
	/// </summary>
	public class SheetData
	{
		/// <summary>開始カラム</summary>
		private const int START_COL = 0;
		/// <summary>開始行</summary>
		private const int START_ROW = 3;
		/// <summary>コメント行</summary>
		private const int ROW_COMMENT = 0;
		/// <summary>キー行</summary>
		private const int ROW_KEY = 1;
		/// <summary>タイプ行</summary>
		private const int ROW_TYPE = 2;

		/// <summary>キーとタイプの対応</summary>
		private static readonly Dictionary<string, KeyType> KEY_TYPES = new Dictionary<string, KeyType>() {
				{ "int", KeyType.Int },
				{ "string", KeyType.String },
				{ "float", KeyType.Float },
			};

		/// <summary>シート名</summary>
		public string sheetName;
		/// <summary>項目列の総数</summary>
		public int columns;
		/// <summary>行の総数</summary>
		public int rows;
		/// <summary>コメントリスト</summary>
		public List<string> comments;
		/// <summary>変数名リスト</summary>
		public List<string> keys;
		/// <summary>変数型リスト</summary>
		public List<KeyType> types;
		/// <summary>データ</summary>
		public List<List<string>> values;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="srcSheet">元のシートデータ</param>
		public SheetData(Excel.SheetData srcSheet)
		{
			sheetName = srcSheet.sheetName;
			columns = srcSheet.columns;
			rows = srcSheet.rows - START_ROW;
			comments = srcSheet.data[ROW_COMMENT];
			keys = srcSheet.data[ROW_KEY];

			types = new List<KeyType>();

			foreach (var str in srcSheet.data[ROW_TYPE])
			{
				types.Add(KEY_TYPES[str]);
			}

			values = new List<List<string>>();

			for (int j = START_ROW; j < srcSheet.rows; j++)
			{
				values.Add(srcSheet.data[j]);
			}
		}

		/// <summary>
		/// 指定のキーから値を検索し見つかったキーの値を返す
		/// </summary>
		/// <param name="fromValue">検索元の値</param>
		/// <param name="fromKey">検索元のキー</param>
		/// <param name="toKey">検索先のキー</param>
		/// <returns>検索先の値</returns>
		public string SearchValueByOtherKey(string fromValue, string fromKey, string toKey)
		{
			int fromKeyIndex = keys.IndexOf(fromKey);

			if (fromKeyIndex != -1)
			{
				var targetRow = values.Find(val => val[fromKeyIndex] == fromValue);

				if (targetRow != null)
				{
					int toKeyIndex = keys.IndexOf(toKey);

					if (toKeyIndex != -1)
					{
						return targetRow[toKeyIndex];
					}
				}
			}

			return null;
		}
	}
}
