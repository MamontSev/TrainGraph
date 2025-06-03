using Mamont.Events;
using Mamont.Events.Signals;

using UnityEngine;

using Zenject;

namespace Mamont.UI.LevelMenu.HUD
{
	public class LevelMenuHudFactory:MonoBehaviour 
	{
		private DiContainer _diContainer;
		private IEventBusService _eventBusService;
		[Inject]
		private void Construct
		(
			DiContainer _diContainer,
			IEventBusService _eventBusService
		)
		{
			this._diContainer = _diContainer;
			this._eventBusService = _eventBusService;
			_eventBusService.Subscribe<LevelInitCompletedSignal>(OnLevelStart);
		}

		private void OnLevelStart( LevelInitCompletedSignal signal )
		{
			InitHud();
			_eventBusService.Unsubscribe<LevelInitCompletedSignal>(OnLevelStart);
		}



		[SerializeField]
		private LevelMenuHudView _hudMenu;

		private void InitHud()
		{
			LevelMenuHudViewModel vm = _diContainer.Instantiate<LevelMenuHudViewModel>();
			_hudMenu.Bind(vm);
		}
	}
}
