using Mamont.Core.GameLoop;
using Mamont.Core.Graph;
using Mamont.Core.Train;
using Mamont.Data.Train;
using Mamont.GameLevels;
using Mamont.UI.General.Loading;

using UnityEngine;

using Zenject;

namespace Mamont.EntryPoint
{
	public class GamePlayEntryPoint:MonoBehaviour
	{
		private ILoadingPanel _loadingPanel;
		private GraphCreator _graphCreator;
		private TrainFactory _trainFactory;
		private IGameLevelsService _gameLevelsService;
		private IGameLoopControl _gameLoopControl;
		[Inject]
		private void Construct
		(
			ILoadingPanel _loadingPanel ,
			GraphCreator _graphCreator ,
			TrainFactory _trainFactory ,
			IGameLevelsService _gameLevelsService ,
			IGameLoopControl _gameLoopControl
		)
		{
			this._loadingPanel = _loadingPanel;
			this._graphCreator = _graphCreator;
			this._trainFactory = _trainFactory;
			this._gameLevelsService = _gameLevelsService;
			this._gameLoopControl = _gameLoopControl;
		}
		private void Start()
		{
			_graphCreator.Create();

			TrainType trainType = _gameLevelsService.CurrSelectedTrain;
			_trainFactory.Create(trainType);

			_gameLoopControl.Start();
			_loadingPanel.Hide();
		}
	}
}



