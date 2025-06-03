using Mamont.Events;
using Mamont.Events.Signals;

namespace Mamont.Core.Score
{
	public class ScoreControl:IScoreControl
	{
		private IEventBusService _eventBusService;
        public ScoreControl
		(
			 IEventBusService _eventBusService
		)
        {
			this._eventBusService = _eventBusService;
		}

		public float CurrScore
		{
			private set;
			get;
		} = 0.0f;

		public void AddScore( float amount )
		{
			CurrScore += amount;
			_eventBusService.Invoke(new LevelScoreChangedSignal(CurrScore));
		}
    }
}
