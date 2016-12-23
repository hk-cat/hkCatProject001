using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CommonMenuScript : MonoBehaviour
{
	/// <summary> ボタンリスト </summary>
	[SerializeField]
	private List<UIButton> _buttonList;

	/// <summary> ループするかどうか </summary>
	[SerializeField]
	private bool _isLoop;

	/// <summary> インデックス </summary>
	private int _index;
	/// <summary> 前のインデックス </summary>
	private int _preIndex;
	/// <summary> 再生するコールバック  </summary>
	private Action<int> _callback;

	/// <summary>
	/// セットアップ
	/// </summary>
	public void SetUp(Action<int> callback)
	{
		_preIndex = 0;
		_index = 0;
		SwapTex(_buttonList[_index]);
		_callback = callback;

		UiPadManager.Instance.SetUse(true);
		UiPadManager.Instance.PadCommandLeftSet(
			UpCommand,
			DownCommand,
			null,
			null,
			true,
			0.2f);

		UiPadManager.Instance.PadCommandRightSet(
			null,
			null,
			null,
			DecideCommand);
	}

	/// <summary>
	/// テクスチャ入れ替え
	/// </summary>
	/// <param name="button"> 入れ替えを行うボタン </param>
	private void SwapTex(UIButton button)
	{
		var tmp = button.pressedSprite;
		button.pressedSprite = button.normalSprite;
		button.normalSprite = tmp;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			DownCommand();
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			UpCommand();
		}
		else if (Input.GetKeyDown(KeyCode.Return))
		{
			DecideCommand();
		}
	}

	/// <summary>
	/// 現在選択中のインデックス取得
	/// </summary>
	/// <returns> インデックス </returns>
	public int GetIndex()
	{
		return _index;
	}

	/// <summary>
	/// 上コマンド
	/// </summary>
	private void UpCommand()
	{
		_preIndex = _index;

		--_index;

		if (_index < 0)
		{
			if (_isLoop)
			{
				_index = _buttonList.Count - 1;
			}
			else
			{
				_index = 0;
			}
		}

		SwapTex(_buttonList[_preIndex]);
		SwapTex(_buttonList[_index]);
	}

	/// <summary>
	/// 下コマンド
	/// </summary>
	private void DownCommand()
	{
		_preIndex = _index;

		++_index;

		if (_index > _buttonList.Count - 1)
		{
			if (_isLoop)
			{
				_index = 0;
			}
			else
			{
				_index = _buttonList.Count - 1;
			}
		}

		SwapTex(_buttonList[_preIndex]);
		SwapTex(_buttonList[_index]);
	}

	/// <summary>
	/// 決定コマンド
	/// </summary>
	private void DecideCommand()
	{
		_callback(_index);
	}
}
