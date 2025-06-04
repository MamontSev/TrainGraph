using System;
using System.Collections.Generic;

using Mamont.Core.Graph.Notion;

using Manmont.Tools.StateMashine;

using UnityEngine;

namespace Mamont.Core.Train.Model
{
	public abstract class AbstractGoToState:ITrainModelState, IEnterState, IExitState, IUpdateState
	{
		protected readonly GraphNotion _graphNotion;
		protected readonly ModelData _selfData;
		protected readonly TrainModelActions _selfActions;
		protected readonly TrainModelStateMachine _stateMachine;
		public AbstractGoToState
		(
			GraphNotion _graphNotion ,
			ModelData _selfData ,
			TrainModelActions _selfActions ,
			TrainModelStateMachine _stateMachine
		)
		{
			this._graphNotion = _graphNotion;
			this._selfData = _selfData;
			this._selfActions = _selfActions;
			this._stateMachine = _stateMachine;
		}
		public virtual void Enter()
		{
			_selfActions.OnChangedGraphValue += OnGraVertexEdgeValueChanged;
		}

		public void Exit()
		{
			_selfActions.OnChangedGraphValue -= OnGraVertexEdgeValueChanged;
		}
		public abstract void Update();

		private void OnGraVertexEdgeValueChanged()
		{
			
			_wasChangeGraphValueOnGo = true;
		}

		private bool _wasChangeGraphValueOnGo;

		protected int _currPathIndex;
		protected ModelEdgeData _currEdge;

		protected List<int> _currWalkPath = new();
		protected float _currGoValue;


		protected void GoToVertex( int _pathIndex, float goValue = 0.0f )
		{
			_wasChangeGraphValueOnGo = false;
			_currGoValue = goValue;
			_currPathIndex = _pathIndex;
			_selfData.TargetVertexName = _currWalkPath[_currPathIndex];

			foreach( var item in _selfData.EdgesDict )
			{
				if( item.Value.Vertex1 == _selfData.CurrVertexName && item.Value.Vertex2 == _selfData.TargetVertexName )
				{
					_currEdge = item.Value;
					break;
				}
				if( item.Value.Vertex2 == _selfData.CurrVertexName && item.Value.Vertex1 == _selfData.TargetVertexName )
				{
					_currEdge = item.Value;
					break;
				}
			}
			_selfActions.SetPathStartIndex(_currPathIndex);
		}

		protected void UpdateGo( Action onComplete , Action onChangedGraphValue )
		{
			_currGoValue += _selfData.MovementSpeed * Time.deltaTime;
			if(  _currEdge.Weight != 0.0f )
			{
				_selfActions.PathProgress(_selfData.CurrVertexName , _selfData.TargetVertexName , _currGoValue / _currEdge.Weight);
			}
		
			if( _currGoValue < _currEdge.Weight )
			{
				if( _wasChangeGraphValueOnGo )
				{
					_selfActions.PathComplete();
					onChangedGraphValue?.Invoke();
				}
				return;
			}
			_currGoValue = _currEdge.Weight;
			_selfData.CurrVertexName = _selfData.TargetVertexName;
			_selfActions.CompleteGoToVertex(_selfData.CurrVertexName);

			if( _currPathIndex == _currWalkPath.Count - 1 )
			{
				_selfActions.PathComplete();
				onComplete?.Invoke();
				return;
			}
			
			GoToVertex(_currPathIndex + 1);
		}


	}
}
