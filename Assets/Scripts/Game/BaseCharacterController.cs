using UnityEngine;
using System;
using System.Collections;

public class BaseCharacterController : MonoBehaviour
{
	// ステータス
	public enum Status
	{
		Init,       // 初期状態
		Start,      // 開始
		Normal,     // 通常
		Damage,     // ダメージ
		Dead,       // 死んだ瞬間
		Dying,      // 死んでいる状態
	}

	/// <summary> アニメーション </summary>
	[SerializeField]
	protected Animation _animation = null;
	/// <summary> アニメーション </summary>
	[SerializeField]
	protected Rigidbody _rigitBody = null;
	/// <summary> スキンメッシュレンダラー </summary>
	[SerializeField]
	protected SkinnedMeshRenderer _skinMesh = null;
	/// <summary> ライフ </summary>
	protected int _life = 0;
	/// <summary> ステータス </summary>
	protected Status _status = Status.Init;
	/// <summary> 位置フレーム前のステータス </summary>
	protected Status _preStatus = Status.Init;

	public virtual void SetUp() { }
	protected virtual void Move() { }
	protected virtual void Jump() { }
	protected virtual void Damage(int damage) { }

	/// <summary>
	/// 点滅開始
	/// </summary>
	/// <param name="flashTime"> 点滅時間 </param>
	/// <returns></returns>
	protected IEnumerator CoStartFlash(float flashTime = 2.0f, Action endAction = null)
	{
		float nextTime = Time.time;
		float endTime = Time.time + flashTime;
		float interval = 0.1f;

		// 点滅時間になるまで処理を行う
		while (endTime > nextTime)
		{
			if (Time.time > nextTime)
			{
				_skinMesh.enabled = !_skinMesh.enabled;
				nextTime += interval;
			}

			// 1フレーム待つ
			yield return null;
		}

		// 終了後　に行いたい処理があるなら行う
		if (endAction != null)
		{
			endAction();
		}

		_skinMesh.enabled = true;
	}

	/// <summary>
	/// アニメーション待機処理(アニメ指定あり)
	/// </summary>
	/// <param name="animName"> 待機するアニメーション名 </param>
	/// <param name="endAction"> 終了時の処理 </param>
	/// <returns></returns>
	protected IEnumerator CoWaitAnimation(string animName, Action endAction = null)
	{
		// 指定のアニメが再生中なら待つ
		while (_animation.IsPlaying(animName))
		{
			yield return null;
		}

		if (endAction != null)
		{
			endAction();
		}
	}

	/// <summary>
	/// アニメーション待機処理(アニメ指定なし)
	/// </summary>
	/// <param name="endAction"> 終了時の処理 </param>
	/// <returns></returns>
	protected IEnumerator CoWaitAnimation(Action endAction = null)
	{
		// 何かしらアニメが再生中なら待つ
		while (_animation.isPlaying)
		{
			yield return null;
		}

		if (endAction != null)
		{
			endAction();
		}
	}

	/// <summary>
	/// 秒指定で待機処理
	/// </summary>
	/// <param name="waitTime"> 待機時間 </param>
	/// <param name="endAction"> 終了時の処理 </param>
	/// <returns></returns>
	protected IEnumerator CoWait(float waitTime, Action endAction)
	{
		var wait = Time.time + waitTime;

		while (wait > Time.time)
		{
			yield return null;
		}

		if (endAction != null)
		{
			endAction();
		}
	}
}
