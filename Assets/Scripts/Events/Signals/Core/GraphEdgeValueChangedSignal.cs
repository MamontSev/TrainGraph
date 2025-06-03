namespace Mamont.Events.Signals
{
	public class GraphEdgeValueChangedSignal:IEventBusSignal
	{
		public readonly int NameIndex;
		public readonly int Value;
		public GraphEdgeValueChangedSignal( int value , int nameIndex )
		{
			NameIndex = nameIndex;
			Value = value;
		}
	}
}
