namespace Mamont.Events.Signals
{
	public class TrainMovementSpeedChangedSignal:IEventBusSignal
	{
		public readonly float Value;
		public TrainMovementSpeedChangedSignal( float value )
		{
			Value = value;
		}									 
	}
}
