using UnityEditor;
using UnityEngine;

public static class MasterDataConverter
{
	// 出力コマンド名
	private const string CONVERT_CS_COMMAND_NAME = "Assets/MasterData/Create cs File";
	// 出力コマンド名
	private const string CONVERT_CSV_COMMAND_NAME = "Assets/MasterData/Convert To Csv File";

	// 出力ファイルパス
	private const string EXPORT_FILEPATH = "Assets/Scripts/Game/MasterData/";

	// 選択中のオブジェクトが存在するかどうか
	private static bool isSelectAssets
	{
		get { return Selection.assetGUIDs != null && Selection.assetGUIDs.Length > 0; }
	}

	/// <summary>
	/// シーン名を定数で管理するクラスを作成します
	/// </summary>
	[MenuItem(CONVERT_CS_COMMAND_NAME, false, 1)]
	public static void ExportMasterData()
	{
		// ファイルが選択されている時.
		if (!isSelectAssets)
		{
			return;
		}

		// 選択されているパスの取得.
		foreach (var files in Selection.assetGUIDs)
		{
			var filePath = AssetDatabase.GUIDToAssetPath(files);
			MakeMasterData.MakeMaster(filePath, EXPORT_FILEPATH);

			EditorUtility.DisplayDialog(filePath, "作成が完了しました", "OK");
		}
	}

	/// <summary>
	/// シーン名を定数で管理するクラスを作成します
	/// </summary>
	[MenuItem(CONVERT_CSV_COMMAND_NAME, false, 1)]
	public static void ExportCsvFile()
	{
		Debug.Log("CSVファイルへ変換します");
	}
}
