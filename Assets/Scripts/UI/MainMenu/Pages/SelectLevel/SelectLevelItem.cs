using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Mamont.UI.MainMenu.Pages
{
	public class SelectLevelItem:MonoBehaviour
	{
		public void Init( int index , string name , Action<int> onPressed )
		{
			HeaderText.text = name;
			_selectButton.onClick.AddListener(() => onPressed?.Invoke(index));
		}

		private void OnDestroy()
		{
			_selectButton.onClick.RemoveAllListeners();
		}

		[SerializeField]
		private Button _selectButton;

		[SerializeField]
		private TextMeshProUGUI HeaderText;
	}
}
