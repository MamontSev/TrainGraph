using Mamont.UI.MVVM;

namespace Mamont.UI.MainMenu.HUD
{
	public class MainMenuHudOverlayViewModel:IViewModel
	{
		public MainMenuHudOverlayViewModel()
		{

		}
		private IMainMenuHudOverlayView _view;
		public void OnInitView( IMainMenuHudOverlayView _view )
		{
			this._view = _view;
			this._view.SetHeaderText("Train Game");
		}
	}
}
