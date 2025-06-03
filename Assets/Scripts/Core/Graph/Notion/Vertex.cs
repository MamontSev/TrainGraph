using System.Collections.Generic;

namespace Mamont.Core.Graph.Notion
{
	public class Vertex
	{
		public int Name
		{
			get;
		}
		public List<Edge> Edges
		{
			get;
		}

		public Vertex( int name )
		{
			Name = name;
			Edges = new List<Edge>();
		}
	}


}
