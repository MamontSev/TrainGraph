using Mamont.SceneControl;
using Mamont.UI.General.Loading;

using Manmont.Tools.StateMashine;

namespace Mamont.GeneralStateMashine
{
	public class MenuState:IGeneralGameState, IEnterState
	{
		private readonly ILoadingPanel _loadingPanel;
		private readonly ISceneControlService _sceneControlService;
		public MenuState
		(
			ILoadingPanel _loadingPanel,
			ISceneControlService _sceneControlService
		)
		{
			this._loadingPanel = _loadingPanel;
			this._sceneControlService = _sceneControlService;
		}
		public void Enter()
		{
			_loadingPanel.Show();
			_sceneControlService.LoadMenu();
		}
	}
}
