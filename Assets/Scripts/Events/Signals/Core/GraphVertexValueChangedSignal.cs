namespace Mamont.Events.Signals
{
	public class GraphVertexValueChangedSignal:IEventBusSignal
	{
		public readonly int NameIndex;
		public readonly float Value;
		public GraphVertexValueChangedSignal(  float value, int nameIndex  )
		{
			NameIndex = nameIndex;
			Value = value;
		}
	}
}
