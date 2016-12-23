using UnityEngine;
using System.Collections;
using System;

/// <summary> 
/// NGUIのボタン押しっぱなし判定用 
/// </summary> 
[RequireComponent(typeof(UIButton))]
public class UIHoldButton : MonoBehaviour
{
	// 押しっぱなし判定の間隔（この間隔毎にActionHoldPressが呼ばれる） 
	[SerializeField]
	private float _interval = 0.0f;
	// 次の押下判定時間 
	private float _nextTime = 0f;
	// 押しっぱなし時に呼び出すAction 
	private Action _actionHoldPress;
	// 押下中フラグ 
	public bool pressed
	{
		get;
		private set;
	}

	/// <summary>
	/// インターバル時間変更
	/// </summary>
	/// <param name="interval"></param>
	public void SetInterval(float interval = 0.0f)
	{
		_interval = interval;
	}

	void Update()
	{
		if (pressed && _nextTime < Time.realtimeSinceStartup)
		{
			_nextTime = Time.realtimeSinceStartup + _interval;
			if (_actionHoldPress != null)
			{
				_actionHoldPress();
			}
		}
	}

	void OnPress(bool pressed)
	{
		this.pressed = pressed;
		_nextTime = Time.realtimeSinceStartup + _interval;
	}

	/// <summary>
	/// ホールド時の処理を設定
	/// </summary>
	/// <param name="action"></param>
	public void SetActionHoldPress(Action action)
	{
		_actionHoldPress = action;
	}
}
