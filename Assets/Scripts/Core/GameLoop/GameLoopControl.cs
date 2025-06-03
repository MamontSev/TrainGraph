using System.Collections.Generic;

using Mamont.Events;
using Mamont.Events.Signals;
using Mamont.Log;

using Zenject;

namespace Mamont.Core.GameLoop
{
	public class GameLoopControl:IGameLoopControl,ITickable
	{
		private readonly IEventBusService _eventBusService;
		private readonly ILogService _logService;
		public GameLoopControl
		( 
			IEventBusService _eventBusService,
			ILogService _logService
		)
		{
			this._eventBusService = _eventBusService;
			this._logService = _logService;
		}

		private LevelState _currState = LevelState.waitForStart;

		public void Start()
		{
			_currState = LevelState.playing;
			_eventBusService.Invoke(new LevelInitCompletedSignal());
		}

		private int _pauseCounter = 0;
		public void Pause()
		{
			_pauseCounter++;
			if( IsPlaying == true )
			{
				_currState = LevelState.paused;
				_eventBusService.Invoke(new LevelPauseSignal(true));
			}

		}
		public void UnPause()
		{
			_pauseCounter--;
			if( _pauseCounter == 0 )
			{
				_currState = LevelState.playing;
				_eventBusService.Invoke(new LevelPauseSignal(false));
			}
		}

		public bool IsPlaying => _currState == LevelState.playing;

		private enum LevelState
		{
			waitForStart,
			playing,
			paused
		}

		private readonly List<IGameLoopUpdate> _gameLoopList = new();
		public void Register( IGameLoopUpdate item )
		{
			if( _gameLoopList.Contains(item) )
			{
				_logService.LogError($"GameLoopControl Register - try add again `{item}`");
				return;
			}
			_gameLoopList.Add(item);
		}
		public void Unregister( IGameLoopUpdate item )
		{
			if( _gameLoopList.Contains(item) == false )
			{
				_logService.LogError($"GameLoopControl Unregister - not contains `{item}`");
				return;
			}
			_gameLoopList.Remove(item);
		}

		public void Tick()
		{
			if( IsPlaying == false )
			{
				return;
			}
			_gameLoopList.ForEach(x => x.Update());
		}



	}
}
