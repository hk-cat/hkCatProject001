using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class FirstCustomWindow : EditorWindow
{
	public const int DEFAULT_PREFAB_NUM = 5;
	string myString = "Hello World";
	bool groupEnabled;
	bool myBool = true;
	bool foldout = false;
	float myFloat = 1.23f;
	Color col = Color.white;
	Texture2D tex = null;

	/// <summary>
	/// プレハブリスト
	/// </summary>
	int prefabNum = 0;
	private List<GameObject> prefabList = new List<GameObject>();
	private List<string> prefabNameList = new List<string>();
	int sel = 0;

	[MenuItem("Window/Custom Window/Show First Custom Window")]
	public static void ShowWindow()
	{
		FirstCustomWindow window = EditorWindow.GetWindow<FirstCustomWindow>();
		window.Show();
	}

	private void Initialize()
	{
		for(int i = 0; i < DEFAULT_PREFAB_NUM; ++i)
		{
			prefabList.Add(null);
		}
	}

	void OnGUI()
	{
		// 色んな拡張サンプル
		VariousSample();

		// プレハブリスト
		OnGUIPrefabSetting();

		sel = EditorGUILayout.Popup("作成プレハブの選択", sel, prefabNameList.ToArray());

		if (GUILayout.Button("作成"))
		{
			if (prefabList[sel] != null)
			{
				PrefabUtility.InstantiatePrefab(prefabList[sel]);
				Debug.Log(prefabNameList[sel] + "を作成しました");
			}
			else
			{
				Debug.Log("プレハブを設定してください");
			}
		}
	}

	/// <summary>
	/// サンプル
	/// </summary>
	private void VariousSample()
	{
		GUILayout.Label("Base Settings", EditorStyles.boldLabel);
		myString = EditorGUILayout.TextField("Text Field", myString);
		col = EditorGUILayout.ColorField("Color Field", col);

		groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
		myBool = EditorGUILayout.Toggle("Toggle", myBool);
		myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
		EditorGUILayout.EndToggleGroup();

		// Texture2Dを選ばせる例
		tex = EditorGUILayout.ObjectField("テクスチャ", tex, typeof(Texture2D), false) as Texture2D;

		if (GUILayout.Button("クリア"))
		{
			tex = null;
		}
	}

	/// <summary>
	/// プレハブリスト
	/// </summary>
	private void OnGUIPrefabSetting()
	{
		if (foldout = EditorGUILayout.Foldout(foldout, "プレハブリスト"))
		{
			var num = EditorGUILayout.IntField("プレハブ数", prefabNum);

			if (num < DEFAULT_PREFAB_NUM)
			{
				num = DEFAULT_PREFAB_NUM;
			}

			// 違う数値が入ったら
			if (prefabNum != num)
			{
				// 中身をコピーしておく
				var list = new List<GameObject>(prefabList);
				var names = new List<string>(prefabNameList);
				// リストをクリアする
				prefabList.Clear();
				prefabNameList.Clear();

				// 数値を更新する
				prefabNum = num;

				for (int i = 0; i < prefabNum; ++i)
				{
					// コピーしておいた中身があれば入れなおし、なければnullを入れる
					if (i < list.Count)
					{
						prefabList.Add(list[i]);
					}
					else
					{
						prefabList.Add(null);
					}

					if (i < names.Count)
					{
						prefabNameList.Add(names[i]);
					}
					else
					{
						prefabNameList.Add(null);
					}
				}
			}

			for (int i = 0; i < prefabNum; ++i)
			{
				EditorGUILayout.BeginHorizontal();      // 開始
				prefabNameList[i] = EditorGUILayout.TextField("プレハブ名", prefabNameList[i]);
				// Texture2Dを選ばせる例
				prefabList[i] = EditorGUILayout.ObjectField("プレハブ", prefabList[i], typeof(GameObject), false) as GameObject;
				EditorGUILayout.EndHorizontal();        // 終了
			}
		}
	}
}
