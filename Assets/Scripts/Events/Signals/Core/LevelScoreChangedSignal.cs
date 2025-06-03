using UnityEngine;

namespace Mamont.Events.Signals
{
	public class LevelScoreChangedSignal:IEventBusSignal
	{
		public readonly float Value;
		public LevelScoreChangedSignal( float value )
		{
			this.Value = value;
		}
	}
}
