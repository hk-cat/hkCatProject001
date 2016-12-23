using UnityEngine;
using System;
using System.Collections;

public class UiPadManager : SingletonMonoBehaviour<UiPadManager>
{
	/// <summary> カメラ </summary>
	[SerializeField]
	private GameObject _camera = null;
	/// <summary> ホールド上 </summary>
	[SerializeField]
	private UIHoldButton _holdButtonUp;
	/// <summary> ホールド下 </summary>
	[SerializeField]
	private UIHoldButton _holdButtonDown;
	/// <summary> ホールド左 </summary>
	[SerializeField]
	private UIHoldButton _holdButtonLeft;
	/// <summary> ホールド右 </summary>
	[SerializeField]
	private UIHoldButton _holdButtonRight;

	/// <summary> Up押下 </summary>
	public Action onClickLUp = null;
	/// <summary> Down押下 </summary>
	public Action onClickLDown = null;
	/// <summary> Left押下 </summary>
	public Action onClickLLeft = null;
	/// <summary> Right押下 </summary>
	public Action onClickLRight = null;

	/// <summary> Up押下 </summary>
	public Action onClickRUp = null;
	/// <summary> Down押下 </summary>
	public Action onClickRDown = null;
	/// <summary> Left押下 </summary>
	public Action onClickRLeft = null;
	/// <summary> Right押下 </summary>
	public Action onClickRRight = null;

	/// <summary>
	/// 使用するかどうか
	/// </summary>
	/// <param name="isUse"> 使用するか </param>
	public void SetUse(bool isUse)
	{
		_camera.SetActive(isUse);
		transform.parent.gameObject.SetActive(isUse);
	}

	/// <summary>
	/// パッドの左コマンドをセット
	/// </summary>
	/// <param name="up">上</param>
	/// <param name="down">下</param>
	/// <param name="left">左</param>
	/// <param name="right">右</param>
	public void PadCommandLeftSet(Action up, Action down, Action left, Action right, bool isRepeat = false, float interval = 0.0f)
	{
		onClickLUp = up;
		onClickLDown = down;
		onClickLLeft = left;
		onClickLRight = right;

		if (isRepeat)
		{
			_holdButtonUp.SetActionHoldPress(up);
			_holdButtonDown.SetActionHoldPress(down);
			_holdButtonLeft.SetActionHoldPress(left);
			_holdButtonRight.SetActionHoldPress(right);
			_holdButtonUp.SetInterval(interval);
			_holdButtonDown.SetInterval(interval);
			_holdButtonLeft.SetInterval(interval);
			_holdButtonRight.SetInterval(interval);
		}
	}

	/// <summary>
	/// パッドの右コマンドセット
	/// </summary>
	/// <param name="up"></param>
	/// <param name="down"></param>
	/// <param name="left"></param>
	/// <param name="right"></param>
	public void PadCommandRightSet(Action up, Action down, Action left, Action right)
	{
		onClickRUp = up;
		onClickRDown = down;
		onClickRLeft = left;
		onClickRRight = right;
	}

	/// <summary>
	/// リセット
	/// </summary>
	public void Reset()
	{
		onClickLUp = null;
		onClickLDown = null;
		onClickLLeft = null;
		onClickLRight = null;
		_holdButtonUp.SetActionHoldPress(null);
		_holdButtonDown.SetActionHoldPress(null);
		_holdButtonLeft.SetActionHoldPress(null);
		_holdButtonRight.SetActionHoldPress(null);

		onClickRUp = null;
		onClickRDown = null;
		onClickRLeft = null;
		onClickRRight = null;
	}

	/// <summary>
	/// Upボタン押下処理
	/// </summary>
	public void OnClickLUp()
	{
		if (onClickLUp != null)
		{
			onClickLUp();
		}
	}

	/// <summary>
	/// Downボタン押下処理
	/// </summary>
	public void OnClickLDown()
	{
		if (onClickLDown != null)
		{
			onClickLDown();
		}
	}

	/// <summary>
	/// Leftボタン押下処理
	/// </summary>
	public void OnClickLLeft()
	{
		if (onClickLLeft != null)
		{
			onClickLLeft();
		}
	}

	/// <summary>
	/// Rightボタン押下処理
	/// </summary>
	public void OnClickLRight()
	{
		if (onClickLRight != null)
		{
			onClickLRight();
		}
	}

	/// <summary>
	/// Upボタン押下処理
	/// </summary>
	public void OnClicRLUp()
	{
		if (onClickRUp != null)
		{
			onClickRUp();
		}
	}

	/// <summary>
	/// Downボタン押下処理
	/// </summary>
	public void OnClickRDown()
	{
		if (onClickRDown != null)
		{
			onClickRDown();
		}
	}

	/// <summary>
	/// Leftボタン押下処理
	/// </summary>
	public void OnClickRLeft()
	{
		if (onClickRLeft != null)
		{
			onClickRLeft();
		}
	}

	/// <summary>
	/// Rightボタン押下処理
	/// </summary>
	public void OnClickRRight()
	{
		if (onClickRRight != null)
		{
			onClickRRight();
		}
	}
}
