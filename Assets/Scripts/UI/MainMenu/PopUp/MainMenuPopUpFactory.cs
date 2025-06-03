using Mamont.Log;
using Mamont.UI.General.PopUp;

using UnityEngine;

using Zenject;

namespace Mamont.UI.MainMenu.PopUp
{
	public interface IMainMenuPopUpFactory:IBasePopUpFactory
	{
	}
	public class MainMenuPopUpFactory:BasePopUpFactory, IMainMenuPopUpFactory
	{
		[Inject]
		private void Construct(
			ILogService _logService ,
			DiContainer _diContainer
		)
		{
			this._logService = _logService;
			this._diContainer = _diContainer;
		}

		protected sealed override void InitPrefabs()
		{
			_prefabDict.Add(typeof(FailLoadGraphDataViewModel) , _failLoadGraphDataView.gameObject);
			_prefabDict.Add(typeof(SelectTrainViewModel) , _selectTrainView.gameObject);
			_prefabDict.Add(typeof(StartLevelViewModel) , _startLevelView.gameObject);
		}

		[SerializeField]
		private FailLoadGraphDataView _failLoadGraphDataView;
		[SerializeField]
		private SelectTrainView _selectTrainView;
		[SerializeField]
		private StartLevelView _startLevelView;


	}
}
