using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PressStartButton : MonoBehaviour
{
	void OnPress(bool isDown)
	{
		if(isDown)
		{
			FadeManager.Instance.LoadScene(SceneName.HomeScene, LoadSceneMode.Single, FadeManager.FadeType.White);
		}
	}
}
