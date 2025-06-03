using System.Collections.Generic;

namespace Mamont.Core.Graph.Notion
{
	public class Graph
	{
		public List<Vertex> Vertices
		{
			get;
		}
		public List<Edge> Edges
		{
			get;
		}
		public Graph()
		{
			Vertices = new List<Vertex>();
			Edges = new();
		}
	}


}
