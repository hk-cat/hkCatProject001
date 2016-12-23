using UnityEngine;
using System;
using System.Collections;

public class MasterDataManager : SingletonMonoBehaviour<MasterDataManager>
{
	/// <summary>
	/// データのロード
	/// </summary>
	public IEnumerator CoLoad()
	{
		yield return null;
	}
}
