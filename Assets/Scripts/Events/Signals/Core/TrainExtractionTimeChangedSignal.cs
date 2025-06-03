namespace Mamont.Events.Signals
{
	public class TrainExtractionTimeChangedSignal:IEventBusSignal
	{
		public readonly float Value;
		public TrainExtractionTimeChangedSignal( float value )
		{
			Value = value;
		}
	}
}
