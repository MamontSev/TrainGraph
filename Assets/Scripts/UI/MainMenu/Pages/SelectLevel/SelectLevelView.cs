using System;

using TMPro;

using UnityEngine;

namespace Mamont.UI.MainMenu.Pages
{
	public interface ISelectLevelView
	{
		void AddLevelItem( int index , string name , Action<int> onPressed );
		void SetHeaderText( string s );
	}
	public class SelectLevelView:MainMenuPageBase<SelectLevelViewModel>, ISelectLevelView
	{
		private SelectLevelViewModel _model;
		protected override void OnBind( SelectLevelViewModel model )
		{
			this._model = model;
		}

		protected override void OnHide()
		{
			
		}

		protected override void OnShow()
		{
			_model.OnShowView(this);
		}

		[Header("HeaderText")]
		[SerializeField]
		private TextMeshProUGUI HeaderText;
		public void SetHeaderText( string s )
		{
			HeaderText.text = s;
		}

		[Header("Level Items")]
		[SerializeField]
		private Transform _levelItemsContainer;
		[SerializeField]
		private SelectLevelItem _selectLevelItemPrefab;

		public void AddLevelItem( int index , string name , Action<int> onPressed )
		{
			var item = Instantiate(_selectLevelItemPrefab , _levelItemsContainer).GetComponent<SelectLevelItem>();
			item.Init(index , name , onPressed);
		}


	}
}
