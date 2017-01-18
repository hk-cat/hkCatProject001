using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : BaseCharacterController
{
	/// <summary> 最大ジャンプ回数 </summary>
	private const int MAX_JUMP_COUNT = 2;
	/// <summary> 移動量 </summary>
	private const float MOVE_VAL = 0.1f;
	/// <summary> ジャンプ力 </summary>
	private const float JUMP_VAL = 100.0f;
	/// <summary> ライフの最大値 </summary>
	private const int LIFE_MAX = 100;
	/// <summary> ダメージ量 </summary>
	private const int DAMAGE_VAL = 50;

	/// <summary> カメラ </summary>
	[SerializeField]
	private Camera _camera = null;

	/// <summary> ジャンプ数 </summary>
	private int _junpCount = 0;

	/// <summary>
	/// セットアップ
	/// </summary>
	public override void SetUp()
	{
		var start = GameObject.Find("Start(Clone)");
		if(start != null)
		{
			gameObject.transform.localPosition = start.gameObject.transform.localPosition;
		}

		_junpCount = 0;
		_life = LIFE_MAX;

		// 現在ステータスのみスタートにする
		_status = Status.Start;

		// 前フレームステータスは初期化のまま
		_preStatus = Status.Init;

		// パッドの設定
		UiPadManager.Instance.SetUse(true);
		UiPadManager.Instance.PadCommandLeftSet(
			MoveUp,
			MoveDown,
			MoveLeft,
			MoveRight,
			true);

		UiPadManager.Instance.PadCommandRightSet(
			null,
			Jump,
			null,
			Attack);
	}

	private void Update()
	{
		// ステータスチェック
		CheckStatus();

		// 死亡中は操作負荷
		if (_status != Status.Normal) return;

		Move();
		Action();
	}

	private void LateUpdate()
	{
		if (gameObject.transform.localPosition.y < -10)
		{
			Damage(DAMAGE_VAL);
			gameObject.transform.localPosition = Vector3.zero;
		}

		if (_status == Status.Normal)
		{
			if (!UiPadManager.Instance.IsPressedLeftCommand())
			{
				if(!_animation.IsPlaying("Attack")
					&& !_animation.IsPlaying("Dead")
					&& !_animation.IsPlaying("Damage"))
				// 待機状態に戻す
				_animation.Play("Wait");
			}
		}
	}

	// 当たり判定
	private void OnCollisionEnter(Collision collision)
	{
		// 床
		if (collision.gameObject.name == "Stg_Floor")
		{
			_junpCount = 0;
		}

		Debug.Log(collision.gameObject.name);

		if(collision.gameObject.name == "Goal(Clone)")
		{
			_status = Status.Clear;
		}
	}

	/// <summary>
	/// 移動
	/// </summary>
	protected override void Move()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			if (Input.GetKey(KeyCode.RightArrow))
			{
				var cameraRot = _camera.gameObject.transform.localEulerAngles;
				var euler = cameraRot.y + 45f;

				gameObject.transform.localEulerAngles = new Vector3(0, euler, 0);
				gameObject.transform.localPosition += Vector3.forward * MOVE_VAL * Mathf.Cos(euler);
				gameObject.transform.localPosition += Vector3.right * MOVE_VAL * Mathf.Sin(euler);
				_animation.Play("Walk");
			}
			else if (Input.GetKey(KeyCode.LeftArrow))
			{
				var cameraRot = _camera.gameObject.transform.localEulerAngles;
				var euler = cameraRot.y - 45f;

				gameObject.transform.localEulerAngles = new Vector3(0, euler, 0);
				gameObject.transform.localPosition += Vector3.forward * MOVE_VAL * Mathf.Cos(euler);
				gameObject.transform.localPosition += Vector3.right * MOVE_VAL * Mathf.Sin(euler);
				_animation.Play("Walk");
			}
			else
			{
				MoveUp();
			}
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			if (Input.GetKey(KeyCode.RightArrow))
			{
				var cameraRot = _camera.gameObject.transform.localEulerAngles;
				var euler = cameraRot.y + 135f;

				gameObject.transform.localEulerAngles = new Vector3(0, euler, 0);
				gameObject.transform.localPosition += Vector3.back * MOVE_VAL * Mathf.Cos(45);
				gameObject.transform.localPosition += Vector3.right * MOVE_VAL * Mathf.Sin(45);

				_animation.Play("Walk");
			}
			else if (Input.GetKey(KeyCode.LeftArrow))
			{
				var cameraRot = _camera.gameObject.transform.localEulerAngles;
				var euler = cameraRot.y - 135f;

				gameObject.transform.localEulerAngles = new Vector3(0, euler, 0);
				gameObject.transform.localPosition += Vector3.back * MOVE_VAL * Mathf.Cos(-45);
				gameObject.transform.localPosition += Vector3.right * MOVE_VAL * Mathf.Sin(-45);
				_animation.Play("Walk");
			}
			else
			{
				MoveDown();
			}
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			MoveRight();
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			MoveLeft();
		}
	}

	/// <summary>
	/// 上に移動
	/// </summary>
	private void MoveUp()
	{
		// 死亡中は操作負荷
		if (_status != Status.Normal) return;

		var cameraRot = _camera.gameObject.transform.localEulerAngles;
		var euler = cameraRot.y + 0f;

		gameObject.transform.localEulerAngles = new Vector3(0, euler, 0);
		gameObject.transform.localPosition += Vector3.forward * MOVE_VAL;
		_animation.Play("Walk");
	}

	/// <summary>
	/// 下に移動
	/// </summary>
	private void MoveDown()
	{
		// 死亡中は操作負荷
		if (_status != Status.Normal) return;

		var cameraRot = _camera.gameObject.transform.localEulerAngles;
		var euler = cameraRot.y + 180f;

		gameObject.transform.localEulerAngles = new Vector3(0, euler, 0);
		gameObject.transform.localPosition += Vector3.back * MOVE_VAL;
		_animation.Play("Walk");
	}

	/// <summary>
	/// 左に移動
	/// </summary>
	private void MoveLeft()
	{
		// 死亡中は操作負荷
		if (_status != Status.Normal) return;

		var cameraRot = _camera.gameObject.transform.localEulerAngles;
		var euler = cameraRot.y - 90;

		gameObject.transform.localPosition += Vector3.left * MOVE_VAL;
		gameObject.transform.localEulerAngles = new Vector3(0, euler, 0);
		_animation.Play("Walk");
	}

	/// <summary>
	/// 右に移動
	/// </summary>
	private void MoveRight()
	{
		// 死亡中は操作負荷
		if (_status != Status.Normal) return;

		var cameraRot = _camera.gameObject.transform.localEulerAngles;
		var euler = cameraRot.y + 90;

		gameObject.transform.localPosition += Vector3.right * MOVE_VAL;
		gameObject.transform.localEulerAngles = new Vector3(0, euler, 0);
		_animation.Play("Walk");
	}

	/// <summary>
	/// ジャンプ
	/// </summary>
	protected override void Jump()
	{
		// 死亡中は操作負荷
		if (_status != Status.Normal) return;

		if (_junpCount < MAX_JUMP_COUNT)
		{
			_rigitBody.AddForce(Vector3.up * 100);
			_animation.Play("Wait");

			++_junpCount;
		}
	}

	/// <summary>
	/// 攻撃
	/// </summary>
	private void Attack()
	{
		// 死亡中は操作負荷
		if (_status != Status.Normal) return;

		_animation.Play("Attack");
	}

	/// <summary>
	/// アクション
	/// </summary>
	private void Action()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
		else if (Input.GetKeyDown(KeyCode.Keypad2))
		{
			Attack();
		}
	}

	/// <summary>
	/// ダメージ処理
	/// </summary>
	/// <param name="damage"> ダメージ量 </param>
	protected override void Damage(int damage)
	{
		// 通常状態ならダメージを受ける
		if (_status == Status.Normal)
		{
			_life -= damage;

			if (_life > 0)
			{
				_status = Status.Damage;
			}
			else
			{
				_life = 0;
				_status = Status.Dead;
			}
		}
	}

	/// <summary>
	/// ステータス確認
	/// </summary>
	private void CheckStatus()
	{
		// 前フレームのステータスから変化がなければ処理しない
		if (_status == _preStatus) return;

		// 前フレームのステータスも更新
		_preStatus = _status;

		switch (_status)
		{
		case Status.Start:
			_animation.Play("Wait");
			StartCoroutine(CoStartFlash(2.0f, () =>
			{
				_status = Status.Normal;
			}));

			break;
		case Status.Normal:

			// とりあえず何もしない

			break;
		case Status.Damage:

			_animation.Play("Damage");

			StartCoroutine(CoStartFlash(1.0f, () =>
			{
				_status = Status.Normal;
			}));
			break;
		case Status.Dead:

			_animation.Play("Dead");
			_status = Status.Dying;

			break;
		case Status.Dying:
			StartCoroutine(CoWaitAnimation("Dead", () =>
			{
				StartCoroutine(CoWait(1.0f, () =>
				{
					SetUp();
				}));
			}));

			break;

		case Status.Clear:

			FadeManager.Instance.LoadScene(SceneName.HomeScene, LoadSceneMode.Single, FadeManager.FadeType.White);

			break;

		default:
			break;
		}
	}
}
