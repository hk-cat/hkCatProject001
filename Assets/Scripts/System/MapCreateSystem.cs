using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Map;

public class MapCreateSystem : MonoBehaviour
{
	///// <summary>
	///// マップ定義
	///// </summary>
	//private static readonly string[] Map =
	//{
	//	"S"," "," "," "," ",
	//	" ","壁"," "," "," ",
	//	" "," ","壁"," "," ",
	//	" "," "," ","壁"," ",
	//	" "," "," "," ","G",
	//};

	///// <summary>
	///// マップのパラメータに
	///// </summary>
	//[SerializeField]
	//private Dictionary<string, GameObject> _mapObjDic = new Dictionary<string, GameObject>();

	/// <summary>
	/// マップ生成
	/// </summary>
	/// <param name="mapInfo">マップ情報</param>
	private void CreateMap(string[] mapInfo)
	{
		//foreach(var map in mapInfo)
		//{
		//	switch(map)
		//	{
		//	// スタート
		//	case MapType.start:

		//		break;

		//	// ゴール
		//	case MapType.goal:

		//		break;

		//	// 壁
		//	case MapType.wall:

		//		break;
			
		//	// 何もない
		//	case MapType.none:

		//		break;

		//	// 未定義
		//	default:
		//		Debug.LogWarning(map + " is not defined");
		//		break;
		//	}

		//}


	}
}
