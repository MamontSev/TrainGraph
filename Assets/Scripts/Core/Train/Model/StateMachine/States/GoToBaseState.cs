using System.Collections.Generic;
using System.Linq;

using Mamont.Core.Graph.Notion;
using Mamont.Data.Graph.Builder;

using UnityEngine;

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
			_graphNotion.Calculate(_selfData.CurrVertexName , out Dictionary<int , int> outDistances);
			int bestVertexName = 1;
            foreach (var item in _selfData.VertexDict)
            {
				if( item.Value.Type == GraphVertexType.Base )
				{
					bestVertexName = item.Key;
					break;
				}
            }           
			float bestParam = 0.0f;
			foreach( var item in outDistances )
			{
				int vertexName = item.Key;
				if( _selfData.VertexDict[vertexName].Type != GraphVertexType.Base )
				{
					continue;
				}

				float newParam = _selfData.VertexDict[vertexName].Value / item.Value;
				if( newParam > bestParam )
				{
					bestParam = newParam;
					bestVertexName = vertexName;
				}
			}
			_graphNotion.GetPath(_selfData.CurrVertexName , bestVertexName , out _selfData.CurrWalkPath);
			_selfData.CurrWalkPath.Reverse();
			_selfActions.SetPathToBase(new List<int>(_selfData.CurrWalkPath) , _selfData.CurrVertexName);
			GoToVertex(0);
		}
		public override void Update()
		{
			UpdateGo(() => { _stateMachine.Enter<BaseProgressState>(); } , () => { _stateMachine.Enter<GoToBaseState>(); });
		}

	}
}
