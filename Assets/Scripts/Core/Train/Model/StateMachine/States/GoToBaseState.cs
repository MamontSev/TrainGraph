using System.Collections.Generic;

using Mamont.Core.Graph.Notion;
using Mamont.Data.Graph.Builder;


namespace Mamont.Core.Train.Model
{
	public class GoToBaseState:AbstractGoToState
	{
		public GoToBaseState
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
			if( _selfData.VertexDict[_selfData.CurrVertexName].Type == GraphVertexType.Base )
			{
				_stateMachine.Enter<BaseProgressState>();
				return;
			}
			CalculateFromVertex(
			 _selfData.CurrVertexName ,
			 out _currWalkPath);


			_currWalkPath.Reverse();
			_selfActions.SetPathToBase(new List<int>(_currWalkPath));
			GoToVertex(0);

		}

		private void CalculateFromVertex( int startVertex , out List<int> path , float offsetValue = 0.0f , int offfsetVertex = -1 )
		{
			int targetIndex = 1;
			float param = -1.0f;
			_graphNotion.Calculate(startVertex , out Dictionary<int , int> outDistances);
			foreach( var item in outDistances )
			{
				int vertexName = item.Key;
				float dist = item.Value;
				if( vertexName == offfsetVertex )
				{
					dist -= offsetValue;
				}

				if( _selfData.VertexDict[vertexName].Type != GraphVertexType.Base )
				{
					continue;
				}
				float newParam;
				if( _selfData.VertexDict[vertexName].Value == 0.0f )
				{
					newParam = 0.0f;
				}
				else if( dist == 0.0f )
				{
					newParam = _selfData.VertexDict[vertexName].Value;
				}
				else
				{
					newParam = _selfData.VertexDict[vertexName].Value / dist;
				}
				if( newParam > param )
				{
					param = newParam;
					targetIndex = vertexName;
				}
			}
			_graphNotion.GetPath(startVertex , targetIndex , out path);
		}


		public override void Update()
		{
			UpdateGo(() => { _stateMachine.Enter<BaseProgressState>(); } , OnChangedGraphValue);
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
				_selfActions.SetPathToBase(new List<int>(_currWalkPath));
				GoToVertex(0 , _currEdge.Weight - _currGoValue);
			}
			else if( path[0] == _selfData.TargetVertexName )
			{
				_currWalkPath = path;
				_selfActions.SetPathToBase(new List<int>(_currWalkPath));
				GoToVertex(0 , _currGoValue);
			}
			else
			{
				_currWalkPath = path;
				_currWalkPath.Insert(0 , _selfData.CurrVertexName);
				_selfData.CurrVertexName = _selfData.TargetVertexName;
				_selfActions.SetPathToBase(new List<int>(_currWalkPath));
				GoToVertex(0 , _currEdge.Weight - _currGoValue);
			}
		}
	}
}
