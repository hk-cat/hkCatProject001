using UnityEngine;
using System.Collections;

public class TitleScene : MonoBehaviour
{
	/// <summary> Animator </summary>
	[SerializeField]
	private Animator _animator;

	void Awake()
	{
		_animator.gameObject.SetActive(false);
	}

	void Start()
	{
		_animator.gameObject.SetActive(true);
	}
}
