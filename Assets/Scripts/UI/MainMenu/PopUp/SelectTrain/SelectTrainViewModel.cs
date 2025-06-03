using System;

using Mamont.Data.Train;
using Mamont.GameLevels;
using Mamont.UI.MVVM;

using Zenject;

namespace Mamont.UI.MainMenu.PopUp
{
	public class SelectTrainViewModel:IPopUpViewModel
	{
		private ITrainDataConfig _trainDataConfig;
		private IGameLevelsService _gameLevelsService;
		private IMainMenuPopUpFactory _mainMenuPopUpFactory;
		[Inject]
		private void Contruct
		(
			ITrainDataConfig _trainDataConfig ,
			IGameLevelsService _gameLevelsService ,
			IMainMenuPopUpFactory _mainMenuPopUpFactory
		)
		{
			this._trainDataConfig = _trainDataConfig;
			this._gameLevelsService = _gameLevelsService;
			this._mainMenuPopUpFactory = _mainMenuPopUpFactory;
		}

		private ISelectTrainView _myView;
		public void OnShowView( ISelectTrainView _myView )
		{
			this._myView = _myView;
			SetHeaderText();
			InitTrainItems();
		}

		private void SetHeaderText()
		{
			_myView.SetHeaderText("Select train");
		}

		private void InitTrainItems()
		{
			foreach( TrainType type in Enum.GetValues(typeof(TrainType)) )
			{
				_myView.AddLevelItem(
				  (int)type ,
				  type.ToString() ,
				  $"Movement Speed:{_trainDataConfig.GetTrainData(type).movementSpeed}" ,
				  $"Extraction Time:{_trainDataConfig.GetTrainData(type).extractionTime}" ,
				  PressedTrain
				);
			}
		}

		private void PressedTrain( int index )
		{
			_gameLevelsService.CurrSelectedTrain = (TrainType)index;
			_mainMenuPopUpFactory.Show(new StartLevelViewModel());
		}
	}
}






