using Mamont.Core.Score;
using Mamont.Events;
using Mamont.Events.Signals;
using Mamont.GameLevels;
using Mamont.UI.LevelMenu.PopUp;
using Mamont.UI.MVVM;

using Manmont.Tools;

namespace Mamont.UI.LevelMenu.HUD
{
	public class LevelMenuHudViewModel:IViewModel
	{
		private readonly IGameLevelsService _gameLevelsService;
		private readonly ILevelMenuPopUpFacrtory _levelMenuPopUpFacrtory;
		private readonly IEventBusService _eventBusService;
		private readonly IScoreControl _levelScoreControl;
		public LevelMenuHudViewModel
		(
			IGameLevelsService _gameLevelsService,
			ILevelMenuPopUpFacrtory _levelMenuPopUpFacrtory,
			IEventBusService _eventBusService,
			IScoreControl _levelScoreControl
		)
		{
			this._gameLevelsService = _gameLevelsService;
			this._levelMenuPopUpFacrtory = _levelMenuPopUpFacrtory;
			this._eventBusService = _eventBusService;
			this._levelScoreControl = _levelScoreControl;
			SubscribeEvents();
		}

		private bool _isSubscribe = false;
		private void SubscribeEvents()
		{
			if( _isSubscribe == true )
			{
				return;
			}
			_isSubscribe = true;
			_eventBusService.Subscribe<LevelScoreChangedSignal>(OnLevelScoreChangedSignal);
			_eventBusService.Subscribe<ExitGamePlayState>(OnExitGamePlayState);
		}
		private void UnSubscribeEvents()
		{
			if( _isSubscribe == false )
			{
				return;
			}
			_isSubscribe = false;

			_eventBusService.Unsubscribe<LevelScoreChangedSignal>(OnLevelScoreChangedSignal);
			_eventBusService.Unsubscribe<ExitGamePlayState>(OnExitGamePlayState);
		}

		private void OnExitGamePlayState( ExitGamePlayState signal )
		{
			UnSubscribeEvents();
		}

		private void OnLevelScoreChangedSignal( LevelScoreChangedSignal signal )
		{
			SetScore();
		}

		private ILevelMenuHudView _myView = null;
		public void OnInitView( ILevelMenuHudView myView )
		{
			_myView = myView;
			SetLevelNameText();
			SetScore();
		}

		public void PressedPause() 
		{
			_levelMenuPopUpFacrtory.Show(new LevelPauseMenuViewModel());
		}

		private void SetLevelNameText()
		{
			_myView.SetLevelText($"Level {_gameLevelsService.CurrPlayedLevel} \n {_gameLevelsService.CurrSelectedTrain} ");
		}

		private void SetScore()
		{
			_myView.SetScoreText($"Score: {_levelScoreControl.CurrScore.DigitToString()}");
		}
	}
}
