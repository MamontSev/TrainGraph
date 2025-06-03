using Mamont.Core.Graph.Notion;
using Mamont.Core.Graph.Viewer;
using Mamont.Data.Graph.Builder;
using Mamont.Data.Graph.Config;
using Mamont.GameLevels;

using UnityEngine;

namespace Mamont.Core.Graph
{

	public class GraphCreator
	{
		private readonly GraphViewer _graphViewer;
		private readonly IGameLevelsService _gameLevelsService;
		private readonly GraphNotion _graphNotion;
		public GraphCreator
		(
			 GraphViewer _graphViewer ,
			 IGameLevelsService _gameLevelsServic ,
			 GraphNotion _graphNotion
		)
		{
			this._graphViewer = _graphViewer;
			this._gameLevelsService = _gameLevelsServic;
			this._graphNotion = _graphNotion;
		}

		private GraphDataConfig data;					 
		public void Create()
		{
			int levelIndex = _gameLevelsService.CurrPlayedLevel;
			data = _gameLevelsService.GetDataConfig(levelIndex);

			CreateView();
			CreateNotion();
		}

		private void CreateView()
		{
			foreach( var vertex in data.VertexData )
			{
				Vector3 pos = new(vertex.Position.X , vertex.Position.Y , vertex.Position.Z);
				switch( vertex.VertexType )
				{
					case GraphVertexType.Empty:
					{
						_graphViewer.AddVertexEmpty(vertex.NameIndex , pos);
					}
					break;
					case GraphVertexType.Mine:
					{
						_graphViewer.AddVertexMine(vertex.NameIndex , pos , vertex.Value);
					}
					break;
					case GraphVertexType.Base:
					{
						_graphViewer.AddVertexBase(vertex.NameIndex , pos , vertex.Value);
					}
					break;
				}
			}

            foreach (var edge in data.EdgeData)
            {
				_graphViewer.AddEdge(edge.Vertex1 , edge.Vertex2 , edge.Weight, edge.NameIndex);
			}
        }

		private void CreateNotion()
		{
			int levelIndex = _gameLevelsService.CurrPlayedLevel;
			data = _gameLevelsService.GetDataConfig(levelIndex);

			foreach( var vertex in data.VertexData )
			{
				_graphNotion.AddVertex(vertex.NameIndex);
			}

			foreach( var edge in data.EdgeData )
			{
				_graphNotion.AddEdge(edge.Vertex1 , edge.Vertex2 , edge.Weight , edge.NameIndex);
			}
		}
	}
}
