using UnityEngine;
using System.Collections;

public class ModelCameraController : MonoBehaviour
{
	private const float CAMERA_POS_Y = 100;

	/// <summary> プレイヤー </summary>
	[SerializeField]
	private PlayerController _player;

	private void LateUpdate()
	{
		var playerTransform = _player.gameObject.transform;

		// 注視点をプレイヤーに
		//Vector3 velocity = Vector3.zero;
		//gameObject.transform.LookAt(Vector3.SmoothDamp(transform.position, playerTransform.position, ref velocity, 0.5f));

		// 常にTPS視点になるよう移動
		//var pPos = playerTransform.localPosition;
		var myPos = gameObject.transform.localPosition;
		gameObject.transform.localPosition = new Vector3(playerTransform.localPosition.x, myPos.y, myPos.z);
	}
}
