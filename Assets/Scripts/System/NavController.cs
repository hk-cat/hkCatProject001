using UnityEngine;
using System.Collections;

public class NavController : MonoBehaviour
{
	/// <summary> NavMeshAgent </summary>
	[SerializeField]
	private UnityEngine.AI.NavMeshAgent _navMeshAgent = null;

	/// <summary> ターゲット </summary>
	private GameObject _target = null;

	/// <summary>
	/// セットアップ
	/// </summary>
	public void SetUp()
	{
		if (_navMeshAgent == null)
		{
			_navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		}

		if (_navMeshAgent != null && _navMeshAgent.enabled && _target != null)
		{
			_navMeshAgent.SetDestination(_target.transform.position);
		}
	}

	/// <summary>
	/// ターゲット変更
	/// </summary>
	public void SetTarget(GameObject target)
	{
		_target = target;
	}
}
