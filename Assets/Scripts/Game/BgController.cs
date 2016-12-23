using UnityEngine;
using System.Collections;

public class BgController : MonoBehaviour
{
	[SerializeField]
	private UITexture _bgTex;

	private float _uvRectX;

	void Start()
	{
		_uvRectX = 0;
	}

	void Update()
	{
		_uvRectX -= 0.01f;

		if (_uvRectX <= -1)
		{
			_uvRectX = 0;
		}

		_bgTex.uvRect.Set(_uvRectX, 0,1,1);
	}
}
