using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
	/// <summary>
	/// メニューボタンの種類
	/// </summary>
	private enum ButtonAction
	{
		Start,
		Continue,
		Config,

		MapEdit,
	}

	/// <summary> メニュー </summary>
	[SerializeField]
	private CommonMenuScript _menu = null;

	private void Awake()
	{
		_menu.SetUp(Decide);
	}

	private void Decide(int index)
	{
		switch ((HomeScene.ButtonAction)index)
		{
		case ButtonAction.Start:

			OnClickStart();

			break;

		case ButtonAction.Continue:

			OnClickContinue();

			break;

		case ButtonAction.Config:

			OnClickConfig();

			break;

		case ButtonAction.MapEdit:

			OnClickMapEdit();

			break;
		}
	}

	/// <summary>
	/// Startボタン押下
	/// </summary>
	public void OnClickStart()
	{
		FadeManager.Instance.LoadScene(SceneName.FieldMapScene, LoadSceneMode.Single, FadeManager.FadeType.White);
	}

	/// <summary>
	/// Continueボタン押下
	/// </summary>
	public void OnClickContinue()
	{
		Debug.Log("On Continue");
	}

	/// <summary>
	/// Configボタン押下
	/// </summary>
	public void OnClickConfig()
	{
		Debug.Log("On Config");
	}

	/// <summary>
	/// Mapボタン押下
	/// </summary>
	public void OnClickMapEdit()
	{
		FadeManager.Instance.LoadScene(SceneName.NavigationScene, LoadSceneMode.Single, FadeManager.FadeType.White);
	}
}
