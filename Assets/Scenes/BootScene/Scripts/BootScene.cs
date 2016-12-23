using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BootScene : MonoBehaviour
{
	void Start()
	{
		StartCoroutine(CoSetUp());
		DontDestroyOnLoad(gameObject);
	}

	private IEnumerator CoSetUp()
	{
		UiPadManager.Instance.SetUse(false);

		yield return StartCoroutine(MasterDataManager.Instance.CoLoad());

		SceneManager.LoadSceneAsync(SceneName.TitleScene.ToString(), LoadSceneMode.Single);
		SceneManager.LoadSceneAsync(SceneName.FadeScene.ToString(), LoadSceneMode.Additive);
	}
}
