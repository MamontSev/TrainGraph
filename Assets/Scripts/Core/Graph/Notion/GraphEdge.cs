namespace Mamont.Core.Graph.Notion
{

	public class Edge
	{
		public Vertex From
		{
			get;
		}
		public Vertex To
		{
			get;
		}
		public int Weight
		{
			get;
			set;
		}
		public int Name
		{
			get;
		}

		public Edge( Vertex from , Vertex to , int weight , int name )
		{
			Name = name;
			From = from;
			To = to;
			Weight = weight;
		}
	}


}
