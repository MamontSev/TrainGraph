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
			 out List<int> path);
			StartGo(path,this.GetType());
        }

		private void CalculateFromVertex( int startVertex , out List<int> path , float offsetValue = 0.0f , int offsetVertex = -1 )
		{
			int targetVertex = 1;
			float param = -1.0f;
			_graphNotion.Calculate(startVertex , out Dictionary<int , int> outDistances);
			foreach( var item in outDistances )
			{
				int vertexName = item.Key;
				float dist = item.Value;
				if( vertexName == offsetVertex )
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
					targetVertex = vertexName;
				}
			}
			_graphNotion.GetPath(startVertex , targetVertex , out path);
			path.Reverse();
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

			SetPathAfterValueChanged(path, this.GetType());
		}
	}
}
