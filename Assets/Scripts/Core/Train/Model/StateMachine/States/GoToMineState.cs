using System.Collections.Generic;

using Mamont.Core.Graph.Notion;
using Mamont.Data.Graph.Builder;

namespace Mamont.Core.Train.Model
{
	public class GoToMineState:AbstractGoToState
	{
		public GoToMineState
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
			 out List<int> path);
			StartGo(path , this.GetType());
		}

		private void CalculateFromVertex( int startVertex , out List<int> path , float offsetValue = 0.0f , int offsetVertex = -1 )
		{
			_graphNotion.Calculate(_selfData.CurrVertexName , out Dictionary<int , int> outDistances);
			int targetVertex = 1;
			foreach( var item in _selfData.VertexDict )
			{
				if( item.Value.Type == GraphVertexType.Mine )
				{
					targetVertex = item.Key;
					break;
				}
			}
			float bsetTime = float.MaxValue;
			foreach( var item in outDistances )
			{
				int vertexName = item.Key;
				float dist = item.Value;
				if( vertexName == offsetVertex )
				{
					dist -= offsetValue;
				}
				if( _selfData.VertexDict[vertexName].Type != GraphVertexType.Mine )
				{
					continue;
				}

				float newTime = dist + ( _selfData.VertexDict[vertexName].Value * _selfData.ExtractionTime );
				if( newTime < bsetTime )
				{
					bsetTime = newTime;
					targetVertex = vertexName;
				}
			}
			_graphNotion.GetPath(startVertex , targetVertex , out path);
			path.Reverse();
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

			SetPathAfterValueChanged(path , this.GetType());
		}

	}
}
