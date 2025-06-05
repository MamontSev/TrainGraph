using Mamont.Core.Inspector;
using Mamont.Core.Train.Model;
using Mamont.Core.Train.Viewer;
using Mamont.Data.Graph.Config;
using Mamont.Data.Train;
using Mamont.GameLevels;

using UnityEngine;

using Zenject;

namespace Mamont.Core.Train
{
	public class TrainFactory:MonoBehaviour
	{
		private DiContainer _diContainer;
		private IGameLevelsService _gameLevelsService;
		private ITrainDataConfig _trainDataConfig;
		private InsperctorValue _insperctorValue;

		[Inject]
		private void Construct
		(
			DiContainer _diContainer ,
			IGameLevelsService _gameLevelsService ,
			ITrainDataConfig _trainDataConfig,
			InsperctorValue _insperctorValue
		)
		{
			this._diContainer = _diContainer;
			this._gameLevelsService = _gameLevelsService;
			this._trainDataConfig = _trainDataConfig;
			this._insperctorValue = _insperctorValue;
		}

		[SerializeField]
		private TrainViewer _trainViewerPrefab;

		[SerializeField]
		private Transform _parentTransform;

		public void Create( TrainType trainType )
		{
			CreateInspectorValue(trainType);

			CreateActions(out TrainModelActions actions);

			CreateViewer(trainType,actions);

			CreateData(trainType , out ModelData modelData);

			TrainModel model 
				= _diContainer.Instantiate<TrainModel>(new object[]{ modelData, actions });
		}

		private void CreateData( TrainType trainType , out ModelData modelData )
		{
			modelData = new(_trainDataConfig.GetTrainData(trainType));

			int levelIndex = _gameLevelsService.CurrPlayedLevel;
			GraphDataConfig graphData = _gameLevelsService.GetDataConfig(levelIndex);

			foreach( var vertex in graphData.VertexData )
			{
				modelData.VertexDict.Add(vertex.NameIndex , new ModelVertexData { Type = vertex.VertexType , Value = vertex.Value });
			}
			foreach( var edge in graphData.EdgeData )
			{
				modelData.EdgesDict.Add(edge.NameIndex , new ModelEdgeData { Weight = edge.Weight , Vertex1 = edge.Vertex1 , Vertex2 = edge.Vertex2 });
			}
		}

		

		private void CreateViewer( TrainType trainType, TrainModelActions actions ) 
		{
			TrainViewer viewer = _diContainer.InstantiatePrefabForComponent<TrainViewer>(
			  _trainViewerPrefab.gameObject ,
			  _parentTransform,
			  new object[]{ actions/*, _trainDataConfig.GetTrainData(trainType)*/ });
			viewer.name = trainType.ToString();
		}
		private void CreateActions(out TrainModelActions actions)
		{
			actions = new();
		}

		private void CreateInspectorValue( TrainType trainType )
		{
			_insperctorValue.AddTrain(_trainDataConfig.GetTrainData(trainType));
		}
	}
}
