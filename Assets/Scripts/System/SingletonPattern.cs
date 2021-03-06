﻿using UnityEngine;
using System;

/// <summary>
/// MonoBehaviour継承のシングルトンパターン
/// </summary>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				Type t = typeof(T);

				instance = (T)FindObjectOfType(t);
				if (instance == null)
				{
					Debug.LogError(t + " をアタッチしているGameObjectはありません");
				}
			}

			return instance;
		}
	}

	virtual protected void Awake()
	{
		// 他のGameObjectにアタッチされているか調べる.
		// アタッチされている場合は破棄する.
		if (this != Instance)
		{
			Destroy(this);
			return;
		}
	}

}
