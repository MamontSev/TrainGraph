using System.Collections.Generic;
using System.Linq;

namespace Mamont.Core.Graph.Notion
{
	public static class PathCalculator
	{
		public static void DijkstraAlgorithm
		( 
			Graph graph , 
			Vertex source,
			out Dictionary<Vertex , int> outDistances,
			out Dictionary<Vertex , Vertex> previous 
		)
		{
			var distances = graph.Vertices.ToDictionary(v => v , v => int.MaxValue);
			previous = new Dictionary<Vertex , Vertex>();
			var notVisited = new HashSet<Vertex>(graph.Vertices);

			distances[source] = 0;

			while( notVisited.Any() )
			{
				var nearestVertex = notVisited.OrderBy(v => distances[v]).FirstOrDefault();
				notVisited.Remove(nearestVertex);

				foreach( var edge in nearestVertex.Edges )
				{
					var neighbor = edge.To;
					if( notVisited.Contains(neighbor) )
					{
						var currentDistance = distances[nearestVertex] + edge.Weight;
						if( currentDistance < distances[neighbor] )
						{
							distances[neighbor] = currentDistance;
							previous[neighbor] = nearestVertex;
						}
					}
				}
			}

			outDistances = distances;
		}
	}


}
