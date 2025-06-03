using Mamont.GameLevels;
using Mamont.UI.MainMenu.PopUp;

namespace Mamont.UI.MainMenu.Pages
{
	public class SelectLevelViewModel:IMainMenuPageViewModel
	{
		private readonly IGameLevelsService _gameLevelsService;
		private readonly IMainMenuPopUpFactory _mainMenuPopUpFactory;
		public SelectLevelViewModel
		(
			 IGameLevelsService _gameLevelsService,
			 IMainMenuPopUpFactory _mainMenuPopUpFactory
		)
		{
			this._gameLevelsService = _gameLevelsService;
			this._mainMenuPopUpFactory = _mainMenuPopUpFactory;
		}
		private ISelectLevelView _myView;

		public void OnShowView( ISelectLevelView _myView )
		{
			this._myView = _myView;
			SetHeaderText();
			CreateLevelItems();
		}

		private void SetHeaderText()
		{
			_myView.SetHeaderText("Select level");
		}

		private void CreateLevelItems()
		{
			for( int i = 0; i < _gameLevelsService.LevelsCount; i++ )
			{
				_myView.AddLevelItem(i , $"Level {i}" , OnPressedLevel);
			}
		}

		private void OnPressedLevel( int levelIndex )
		{
			_gameLevelsService.CurrPlayedLevel = levelIndex;
			_mainMenuPopUpFactory.Show(new SelectTrainViewModel());
		}



	}
}
