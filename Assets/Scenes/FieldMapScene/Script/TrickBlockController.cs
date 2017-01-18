using UnityEngine;
using System.Collections;

public class TrickBlockController : MonoBehaviour
{
	/// <summary> MeshRenderer </summary>
	[SerializeField]
	private MeshRenderer _meshRenderer = null;

	/// <summary> カラー </summary>
	private Color _col = Color.black;

	/// <summary> アルファ値 </summary>
	private float _alpha = 0.0f;

	/// <summary>
	/// セットアップ
	/// </summary>
	public void SetUp()
	{
		_col.a = 0.0f;
		_meshRenderer.material.color = _col;
	}

	// Use this for initialization
	void Start()
	{
		SetUp();
	}

	// Update is called once per frame
	void Update()
	{
		if (_alpha < 1.0f)
		{
			_alpha += 0.001f;

			_col.a = _alpha;
			_meshRenderer.material.color = _col;
		}
	}
}
