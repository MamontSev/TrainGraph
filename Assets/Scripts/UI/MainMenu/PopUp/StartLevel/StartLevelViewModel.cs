using Mamont.GeneralStateMashine;
using Mamont.UI.MVVM;

using Zenject;

namespace Mamont.UI.MainMenu.PopUp
{
	public class StartLevelViewModel:IPopUpViewModel
	{
		private GeneralGameStateMachine _stateMachine;

		[Inject]
		private void Contruct
		(
			GeneralGameStateMachine _stateMachine
		)
		{
			this._stateMachine = _stateMachine;
		}

		private IStartLevelView _myView;
		public void OnShowView( IStartLevelView _myView )
		{
			this._myView = _myView;
			_myView.SetHeaderText("Would you like start playing?");
		}

		public void OnPressedStartGame()
		{
			_stateMachine.Enter<GamePlayState>();
		}
	}
}
