using UnityEngine;
using System.Collections;

public class FieldMapScene : MonoBehaviour
{

	/// <summary> プレイヤーの参照 </summary>
	[SerializeField]
	private PlayerController _player = null;

	// Use this for initialization
	void Awake()
	{
		_player.SetUp();
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
