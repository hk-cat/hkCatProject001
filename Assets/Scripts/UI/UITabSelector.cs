using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// タブの選択機能コンポーネント
/// </summary>
public class UITabSelector : MonoBehaviour
{
	/// <summary>タブのリスト</summary>
	[SerializeField]
	private List<UITab> _tabs = null;

	/// <summary>選択中のインデックス</summary>
	private int _selectedIndex = -1;

	/// <summary>
	/// タブを選択
	/// </summary>
	/// <param name="index"></param>
	public void SelectTab(int index)
	{
		if (index == _selectedIndex) { return; }

		if (_selectedIndex != -1)
		{
			_tabs[_selectedIndex].OnUnselect();
		}

		_tabs[index].OnSelect();

		_selectedIndex = index;
	}
}
