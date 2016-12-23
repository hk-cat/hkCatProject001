using UnityEngine;
using System.Collections;

/// <summary>
/// タブのコンポーネント
/// </summary>
public class UITab : MonoBehaviour
{
	/// <summary>アニメーション制御</summary>
	[SerializeField]
	private UIPlayTween _playTween = null;

	/// <summary>
	/// 選択状態になったとき
	/// </summary>
	public void OnSelect()
	{
		_playTween.includeChildren = true;
		_playTween.resetOnPlay = true;
		_playTween.tweenGroup = 0;
		_playTween.Play(true);
	}

	/// <summary>
	/// 非選択状態になったとき
	/// </summary>
	public void OnUnselect()
	{
		_playTween.includeChildren = true;
		_playTween.resetOnPlay = true;
		_playTween.tweenGroup = 1;
		_playTween.Play(true);
	}
}
