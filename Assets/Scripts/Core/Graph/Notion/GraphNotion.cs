using System.Collections.Generic;

using Mamont.Events;
using Mamont.Events.Signals;

using UnityEngine;

using Zenject;

namespace Mamont.Core.Graph.Notion
{
	public class GraphNotion:ILateDisposable
	{
		private readonly IEventBusService _eventBusService;
		public GraphNotion( IEventBusService _eventBusService )
		{
			this._eventBusService = _eventBusService;
			_eventBusService.Subscribe<GraphEdgeValueChangedSignal>(OnGraphEdgeValueChanged);
		}

		private void OnGraphEdgeValueChanged( GraphEdgeValueChangedSignal signal )
		{
			_graph.Edges
			.FindAll(x => x.Name == signal.NameIndex)
			.ForEach(x =>
			{
				x.Weight = signal.Value;
			});
		}


		private Graph _graph = new();

		public void AddVertex( int index )
		{
			_graph.Vertices.Add(new Vertex(index));
		}
		public void AddEdge( int vertexName1 , int vertexName2 , int weight , int index )
		{
			Vertex vertex1 = _graph.Vertices.Find(x => x.Name == vertexName1);
			Vertex vertex2 = _graph.Vertices.Find(x => x.Name == vertexName2);
			var edge = new Edge(vertex1 , vertex2 , weight , index);
			var edge1 = new Edge(vertex2 , vertex1 , weight , index);
			vertex1.Edges.Add(edge);
			vertex2.Edges.Add(edge1);

			_graph.Edges.Add(edge);
			_graph.Edges.Add(edge1);
		}

		private Dictionary<Vertex , int> _outDistances;
		private Dictionary<Vertex , Vertex> _previous;
		public void Calculate( int index , out Dictionary<int , int> outDistancesDict )
		{
			var vertex = _graph.Vertices.Find(x => x.Name == index);

			PathCalculator.DijkstraAlgorithm
			(
			  _graph ,
			  vertex ,
			  out _outDistances ,
			  out _previous);
			outDistancesDict = new();
			foreach( var item in _outDistances )
			{
				outDistancesDict.Add(item.Key.Name , item.Value);
			}
		}
		public void GetPath(int index, int tragetIndex, out List<int> path)
		{
			var vertex = _graph.Vertices.Find(x => x.Name == index);
			var targetVertex = _graph.Vertices.Find(x => x.Name == tragetIndex);

			path = new();
			while( targetVertex != vertex )
			{
				path.Add(targetVertex.Name);
				targetVertex = _previous[targetVertex];
				
			}

		}

		public void LateDispose()
		{
			_eventBusService.Unsubscribe<GraphEdgeValueChangedSignal>(OnGraphEdgeValueChanged);
		}
	}


}
