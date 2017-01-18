using UnityEngine;
using System.Collections.Generic;

public class MapCreateSystem : MonoBehaviour
{
	/// <summary>
	/// １マス当たりの距離
	/// </summary>
	private const float MAP_DISTANCE = 1;

	/// <summary> マップを生成する親オブジェクト </summary>
	[SerializeField]
	private GameObject _parent = null;

	[SerializeField]
	private List<string> _mapType = new List<string>();

	[SerializeField]
	private List<GameObject> _mapObject = new List<GameObject>();

	/// <summary>
	/// マップのパラメータに
	/// </summary>
	private Dictionary<string, GameObject> _mapObjDic = new Dictionary<string, GameObject>();

	/// <summary>
	/// セットアップ
	/// </summary>
	public void SetUp()
	{
		var objCount = _mapObject.Count;
		var typeCount = _mapType.Count;

		var num = (objCount < typeCount) ? objCount : typeCount;

		for (int i = 0; i < num; ++i)
		{
			_mapObjDic.Add(_mapType[i], _mapObject[i]);
		}

		var map = MapLoadSystem.LoadCsvFile("Map001");

		CreateMap(map);
	}

	/// <summary>
	/// マップ生成
	/// </summary>
	/// <param name="mapInfo">マップ情報</param>
	private void CreateMap(MapData mapInfo)
	{
		Vector3 pos = Vector3.zero;

		int index = 0;

		var xCollection = mapInfo.columnNum / 2;
		var zCollection = mapInfo.row / 2;

		foreach (var map in mapInfo.map)
		{
			var i = index % mapInfo.columnNum;
			var j = index / mapInfo.columnNum;

			pos.x = (i - xCollection) * MAP_DISTANCE;
			pos.z = (zCollection - j) * MAP_DISTANCE;

			if (_mapObjDic.ContainsKey(map))
			{
				GameObject obj = (GameObject)Instantiate(_mapObjDic[map], pos, Quaternion.identity);

				if (_parent != null)
				{
					obj.transform.parent = _parent.transform;
				}
			}

			++index;
		}
	}
}
