using Mamont.Events;
using Mamont.Events.Signals;
using Mamont.SceneControl;
using Mamont.UI.General.Loading;

using Manmont.Tools.StateMashine;

using UnityEngine;

namespace Mamont.GeneralStateMashine
{
	public class GamePlayState:IGeneralGameState, IEnterState ,IExitState
	{
		private readonly ILoadingPanel _loadingPanel;
		private readonly ISceneControlService _sceneControlService;
		private readonly IEventBusService _eventBusService;
		public GamePlayState
		(
			ILoadingPanel _loadingPanel ,
			ISceneControlService _sceneControlService,
			IEventBusService _eventBusService
		)
		{
			this._loadingPanel = _loadingPanel;
			this._sceneControlService = _sceneControlService;
			this._eventBusService = _eventBusService;
		}

		public void Enter()
		{
			_loadingPanel.Show();
			_sceneControlService.LoadGamePlay();
		}

		public void Exit()
		{
			Time.timeScale = 1.0f;
			_eventBusService.Invoke(new ExitGamePlayState());

		}
	}

}

