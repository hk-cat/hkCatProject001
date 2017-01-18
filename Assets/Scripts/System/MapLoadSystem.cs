using UnityEngine;
using System.IO;
using System.Collections.Generic;

public static class MapLoadSystem
{
	/// <summary> 読み込んだ譜面 </summary>
	private static TextAsset _scoreCsv;

	/// <summary>
	/// CSVファイル読み込み
	/// </summary>
	/// <param name="i_FileName">ファイル名</param>
	public static MapData LoadCsvFile(string i_FileName)
	{
		var ret = new MapData();

		// CSVファイルの読み込み
		_scoreCsv = Resources.Load(i_FileName) as TextAsset;
		StringReader reader = new StringReader(_scoreCsv.text);
		
		// 読み終えるまでループ
		while (reader.Peek() > -1)
		{
			// 1行抜き出す
			string line = reader.ReadLine();
			// ,(カンマ)区切りのパラメータなので、カンマまでの文字列を抽出
			string[] values = line.Split(',');

			ret.columnNum = int.Parse(values[0]);
			ret.row = int.Parse(values[1]);
			
			for(int i= 2; i < values.Length; ++i)
			{
				ret.map.Add(values[i]);
			}
		}
		
		return ret;
	}
}
