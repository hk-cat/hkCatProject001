using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
	/// <summary>
	/// フェードタイプ
	/// </summary>
	public enum FadeType
	{
		None,
		Black,
		White
	}

	/// <summary>
	/// シーンパラメータ
	/// </summary>
	public class SceneParam
	{
		/// <summary> シーン </summary>
		public SceneName scene;
		/// <summary> ロードモード </summary>
		public LoadSceneMode mode;
		/// <summary> フェードタイプ </summary>
		public FadeType fadeType;
		/// <summary> フェード時間 </summary>
		public float fadeTime;
	}

	private static FadeManager instance;
	public static FadeManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (FadeManager)FindObjectOfType(typeof(FadeManager));

				if (instance == null)
				{
					Debug.LogError(typeof(FadeManager) + "is nothing");
				}
			}

			return instance;
		}
	}

	/// <summary> フェード用のテクスチャ </summary>
	[SerializeField]
	private UITexture _fadeTex;

	/// <summary> フェード中か </summary>
	public bool isFading;
	/// <summary> シーンパラメータ </summary>
	public SceneParam sceneParam;

	/// <summary>
	/// フェード付きシーン遷移
	/// </summary>
	/// <param name="scene"></param>
	/// <param name="fadeType"></param>
	/// <param name="fadeTime"></param>
	public void LoadScene(SceneName scene, LoadSceneMode mode, FadeType fadeType, float fadeTime = 1f)
	{
		gameObject.SetActive(true);

		if (Instance.sceneParam == null)
		{
			Instance.sceneParam = new SceneParam();
		}

		// パッドをリセット
		UiPadManager.Instance.Reset();

		Instance.sceneParam.scene = scene;
		Instance.sceneParam.mode = mode;
		Instance.sceneParam.fadeType = fadeType;
		Instance.sceneParam.fadeTime = fadeTime;

		StartCoroutine(CoFade(
			  Instance.sceneParam.scene
			, Instance.sceneParam.mode
			, Instance.sceneParam.fadeType
			, Instance.sceneParam.fadeTime
			));
	}

	void Awake()
	{
		if (this != Instance)
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		isFading = false;
	}

	private IEnumerator CoFade(SceneName scene, LoadSceneMode mode, FadeType fadeType, float fadeTime = 1f)
	{
		if (fadeType == FadeType.Black)
		{
			_fadeTex.color = Color.black;
		}
		else if (fadeType == FadeType.White)
		{
			_fadeTex.color = Color.white;
		}

		// だんだん暗く
		isFading = true;
		float time = 0;
		var col = _fadeTex.color;

		while (time <= fadeTime)
		{
			_fadeTex.color = new Color(col.r, col.g, col.b, Mathf.Lerp(0f, 1f, time / fadeTime));
			time += Time.deltaTime;
			yield return 0;
		}

		//シーン切替 . 
		SceneManager.LoadScene(scene.ToString(), mode);

		// パッドを消す
		UiPadManager.Instance.SetUse(false);

		//だんだん明るく . 
		time = 0;
		while (time <= fadeTime)
		{
			_fadeTex.color = new Color(col.r, col.g, col.b, Mathf.Lerp(1f, 0f, time / fadeTime));
			time += Time.deltaTime;
			yield return 0;
		}


		isFading = false;

		sceneParam = null;

		gameObject.SetActive(false);
	}
}
