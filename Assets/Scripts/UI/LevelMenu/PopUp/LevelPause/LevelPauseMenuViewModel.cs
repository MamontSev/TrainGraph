using Mamont.GeneralStateMashine;
using Mamont.UI.MVVM;

using Zenject;

namespace Mamont.UI.LevelMenu.PopUp
{
	public class LevelPauseMenuViewModel:IPopUpViewModel
	{
		private GeneralGameStateMachine _generalGameStateMachine;
		[Inject]
		private void Construct
		(
			GeneralGameStateMachine _generalGameStateMachine
		)
		{
			this._generalGameStateMachine = _generalGameStateMachine;
		}

		private ILevelPauseMenuView _myView;
		public void OnShowView( ILevelPauseMenuView _myView )
		{
			this._myView = _myView;
			_myView.SetHeaderText("Game Pused");
		}

		public void OnBackToMenu()
		{
			_generalGameStateMachine.Enter<MenuState>();
		}
	}
}
