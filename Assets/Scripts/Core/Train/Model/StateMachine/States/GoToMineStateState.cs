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
			_graphNotion.Calculate(_selfData.CurrVertexName , out Dictionary<int , int> outDistances);
			int bestVertexName = 1;
			foreach( var item in _selfData.VertexDict )
			{
				if( item.Value.Type == GraphVertexType.Mine )
				{
					bestVertexName = item.Key;
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
					bestVertexName = vertexName;
				}
			}
			_graphNotion.GetPath(_selfData.CurrVertexName , bestVertexName , out _selfData.CurrWalkPath);
			_selfData.CurrWalkPath.Reverse();
			_selfActions.SetPathToMine(new List<int>(_selfData.CurrWalkPath) , _selfData.CurrVertexName);
			GoToVertex(0);
		}
		public override void Update()
		{
			UpdateGo(() => { _stateMachine.Enter<MineProgressState>(); } , () => { _stateMachine.Enter<GoToMineStateState>(); });
		}

	}
}
