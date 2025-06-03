
using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Mamont.UI.MainMenu.PopUp
{
	public class SelectTrainItem:MonoBehaviour
	{
		public void Init( int index , string name ,string movementSpeed,string extractionSpeed, Action<int> onPressed )
		{
			_nameText.text = name;
			_movementSpeedText.text = movementSpeed;
			_extractionSpeedText.text = extractionSpeed;
			_selectButton.onClick.AddListener(() => onPressed?.Invoke(index));
		}

		private void OnDestroy()
		{
			_selectButton.onClick.RemoveAllListeners();
		}

		[SerializeField]
		private TextMeshProUGUI _nameText;

		[SerializeField]
		private TextMeshProUGUI _movementSpeedText;

		[SerializeField]
		private TextMeshProUGUI _extractionSpeedText;

		[SerializeField]
		private Button _selectButton;
	}
}






