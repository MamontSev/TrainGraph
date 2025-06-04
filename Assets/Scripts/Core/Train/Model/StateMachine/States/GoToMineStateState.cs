using System.Collections.Generic;

using Mamont.Core.Graph.Notion;
using Mamont.Data.Graph.Builder;

namespace Mamont.Core.Train.Model
{
	public class GoToMineStateState:AbstractGoToState
	{
		public GoToMineStateState
		( 
			GraphNotion _graphNotion ,
			ModelData _selfData ,
			TrainModelActions _selfActions ,
			TrainModelStateMachine _stateMachine ) : base(_graphNotion , _selfData , _selfActions , _stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
			if( _selfData.VertexDict[_selfData.CurrVertexName].Type == GraphVertexType.Mine )
			{
				_stateMachine.Enter<MineProgressState>();
				return;
			}
			CalculateFromVertex(
			 _selfData.CurrVertexName ,
			 out _currWalkPath);

			_currWalkPath.Reverse();
			_selfActions.SetPathToMine(new List<int>(_currWalkPath));
			GoToVertex(0);
		}

		private void CalculateFromVertex( int startVertex , out List<int> path , float offsetValue = 0.0f , int offfsetVertex = -1 )
		{
			_graphNotion.Calculate(_selfData.CurrVertexName , out Dictionary<int , int> outDistances);
			int targetIndex = 1;
			foreach( var item in _selfData.VertexDict )
			{
				if( item.Value.Type == GraphVertexType.Mine )
				{
					targetIndex = item.Key;
					break;
				}
			}
			float bsetTime = float.MaxValue;
			foreach( var item in outDistances )
			{
				int vertexName = item.Key;
				if( _selfData.VertexDict[vertexName].Type != GraphVertexType.Mine )
				{
					continue;
				}

				float newTime = item.Value + ( _selfData.VertexDict[vertexName].Value * _selfData.ExtractionTime );
				if( newTime < bsetTime )
				{
					bsetTime = newTime;
					targetIndex = vertexName;
				}
			}
			_graphNotion.GetPath(startVertex , targetIndex , out path);
		}

		public override void Update()
		{
			UpdateGo(() => { _stateMachine.Enter<MineProgressState>(); } , OnChangedGraphValue);
		}

		private void OnChangedGraphValue()
		{
			CalculateFromVertex(
				 _selfData.CurrVertexName ,
				 out List<int> path ,
				 _currGoValue ,
				 _selfData.TargetVertexName);

			path.Reverse();

			if( path.Count == 0 )
			{
				_currWalkPath = path;
				_currWalkPath.Insert(0 , _selfData.CurrVertexName);
				_selfData.CurrVertexName = _selfData.TargetVertexName;
				_selfActions.SetPathToMine(new List<int>(_currWalkPath));
				GoToVertex(0 , _currEdge.Weight - _currGoValue);
			}
			else if( path[0] == _selfData.TargetVertexName )
			{
				_currWalkPath = path;
				_selfActions.SetPathToMine(new List<int>(_currWalkPath));
				GoToVertex(0 , _currGoValue);
			}
			else
			{
				_currWalkPath = path;
				_currWalkPath.Insert(0 , _selfData.CurrVertexName);
				_selfData.CurrVertexName = _selfData.TargetVertexName;
				_selfActions.SetPathToMine(new List<int>(_currWalkPath));
				GoToVertex(0 , _currEdge.Weight - _currGoValue);
			}
		}

	}
}
