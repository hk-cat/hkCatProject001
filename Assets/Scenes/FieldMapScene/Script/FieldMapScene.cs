using UnityEngine;

public class FieldMapScene : MonoBehaviour
{
	/// <summary> マップ生成 </summary>
	[SerializeField]
	private MapCreateSystem _mapCreate = null;

	/// <summary> プレイヤーの参照 </summary>
	[SerializeField]
	private PlayerController _player = null;

	/// <summary> NavControllerの参照 </summary>
	[SerializeField]
	private NavController _navController = null;

	// Use this for initialization
	void Awake()
	{
		_mapCreate.SetUp();
	}

	// Use this for initialization
	void Start ()
	{
		_player.SetUp();
		_navController.SetUp();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
