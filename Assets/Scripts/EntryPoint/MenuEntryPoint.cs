using Mamont.GameLevels;
using Mamont.UI.General.Loading;
using Mamont.UI.MainMenu.Pages;
using Mamont.UI.MainMenu.PopUp;

using UnityEngine;

using Zenject;

namespace Mamont.EntryPoint
{
	public class MenuEntryPoint:MonoBehaviour
	{
		private ILoadingPanel _loadingPanel;
		private IMainMenuPagesFactory _mainMenuPagesFactory;
		private IMainMenuPopUpFactory _mainMenuPopUpFactory;
		private IGameLevelsService _gameLevelsService;
		[Inject]
		private void Construct
		(
			ILoadingPanel _loadingPanel ,
			IMainMenuPagesFactory _mainMenuPagesFactory,
			IMainMenuPopUpFactory _mainMenuPopUpFactory ,
			IGameLevelsService _gameLevelsService
		)
		{
			this._loadingPanel = _loadingPanel;
			this._mainMenuPagesFactory = _mainMenuPagesFactory;
			this._mainMenuPopUpFactory = _mainMenuPopUpFactory;
			this._gameLevelsService = _gameLevelsService;
		}
		private void Start()
		{
			_loadingPanel.Hide();
			_gameLevelsService.LoadData
			(
				()=>{ _mainMenuPagesFactory.CreatePage<SelectLevelViewModel>(); } ,
				s=> { _mainMenuPopUpFactory.Show(new FailLoadGraphDataViewModel(s)); }
			);
		}
	}
}



