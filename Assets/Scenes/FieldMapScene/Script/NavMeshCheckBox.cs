using UnityEngine;
using System.Collections;

public class NavMeshCheckBox : MonoBehaviour
{
	/// <summary> チェックボックスのチェックマーク </summary>
	[SerializeField]
	private UISprite _checkMarkSprite = null;

	/// <summary> NavMeshAgent </summary>
	[SerializeField]
	private NavMeshAgent _agent = null;

	/// <summary> NavController </summary>
	[SerializeField]
	private NavController _navController = null;

	/// <summary> プレイヤー </summary>
	[SerializeField]
	private GameObject _player = null;

	/// <summary> チェックボックスの状態 </summary>
	private bool _isCheck = false;

	// Use this for initialization
	void Start()
	{
		_checkMarkSprite.enabled = _isCheck;
		_agent.enabled = _isCheck;
	}

	/// <summary>
	/// チェックボックスが押された
	/// </summary>
	public void OnClickCheck()
	{
		_isCheck = !_isCheck;

		_checkMarkSprite.enabled = _isCheck;
		_agent.enabled = _isCheck;

		if (_navController != null && _isCheck)
		{
			_navController.SetUp();
		}
	}

	/// <summary>
	/// チェックボックスが押された
	/// </summary>
	public void OnClickCheck2()
	{
		_isCheck = !_isCheck;

		_checkMarkSprite.enabled = _isCheck;

		if (_navController != null)
		{
			if (_isCheck)
			{
				_navController.SetTarget(_player);
			}
			else
			{
				_navController.SetTarget(GameObject.Find("Goal(Clone)"));
			}
		}
	}
}
