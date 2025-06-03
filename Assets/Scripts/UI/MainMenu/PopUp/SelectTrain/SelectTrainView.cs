
using System;

using Mamont.UI.General.PopUp;
using Mamont.UI.MVVM;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Mamont.UI.MainMenu.PopUp
{
	public interface ISelectTrainView
	{
		void AddLevelItem( int index , string name , string movementSpeed , string extractionSpeed , Action<int> onPressed );
		void SetHeaderText( string s );
	}
	public class SelectTrainView:PopUpBaseAnimator<SelectTrainViewModel>, ISelectTrainView,IHideOnSystemBack
	{
		private SelectTrainViewModel _model;
		protected override void OnBind( SelectTrainViewModel model )
		{
			_model = model;
		}

		protected override void OnShow()
		{
			_model.OnShowView(this);
			FonClozeButton.onClick.AddListener(Cloze);
			ClozeButton.onClick.AddListener(Cloze);
		}
		protected override void OnHide()
		{
			FonClozeButton.onClick.RemoveAllListeners();
			ClozeButton.onClick.RemoveAllListeners();
		}

		private void Cloze()
		{
			Hide();
		}

		[Header("HeaderText")]
		[SerializeField]
		private TextMeshProUGUI HeaderText;
		public void SetHeaderText( string s )
		{
			HeaderText.text = s;
		}

		[Header("Train Items")]
		[SerializeField]
		private Transform _trainItemsContainer;
		[SerializeField]
		private SelectTrainItem _selectTrainItemPrefab;

		[Header("FonClozeButton")]
		[SerializeField]
		private Button FonClozeButton;

		[Header("ClozeButton")]
		[SerializeField]
		private Button ClozeButton;

		public void AddLevelItem( int index , string name , string movementSpeed , string extractionSpeed , Action<int> onPressed )
		{
			var item = Instantiate(_selectTrainItemPrefab , _trainItemsContainer).GetComponent<SelectTrainItem>();
			item.Init(index , name , movementSpeed, extractionSpeed, onPressed);
		}
	}
}






